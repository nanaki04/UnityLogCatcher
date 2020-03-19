namespace UnityLogCatcher.Utilities

open System.Text.RegularExpressions

type IpAddress = string

module IpAddress =
  let empty = "0.0.0.0"

  let validate ip =
    Regex.IsMatch (ip, @"^\d+\.\d+\.\d+\.\d+$")

  let validateAndReplace ip =
    Regex.Replace (ip, @"[^\d\.]", "")
