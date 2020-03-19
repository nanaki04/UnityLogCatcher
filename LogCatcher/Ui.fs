namespace UnityLogCatcher.LogCatcher

open UnityEngine
open UnityEditor
open UnityLogCatcher.Utilities.Utility

module Ui =

  let button (text : string) callback state =
    GUILayout.Button(text)
    |> function
      | true -> callback state
      | false -> state

  let input (label : string) findVal callback state =
    EditorGUILayout.TextField (label, ((findVal state) : string), (Array.empty : GUILayoutOption []))
    |> callback
    <| state

  let intInput (label : string) findVal callback state =
    EditorGUILayout.IntField (label, findVal state)
    |> callback
    <| state

  let label (text : string) state =
    EditorGUILayout.LabelField (text, (Array.empty : GUILayoutOption []))
    |> select state

  let horizontal state =
    EditorGUILayout.BeginHorizontal ()
    |> select state

  let horizontalEnd state =
    EditorGUILayout.EndHorizontal ()
    |> select state
