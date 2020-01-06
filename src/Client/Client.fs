module Client

open Elmish
open Elmish.React
open Fable.FontAwesome
open Fable.FontAwesome.Free
open Fable.React
open Fable.Core
open Fable.React.Props
open Fulma
open Thoth.Json
open Leaflet
open Fable.Core.JsInterop
open Shared

module RL = ReactLeaflet

importAll "../../node_modules/leaflet/dist/leaflet.css"

Leaflet.icon?Default?imagePath <- "//cdnjs.cloudflare.com/ajax/libs/leaflet/1.3.1/images/"

// The model holds data that you want to keep track of while the application is running
// in this case, we are keeping track of a counter
// we mark it as optional, because initially it will not be available from the client
// the initial value will be requested from server
type Model = { Search: string; Auctions: Auction seq; Zoom: LatLngExpression}

// The Msg type defines what events/actions can occur while the application is running
// the state of the application changes *only* in reaction to these events
type Msg =
    | Search of string
    | SearchChanged of string
    | Filtered of Result<Auction seq, exn>
    | Init of Result<Auction seq, exn>
    | Error of exn

module Server =

    open Shared
    open Fable.Remoting.Client

    /// A proxy you can use to talk to server directly
    let api : IAuctionsApi =
      Remoting.createApi()
      |> Remoting.withRouteBuilder Route.builder
      |> Remoting.buildProxy<IAuctionsApi>
let initialize = Server.api.init
let search = Server.api.filtered

// defines the initial state and initial command (= side-effect) of the application
let init () : Model * Cmd<Msg> =
    let initialModel = { Search = ""; Auctions = Seq.empty; Zoom = (Fable.Core.U3.Case3 (54.425, 18.59)) }
    let loadCountCmd =
        Cmd.ofAsync initialize () Init Error
    initialModel, loadCountCmd

// The update function computes the next state of the application based on the current state and the incoming events/messages
// It can also run side-effects (encoded as commands) like calling the server via Http.
// these commands in turn, can dispatch messages to which the update function will react.
let update (msg : Msg) (currentModel : Model) : Model * Cmd<Msg> =
    match currentModel.Auctions, msg with
    | _, Search city ->
        let search = Cmd.ofAsync search (city) Filtered Error
        currentModel, search
    | _, SearchChanged city ->
        let nextModel = { currentModel with Search = city }
        nextModel, Cmd.none
    | _, Filtered possibleAuctions ->
        match possibleAuctions with
        | Ok auc ->
          let zoomToFirst = 
            auc 
            |> Seq.tryHead 
            |> fun x -> 
              match x with 
              | Some a -> (Fable.Core.U3.Case3 (a.localization.latitude |> float, a.localization.longitude |> float)) 
              | None -> currentModel.Zoom  
          let nextModel = { currentModel with Auctions = auc; Zoom = zoomToFirst }
          nextModel, Cmd.none
        | Result.Error e -> currentModel, Cmd.ofMsg (Error e)
    | _, Init possibleAuction ->
        match possibleAuction with
        | Ok auc ->
          let nextModel = { currentModel with Auctions = auc }
          nextModel, Cmd.none
        | Result.Error e -> currentModel, Cmd.ofMsg (Error e)        
    | _, Error e -> 
        JS.console.log(sprintf "%s%s%s" e.Message " " e.StackTrace)
        currentModel, Cmd.none      

let navBrand =
    Navbar.Brand.div [ ]
        [ Navbar.Item.a
            [ Navbar.Item.Props [ Href "https://mnie.me/" ]
              Navbar.Item.IsActive true ]
            [ img [ Src "/mnie.png"
                    Alt "Logo" ] ] ]

let navMenu =
    Navbar.menu [ ]
        [ Navbar.End.div [ ]
            [ Navbar.Item.div [ ]
                [ Button.a
                    [ Button.Color IsWhite
                      Button.IsOutlined
                      Button.Size IsSmall
                      Button.Props [ Href "https://github.com/MNie/Bailif.Auctions" ] ]
                    [ Icon.icon [ ]
                        [ Fa.i [Fa.Brand.Github; Fa.FixedWidth] [] ]
                      span [ ] [ str "View Source" ] ] ] ] ]

let containerBox (model : Model) (dispatch : Msg -> unit) =
    Box.box' [ ]
        [ Field.div [ Field.IsGrouped ]
            [ Control.p [ Control.IsExpanded ]
                [ Input.text
                    [ Input.Disabled false
                      Input.Value model.Search
                      Input.OnChange (fun e -> dispatch (SearchChanged e.Value)) ] ]
              Control.p [ ]
                [ Button.a
                    [ Button.Color IsPrimary
                      Button.OnClick (fun _ -> dispatch (Search model.Search)) ]
                    [ str "Search" ] ] ] ]

let buildMarker (auction: Auction): ReactElement =
    RL.marker 
      [ 
        RL.MarkerProps.Position (Fable.Core.U3.Case3 (auction.localization.latitude |> float, auction.localization.longitude |> float)) ] 
      [ RL.popup 
          [ RL.PopupProps.Key (auction.link |> string)]
          [ Control.p 
              [] 
              [ label [] [ !!auction.description ] ]
            Control.p 
                [] 
                [ Button.a
                    [ Button.Size IsSmall
                      Button.Props [ Href (auction.link |> string) ] ]
                    [ Icon.icon [ ]
                        [ Fa.i [Fa.Brand.Github; Fa.FixedWidth] [] ]
                      span [ ] [ str "Go to bailif description" ] ] ] ] ]    

let tile =
  RL.tileLayer 
    [ RL.TileLayerProps.Url "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
      RL.TileLayerProps.Attribution "&amp;copy <a href=&quot;http://osm.org/copyright&quot;>OpenStreetMap</a> contributors" ] 
    []

let mapElements model =
  let markers = model.Auctions |> Seq.map buildMarker |> Seq.toList
  tile :: markers    

let view (model : Model) (dispatch : Msg -> unit) =
    Hero.hero [ Hero.Color IsPrimary; Hero.IsFullHeight ]
        [ Hero.head [ ]
            [ Navbar.navbar [ ]
                [ Container.container [ ]
                    [ navBrand
                      navMenu ] ] ]

          Hero.body [ ]
            [ Container.container [ Container.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                [ Column.column
                    [ Column.Width (Screen.All, Column.Is9)
                      Column.Offset (Screen.All, Column.Is1) ]
                    [ Heading.p [ ]
                        [ str "Bailif Auctions" ]
                      RL.map [ RL.MapProps.Zoom 10.; RL.MapProps.Style [Height 500; MinWidth 200; Width Column.IsFull ]; RL.MapProps.Center model.Zoom ] 
                        (mapElements model)
                                              
                      containerBox model dispatch ] ] ] ]

#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif

Program.mkProgram init update view
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactBatched "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
