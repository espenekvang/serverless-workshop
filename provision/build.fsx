open Fake.DotNet
// Dependencies and FAKE-specific stuff
// ==============================================
#r "paket:
nuget Fake.Core.Target
nuget Fake.IO.FileSystem
nuget Fake.IO.Zip
nuget Fake.BuildServer.TeamFoundation
nuget Fake.Core.Environment
nuget Fake.Api.GitHub
nuget Fake.DotNet.Cli //"

// Use this for IDE support. Not required by FAKE 5.
#load ".fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.Core.TargetOperators
open Fake.IO
open Fake.IO.Globbing.Operators
open Fake.DotNet

let appName = "Serverless.Workshop"
let sourceDir = __SOURCE_DIRECTORY__
let artifactDir = sourceDir + "/artifacts"
let slnLocation = sourceDir + "\\..\\src\\Serverless.Workshop.sln"
let buildDir = sourceDir + "/build"

Directory.ensure buildDir
Directory.ensure artifactDir

let version =
    match BuildServer.buildServer with
    | TeamFoundation  -> BuildServer.buildVersion
    | LocalBuild -> "1.0.0-local"
    | _ -> Environment.environVarOrDefault "version" "1.0.0"

//------------------------------------------------------------------------------
// Targets
//------------------------------------------------------------------------------

Target.create "Clean" <|fun _ ->
    Shell.cleanDirs [buildDir; artifactDir]

Target.create "Build" <|fun _ -> 
    DotNet.publish (fun opt -> { 
        opt with             
            OutputPath = Some buildDir
            Configuration = DotNet.BuildConfiguration.Release
            }) slnLocation

Target.create "Artifact" <| fun _ ->
    let preZip = sprintf "%s/prezip" buildDir    
    let artifactFilename = sprintf "%s/%s.%s.zip" artifactDir appName version    
    
    Shell.copyDir preZip (sprintf "%s" buildDir) (fun _ -> true)   

    !! "provision/build/prezip/**/*.*"
    |> Zip.zip preZip artifactFilename

    Shell.copyFile artifactDir "provision/upload.cmd"
    Shell.copyFile artifactDir "provision/upload.ps1"

    let artifactDirArm = (artifactDir + "/arm/")
    Shell.copyDir artifactDirArm "provision/arm" (fun _ -> true)

//------------------------------------------------------------------------------
// Dependencies
//------------------------------------------------------------------------------
"Clean"
    ==> "Build"
    
"Build"
    ==> "Artifact"

Target.runOrDefault "Build"