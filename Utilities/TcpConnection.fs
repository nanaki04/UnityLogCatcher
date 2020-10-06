namespace UnityLogCatcher.Utilities

module TcpConnection =
  open System.Threading
  open System.Net.Sockets
  open System.Text

  type Message =
    | Store of string
    | Consume of AsyncReplyChannel<string list>

  type Connection = TcpClient * NetworkStream * Thread * MailboxProcessor<Message>

  let private handleConsumeMessage (input : string) (consumer : AsyncReplyChannel<string list>) =
    match (input.Split '\n') |> List.ofArray |> List.rev with
    | incompleteMessage::messages ->
      consumer.Reply messages
      incompleteMessage
    | _ ->
      input

  let private handleMessage input message =
    match message with
    | Store data -> input + data
    | Consume consumer -> handleConsumeMessage input consumer

  let private createAgent () = MailboxProcessor.Start(fun inbox ->
    let rec receive input = async {
      let! message = inbox.Receive()
      return! handleMessage input message |> receive
    }
  
    receive ""
  )

  let connect (server : ServerAddress) =
    let client = new TcpClient(server.Ip, server.Port)
    let stream = client.GetStream ()
    let agent = createAgent ()
    let rec listen () =
      let buffer = Array.create 256 (byte(0))
      let length = stream.Read (buffer, 0, buffer.Length)
      Encoding.ASCII.GetString (buffer, 0, length)
      |> Store
      |> agent.Post
      listen ()
  
    let connectionThread = Thread listen
    connectionThread.Start ()

    (client, stream, connectionThread, agent)

  let close (connection : Connection) =
    let (client, _, connectionThread, _) = connection
    client.Close ()
    connectionThread.Abort ()

  let send message (connection : Connection) =
    let (_, stream, _, _) = connection
    let bytes = Encoding.ASCII.GetBytes (message + "\n")
    stream.Write (bytes, 0, bytes.Length)

  let receive (connection : Connection) =
    let (_, _, _, agent) = connection
    agent.PostAndReply Consume