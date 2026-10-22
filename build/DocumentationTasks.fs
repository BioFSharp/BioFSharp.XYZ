module DocumentationTasks

open Helpers
open ProjectInfo
open BasicTasks

open BlackFox.Fake


let restoreTools = BuildTask.create "RestoreTools" [] {
    runDotNet "tool restore" "./"
}

let buildDocs = BuildTask.create "BuildDocs" [restoreTools; buildSolution] {
    printfn "building docs with stable version %s" stableVersionTag
    runDotNet 
        (sprintf "fsdocs build --eval --clean --properties Configuration=%s --parameters fsdocs-package-version %s" configuration stableVersionTag)
        "./"
}

let buildDocsPrerelease = BuildTask.create "BuildDocsPrerelease" [restoreTools; setPrereleaseTag; buildSolution] {
    printfn "building docs with prerelease version %s" prereleaseTag
    runDotNet 
        (sprintf "fsdocs build --eval --clean --properties Configuration=%s --parameters fsdocs-package-version %s" configuration prereleaseTag)
        "./"
}

let watchDocs = BuildTask.create "WatchDocs" [restoreTools; buildSolution] {
    printfn "watching docs with stable version %s" stableVersionTag
    runDotNet 
        (sprintf "fsdocs watch --eval --clean --properties Configuration=%s --parameters fsdocs-package-version %s" configuration stableVersionTag)
        "./"
}

let watchDocsPrerelease = BuildTask.create "WatchDocsPrerelease" [restoreTools; setPrereleaseTag; buildSolution] {
    printfn "watching docs with prerelease version %s" prereleaseTag
    runDotNet 
        (sprintf "fsdocs watch --eval --clean --properties Configuration=%s --parameters fsdocs-package-version %s" configuration prereleaseTag)
        "./"
}
