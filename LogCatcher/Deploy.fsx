#r "FSharp.Compiler.Interactive.Settings.dll"

open System.IO

printfn "%s" "Copying output"

let copyBinary (origin, destination) =
  File.Copy (origin, destination, true)
  printfn "Copied file to: %s" destination

let fullPath file dir =
  sprintf "%s%s" dir file

let fullOriginAndDestinationPath file origin destination =
  (fullPath file origin, fullPath file destination)

let copyBinaries targetDir =
  let releaseDir = Path.GetFullPath ("bin/Release/netcoreapp3.1/")
  let solutionRoot = Path.GetFullPath ("../")

  let binarySettings = [
    ("LogCatcher.dll", releaseDir);
    ("Utilities.dll", releaseDir);
    ("FSharp.Core.dll", sprintf "%sExternal/" solutionRoot);
  ]

  List.map ((fun (file, origin) ->
    fullOriginAndDestinationPath file origin targetDir
  ) >> copyBinary) binarySettings
  |> ignore

match fsi.CommandLineArgs with
| [|_fileName; destination|] ->
  copyBinaries destination
| args ->
  printf "%s" "Invalid arguments:\n"
  Array.map (printf "%s\n") args
  |> ignore
