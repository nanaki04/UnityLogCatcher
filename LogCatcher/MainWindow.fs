namespace UnityLogCatcher.LogCatcher

open UnityEngine
open UnityEditor
open UnityLogCatcher.Utilities.Utility
open UnityLogCatcher.Utilities

module MainWindow =

  let logAllEntries state =
    state.Logs
    |> List.fold (fun acc logEntry -> Ui.label (LogEntry.unwrap logEntry) acc) state

  let showConnectionButton (state : State) =
    match state.Connection with
    | Some connection ->
      Ui.button "Disconnect" (fun state ->
        TcpConnection.close connection
        State.withoutConnection state
      ) state
    | None ->
      Ui.button "Connect" (fun state ->
        TcpConnection.connect state.ServerAddress
        |> State.withConnection
        <| state
      ) state
      
  let readIncomingMessages (state : State) =
    match state.Connection with
    | Some connection ->
      TcpConnection.receive connection
      |> MessageReader.read
      <| state
    | None ->
      state

  type LogCatcherWindow () =
    inherit EditorWindow ()

    member self.OnGUI () =
      State.load ()
      |> readIncomingMessages
      |> Ui.horizontal
      |> showConnectionButton
      |> Ui.button "Settings" SettingsWindow.openWindow
      |> Ui.horizontalEnd
      |> logAllEntries
      |> State.save

    member self.Awake () =
      Catcher.listen ()

    member self.OnDestroy () =
      Catcher.silence ()

  [<MenuItem("Window/LogCatcher")>]
  let openWindow () =
    EditorWindow.GetWindow<LogCatcherWindow> (false, "Log Catcher", true)
