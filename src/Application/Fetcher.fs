module Application.Fetcher

open HttpFs.Client
open Hopac

module internal Fetcher =
    let fetchHtml (city) =
        let request =
            Request.createUrl Post "http://www.licytacje.komornik.pl/Notice/Search"
            |> Request.setHeader (Accept "application/json")
            |> Request.body (BodyForm 
                [
                    NameValue ("Type", "1")
                    NameValue ("City", city)
                ])
            |> Request.responseAsString
            |> run
        request
        
    let fetchAuction link =
        let request =
            Request.createUrl Get link
            |> Request.setHeader (Accept "application/json")
            |> Request.responseAsString
            |> run
        request