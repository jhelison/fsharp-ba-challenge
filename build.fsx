#load ".fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.IO
open System.Threading

let serverPath = "./src/Server/" |> Path.getFullName
let serverProjectPath = Path.combine appPath "Server.fsproj"
let sharedPath = "./src/Shared/" |> Path.getFullName
let sharedProjectPath = Path.combine appPath "Shared.fsproj"


Target.create "Clean" ignore

Target.create "Restore" (fun _ ->
    DotNet.restore id projectPath
)

Target.create "Build" (fun _ ->
    DotNet.build id projectPath
)


Target.create "Run" (fun _ ->
  let server = async {
    DotNet.exec (fun p -> { p with WorkingDirectory = appPath } ) "watch" "run" |> ignore
  }
  let browser = async {
    Thread.Sleep 5000
    Process.start (fun i -> { i with FileName = "http://localhost:8085" }) |> ignore
  }

  [ server; browser]
  |> Async.Parallel
  |> Async.RunSynchronously
  |> ignore
)

"Clean"
  ==> "Restore"
  ==> "Build"

"Clean"
  ==> "Restore"
  ==> "Run"

Target.runOrDefault "Build"
