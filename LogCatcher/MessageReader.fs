namespace UnityLogCatcher.LogCatcher

open UnityEngine
open UnityEditor

module MessageReader =
  let private readOne state message =
    match message with
    | "Recompile" ->
      Debug.Log "Recompiling!"
      AssetDatabase.Refresh ()
      Debug.Log "Done recompiling!"
      state
    | _ ->
      state

  let read (messages : string list) (state : State) =
    List.fold readOne state messages