namespace UnityLogCatcher.LogCatcher

open UnityLogCatcher.Utilities

type State = {
  ServerAddress : ServerAddress;
  Logs : LogEntries;
}

module State =
  let initial =
    {
      ServerAddress = ServerAddress.empty;
      Logs = LogEntries.empty;
    }

  let mutable internalState = initial

  let load () = internalState

  let save state = internalState <- state

  let withIp ip state =
    { state with ServerAddress = ServerAddress.withIp ip state.ServerAddress }

  let withPort port state =
    { state with ServerAddress = ServerAddress.withPort port state.ServerAddress }

  let withLogs logs state =
    { state with Logs = logs }

  let ip state =
    ServerAddress.ip state.ServerAddress

  let port state =
    ServerAddress.port state.ServerAddress

  let pushLogEntry logEntry state =
    { state with Logs = logEntry::state.Logs }
