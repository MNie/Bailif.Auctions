namespace Application

open FSharp.Control
open Application.Fetcher
open Application.Coordinates
open Application.Parser
open canopy
open System
open Microsoft.Extensions.Caching.Memory

type internal Auction =
    {
        prize: Prize
        ``when``: Date
        link: Link
        details: Details
    }
    
type AuctionInformation =
    {
        prize: Prize
        link: Link
        description: Info
        ``when``: Date
        point: Point
    }

module Cache =
    let get<'a> (cache: Microsoft.Extensions.Caching.Memory.IMemoryCache) key =
        let result = cache.TryGetValue<'a> key
        match result with
        | true, value -> Ok value
        | _ -> Error "not found"

    let upsert<'a> (cache: Microsoft.Extensions.Caching.Memory.IMemoryCache) key (value: 'a) =
        let opt = MemoryCacheEntryOptions()
        opt.SlidingExpiration <- System.Nullable<TimeSpan> (TimeSpan.FromHours(1.))
        opt.Size <- System.Nullable<int64>(1L)

        cache.Set<'a>(key, value, opt)

module Auctions =
    let get (cache: Microsoft.Extensions.Caching.Memory.IMemoryCache) city =
        configuration.chromeDir <- AppDomain.CurrentDomain.BaseDirectory
        
        match Cache.get<AuctionInformation seq> cache city with
        | Ok existingData -> async { return existingData }
        | _ ->
            let data =
                Fetcher.fetchHtml city
                |> Parser.parseHtml
                |> fun x ->
                    let concreteAuctions = Fetcher.fetchAuctions (x |> List.map (fun y -> y.link.AbsoluteUri))
                    async {
                        let! result =
                            x
                            |> List.map (fun y ->
                                async {
                                    let auction = concreteAuctions.[ y.link.AbsoluteUri ]
                                    let! details = auction |> Parser.parseAuction
                                    match details with
                                    | Some d ->
                                        let info = sprintf "Prize: %M zł, property located near: %s, auction at: %s" y.prize d.address (y.``when``.ToString("dd/MM/yyyy"))
                                        return [{
                                            prize = y.prize
                                            link = y.link
                                            description = info
                                            point = d.point
                                            ``when`` = y.``when``
                                        }]
                                    | None -> return []
                                }
                            )
                            |> List.toSeq
                            |> Async.Parallel
                        return result |> Seq.collect id
                    }
            async {
                let! d = data
                return Cache.upsert cache city d
            }
                        
