module Server

open Saturn

let endpointPipe = pipeline {
    plug head
    plug requestId
}

let app = application {
    pipe_through endpointPipe

    use_router Router.appRouter
    url "http://0.0.0.0:8085/"
    memory_cache
    use_static "static"
    use_gzip
}

[<EntryPoint>]
let main _ =
    printfn "Working directory - %s" (System.IO.Directory.GetCurrentDirectory())
    run app
    0 // return an integer exit code
