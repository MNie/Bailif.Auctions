open System.IO
open System.Threading.Tasks

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open FSharp.Control.Tasks.V2
open Giraffe
open Saturn
open Shared
open Application

open Fable.Remoting.Server
open Fable.Remoting.Giraffe

let tryGetEnv = System.Environment.GetEnvironmentVariable >> function null | "" -> None | x -> Some x

let publicPath = Path.GetFullPath "../Client/public"

let port =
    "SERVER_PORT"
    |> tryGetEnv |> Option.map uint16 |> Option.defaultValue 8085us

let mapToContract (ai: AuctionInformation): Shared.Auction =
    {
        prize = ai.prize
        link = ai.link.AbsoluteUri
        description = ai.description
        ``when`` = ai.``when``.Date
        localization = {
            longitude = ai.point.longitude
            latitude = ai.point.latitude
        }    
    }

let auctionsApi = {
    init = fun () -> async {
        let! auctions = Auctions.get ("Gdańsk")
        return auctions
            |> Seq.map mapToContract
    }

    filtered = fun (city) -> async {
        let! auctions =
            match city with
            | c when String.isEmpty c -> Auctions.get ("Gdańsk")
            | _ -> Auctions.get (city)
        return auctions
            |> Seq.map mapToContract
    }
}

let webApp =
    Remoting.createApi()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.fromValue auctionsApi
    |> Remoting.buildHttpHandler

let app = application {
    url ("http://0.0.0.0:" + port.ToString() + "/")
    use_router webApp
    memory_cache
    use_static publicPath
    use_gzip
}

run app
