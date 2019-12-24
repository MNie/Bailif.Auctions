namespace Application

open FSharp.Control
open Application.Fetcher
open Application.Coordinates
open Application.Parser

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
    let private getConcreteAuction (auction: BaseAuction) =
        Fetcher.fetchAuction auction.link.AbsoluteUri
        |> Parser.parseAuction
    
    let get =
        Fetcher.fetchHtml
        >> Parser.parseHtml
        >> fun x ->
            async {
                let! result =
                    x
                    |> List.map (fun y ->
                        async {
                            let! details = getConcreteAuction (y)
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
