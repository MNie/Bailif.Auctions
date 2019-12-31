module CoordinatesTests
    open Application
    open Xunit
    open Swensen.Unquote
    open Application.Coordinates

    [<Fact>]
    let ``when parsing response from coordinates api, should return 54 for lat and 18 for lng``() =
        let input = """
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
    "lat": "54.312",
    "lon": "18.543",
    "display_name": "2, Piłkarska, Osiedle Cztery Pory Roku, Orunia Górna-Gdańsk Południe, Gdańsk, województwo pomorskie, 80-180, Polska",
    "class": "building",
    "type": "yes",
    "importance": 0.5309999999999999
  }
]
"""
        let result = Coordinates.parseResponse input
        result =! Some { longitude = 18.543m; latitude = 54.312m }