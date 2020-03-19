namespace UnityLogCatcher.LogCatcher

type LogEntry =
| Error of string
| Warning of string
| Log of string

module LogEntry =
  let mapError predicate entry =
    match entry with
    | Error log -> Error <| predicate log
    | _ -> entry

  let mapWarning predicate entry =
    match entry with
    | Warning log -> Warning <| predicate log
    | _ -> entry

  let mapLog predicate entry =
    match entry with
    | Error log -> Log <| predicate log
    | _ -> entry

  let unwrap entry =
    match entry with
    | Error log -> log
    | Warning log -> log
    | Log log -> log
