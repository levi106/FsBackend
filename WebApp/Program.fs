open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Http
open Giraffe
open FSharp.Control.Tasks
open Microsoft.Extensions.Logging

let webApp =
    GET >=> route "/api" >=> (fun next ctx ->
        task {
            let logger = ctx.GetLogger()
            ctx.Request.Headers
            |> Seq.iter (fun x -> logger.Log(LogLevel.Information, $"{x.Key}: {x.Value}"))
            let number = 42
            let! counter = task { return number }
            return! Successful.OK counter next ctx
        })

let configureApp (app : IApplicationBuilder) =
    app.UseGiraffe webApp

let configureServices (services : IServiceCollection) =
    services.AddGiraffe() |> ignore

[<EntryPoint>]
let main _ =
    Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(fun webHost ->
            webHost
                .Configure(configureApp)
                .ConfigureServices(configureServices)
                |> ignore)
        .Build()
        .Run()
    0