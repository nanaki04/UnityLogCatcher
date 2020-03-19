namespace UnityLogCatcher.LogCatcher

open UnityEngine
open UnityEditor
open UnityLogCatcher.Utilities.Utility

module MainWindow =

  let logAllEntries state =
    state.Logs
    |> List.fold (fun acc logEntry -> Ui.label (LogEntry.unwrap logEntry) acc) state

  type LogCatcherWindow () =
    inherit EditorWindow ()

    member self.OnGUI () =
      State.load ()
      |> Ui.button "Settings" SettingsWindow.openWindow
      |> logAllEntries
      |> State.save

    member self.Awake () =
      Catcher.listen ()

    member self.OnDestroy () =
      Catcher.silence ()

  [<MenuItem("Window/LogCatcher")>]
  let openWindow () =
    EditorWindow.GetWindow<LogCatcherWindow> (false, "Log Catcher", true)
