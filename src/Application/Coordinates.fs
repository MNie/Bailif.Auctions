module Application.Coordinates
open FSharp.Data
open HttpFs.Client
open Hopac
open System.Threading.Tasks

type CoordinatesResponse = JsonProvider<"""
[
  {
    "place_id": 101573834,
    "licence": "Data © OpenStreetMap contributors, ODbL 1.0. https://osm.org/copyright",
    "osm_type": "way",
    "osm_id": 111621751,
    "boundingbox": [
      "54.3054945",
      "54.3057362",
      "18.5823766",
      "18.5826803"
    ],
    "lat": "54.30561535",
    "lon": "18.58252845",
    "display_name": "2, Piłkarska, Osiedle Cztery Pory Roku, Orunia Górna-Gdańsk Południe, Gdańsk, województwo pomorskie, 80-180, Polska",
    "class": "building",
    "type": "yes",
    "importance": 0.5309999999999999
  }
]
""">

type Coordinate = decimal

type Point =
    {
        longitude: Coordinate
        latitude: Coordinate
    }

module Coordinates =
    let internal parseResponse (res: string) =
        CoordinatesResponse.Parse res
        |> Array.tryHead
        |> fun x ->
            match x with
            | Some v -> Some { longitude = v.Lon; latitude = v.Lat }
            | _ -> None
    
    let translateAddressToCoords (address) =
        async {
            let request =
                Request.createUrl Get (sprintf "https://nominatim.openstreetmap.org/search?q=%s&format=json" address)
                |> Request.setHeader (Accept "application/json")
                |> Request.setHeader (UserAgent "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.79 Safari/537.36")
                |> Request.responseAsString
                |> run
            do! Task.Delay (1000) |> Async.AwaitTask
            return request |> parseResponse
        }