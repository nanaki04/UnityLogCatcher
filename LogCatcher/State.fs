namespace UnityLogCatcher.LogCatcher

open UnityLogCatcher.Utilities
open UnityEngine
open UnityEditor

type State = {
  ServerAddress : ServerAddress;
  Connection : TcpConnection.Connection option
  Logs : LogEntries;
}

module State =
  let initial =
    {
      ServerAddress = ServerAddress.empty;
      Connection = None;
      Logs = LogEntries.empty;
    }

  let import () =
    let ip =
      if (EditorPrefs.HasKey("log_catcher_settings_ip"))
      then EditorPrefs.GetString ("log_catcher_settings_ip")
      else IpAddress.empty

    let port =
      if (EditorPrefs.HasKey("log_catcher_settings_port"))
      then EditorPrefs.GetInt("log_catcher_settings_port")
      else Port.empty

    {
      initial
      with ServerAddress = {
        Ip = ip;
        Port = port;
      }
    }

  let persist state =
    EditorPrefs.SetString ("log_catcher_settings_ip", state.ServerAddress.Ip)
    EditorPrefs.SetInt ("log_catcher_settings_port", state.ServerAddress.Port)

  let mutable internalState = import ()

  let save state =
    internalState <- state

  let load () = internalState

  let withIp ip state =
    { state with ServerAddress = ServerAddress.withIp ip state.ServerAddress }

  let withPort port state =
    { state with ServerAddress = ServerAddress.withPort port state.ServerAddress }

  let withLogs logs state =
    { state with Logs = logs }

  let withConnection connection state =
    { state with Connection = Some connection }

  let withoutConnection state =
    { state with Connection = None }

  let ip state =
    ServerAddress.ip state.ServerAddress

  let port state =
    ServerAddress.port state.ServerAddress

  let pushLogEntry logEntry state =
    { state with Logs = logEntry::state.Logs }
