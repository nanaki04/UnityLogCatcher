namespace UnityLogCatcher.LogCatcher

open UnityEngine
open UnityEditor
open UnityLogCatcher.Utilities.Utility

module SettingsWindow =

  type SettingsWindow () =
    inherit EditorWindow ()

    member self.OnGUI () =
      Debug.Log("spinning settings window")
      State.load ()
      |> Ui.input "Server ip adress:" State.ip State.withIp
      |> Ui.intInput "Server port:" State.port State.withPort
      |> State.save

  let openWindow state =
    EditorWindow.GetWindow<SettingsWindow> (true, "Log Catcher Settings", true)
    |> select state
