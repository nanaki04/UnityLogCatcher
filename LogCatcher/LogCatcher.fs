namespace UnityLogCatcher.LogCatcher

open UnityEngine

module Catcher =
  let catch message stackTrace logType =
    match logType with
    | LogType.Error -> LogEntry.Error message
    | LogType.Warning -> Warning message
    | _ -> Log message
    |> State.pushLogEntry
    <| State.load ()
    |> State.save

  let logCallback =
    new Application.LogCallback (catch)

  let listen () =
    Application.add_logMessageReceived logCallback
  let silence () =
    Application.remove_logMessageReceived logCallback
