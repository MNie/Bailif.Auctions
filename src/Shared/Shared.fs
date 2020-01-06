namespace Shared

type Prize = decimal
type Link = string
type Info = string
type Address = string
type Date = System.DateTime
type Coordinate = decimal

type Localization =
    {
        longitude: Coordinate
        latitude: Coordinate
    }

type Auction =
    {
        prize: Prize
        link: Link
        description: Info
        ``when``: Date
        localization: Localization
    }

module Route =
    /// Defines how routes are generated on server and mapped from client
    let builder typeName methodName =
        sprintf "/api/%s/%s" typeName methodName

/// A type that specifies the communication protocol between client and server
/// to learn more, read the docs at https://zaid-ajaj.github.io/Fable.Remoting/src/basics.html
type IAuctionsApi =
    { 
        init : unit -> Async<Result<Auction seq, exn>>
        filtered : string -> Async<Result<Auction seq, exn>>
    }

