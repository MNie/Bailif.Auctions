open System.IO

open Saturn
open Shared
open Application

open Fable.Remoting.Server
open Fable.Remoting.Giraffe

open Microsoft.Extensions.Caching.Memory
open System

let tryGetEnv = System.Environment.GetEnvironmentVariable >> function null | "" -> None | x -> Some x

let publicPath = Path.GetFullPath "../Client/public"

let cacheOptions = 
    let opt = MemoryCacheOptions()
    opt.SizeLimit <- System.Nullable<int64>(100L)
    opt.ExpirationScanFrequency <- TimeSpan.FromSeconds(5.)
    opt

let cache = new MemoryCache(cacheOptions)

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

let getAuctions = Auctions.get cache

let auctionsApi = {
    init = fun () -> async {
        try
            let! auctions = getAuctions ("Gdańsk")
            let mapped = auctions |> Seq.map mapToContract
            return Ok mapped            
        with
        | ex -> return Error ex          
    }

    filtered = fun (city) -> async {
        try
            let! auctions =
                match city with
                | c when String.isEmpty c -> getAuctions ("Gdańsk")
                | _ -> getAuctions (city)
            return auctions
                |> Seq.map mapToContract
                |> Ok
        with
        | ex -> return Error ex
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
