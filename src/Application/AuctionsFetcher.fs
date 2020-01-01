namespace Application

open FSharp.Control
open Application.Fetcher
open Application.Coordinates
open Application.Parser
open canopy
open System

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

module Auctions =
    let get =
        configuration.chromeDir <- AppDomain.CurrentDomain.BaseDirectory

        Fetcher.fetchHtml
        >> Parser.parseHtml
        >> fun x ->
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
