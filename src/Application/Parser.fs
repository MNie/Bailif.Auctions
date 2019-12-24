module Application.Parser

open System.Text.RegularExpressions
open FSharp.Data
open System
open Application.Coordinates
open System.Globalization

type Prize = decimal
type Link = System.Uri
type Info = string
type Address = string
type Date = DateTime

type internal BaseAuction =
    {
        prize: Prize
        ``when``: Date
        link: Link
    }
    
type internal Details =
    {
        description: Info
        address: Address
        point: Point
    }

module internal Parser =
    let private onlyNumbers = Regex("[^0-9,]+", RegexOptions.Compiled)
    let private baseUrl = "http://www.licytacje.komornik.pl"
   
    let parseHtml (html) =
        let doc = HtmlDocument.Parse html
        
        doc.CssSelect("table")
        |> List.tryHead
        |> fun x ->
            match x with
            | Some v ->
                v.Elements [ "tr" ]
                |> List.skip 1
                |> List.filter (fun y -> 
                    let nOfElements = y.Elements [ "td" ] |> List.length
                    nOfElements > 1
                )
                |> List.map (fun x ->
                    let columns = x.Elements [ "td" ]
                    let ``when`` = columns.[2] |> fun x -> DateTime.ParseExact(x.InnerText().Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture)
                    let (_, details) = Seq.foldBack (fun e (i, acc) -> (i - 1, if i <= 0 then acc else e :: acc)) columns (2, [])
                    let prize = details |> Seq.head |> fun x -> x.InnerText () |> fun x -> onlyNumbers.Replace(x, "")
                    let link = details |> Seq.last |> fun x -> x.Elements [ "a" ] |> Seq.head |> fun x -> x.Attribute "href" |> fun x -> x.Value
                    {
                        prize = (Decimal.Parse(prize.Replace(",", "."), CultureInfo.InvariantCulture))
                        link = (new System.Uri(sprintf "%s%s" baseUrl (link ())))
                        ``when`` = ``when``
                    }
                )
            | _ -> []
        
    let parseAuction (html) =
        let doc = HtmlDocument.Parse html
        
        let address =
            doc.CssSelect("input#hidden_address")
            |> List.head
            |> fun x -> x.Attribute "value"
            |> fun x -> x.Value ()
        let description = doc.CssSelect("div#Preview") |> List.head |> fun x -> x.InnerText ()
        async {
            let! coords = Coordinates.translateAddressToCoords address
            match coords with
            | Some c -> 
                return Some {
                    description = description
                    address = address
                    point = c
                }
            | _ -> return None
        }
