namespace UnityLogCatcher.LogCatcher

type LogEntries = list<LogEntry>

module LogEntries =
  let empty = ([] : list<LogEntry>)

  let mapErrors predicate entries =
    List.map (LogEntry.mapError predicate) entries

  let mapWarnings predicate entries =
    List.map (LogEntry.mapWarning predicate) entries

  let mapLogs predicate entries =
    List.map (LogEntry.mapLog predicate) entries
