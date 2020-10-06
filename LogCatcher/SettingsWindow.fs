namespace UnityLogCatcher.LogCatcher

open UnityEngine
open UnityEditor
open UnityLogCatcher.Utilities.Utility

module SettingsWindow =

  type SettingsWindow () =
    inherit EditorWindow ()

    member self.OnGUI () =
      State.load ()
      |> Ui.input "Server ip adress:" State.ip State.withIp
      |> Ui.intInput "Server port:" State.port State.withPort
      |> Ui.button "Ok" (fun state ->
        State.persist state
        self.Close ()
        state
      )
      |> State.save

  let openWindow state =
    EditorWindow.GetWindow<SettingsWindow> (true, "Log Catcher Settings", true)
    |> select state
