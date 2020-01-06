module Application.Fetcher

open canopy.classic
open canopy.types
open OpenQA.Selenium.Chrome

module internal Fetcher =
    let private search () =
        (elements ".city").Length > 0

    let private startChrome () =
        let chromeOptions = ChromeOptions()
        chromeOptions.AddArgument("--no-sandbox")
        chromeOptions.AddArgument("--headless")
        let chromeNoSandbox = ChromeWithOptions(chromeOptions)
        start chromeNoSandbox

    let fetchHtml (city) =
        startChrome ()
        
        url "http://www.licytacje.komornik.pl/Notice/Search"
        waitFor search
        ".city" << city
        click "#Type"
        click "Nieruchomość"
        click ".button_next_active"

        let page = browser.PageSource
        quit ()

        page
        
    let fetchAuctions links =
        startChrome ()

        let rec fetchDetails alreadyFetched toCheck =
            match toCheck with
            | [] -> alreadyFetched
            | head::tail ->
                url head
                let page = browser.PageSource
                let v = (head, page)
                fetchDetails (v::alreadyFetched) tail

        let result = fetchDetails [] links        
        quit ()

        dict result