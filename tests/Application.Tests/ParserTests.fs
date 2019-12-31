module ParserTests
    open Application
    open System
    open Xunit
    open Swensen.Unquote
    open Application.Parser
    
    let auctionsHtml = """
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="description" content="Strona internetowa zawiera obwieszczenia o planowanych przez komorników sądowych licytacjach nieruchomości i ruchomości." />
    <meta name="keywords" 
        content="aukcje komornicze, komornik licytacje, licytacje komornik, krajowa rada komornicza, przetargi komornicze, komornik, licytacje, licytacja komornicza, komornik.pl, licytacje komornicze samochody,
            krajowa izba komornicza, licytację komornicze, komornicze licytacje, licytacje komornicze wrocław, domy, garaże, miejsca postojowe, grunty, lokale użytkowe, magazyny i hale, mieszkania,
            nieruchomości, statki morskie, antyki, sztuka, biżuteria, zegarki, łodzie, jachty, maszyny przemysłowe, maszyny rolnicze, materiały przemysłowe, meble, motocykle, skutery, quady,
            odzież, pozostałe ruchomości, przyczepy, naczepy, samochody ciężarowe, samochody osobowe, sprzęt agd, sprzęt komputerowy, sprzęt rtv, surowce, aukcja, obwieszczenie,
            obwieszczenie komornika, obwieszczenie komornicze, obwieszczenia, obwieszczenia komorników, licytacja nieruchomości, licytacja ruchomości, 
            Egzekucja przez sprzedaż przedsiębiorstwa lub gospodarstwa rolnego, Egzekucja ze statków morskich" />
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <title>Wyniki wyszukiwania obwieszczeń o licytacjach | Obwieszczenia o licytacjach</title>
    
</head>
<body>

  

  <!-- HEADER -->
  <a id="top-region"></a>
  <div id="header-wrapper" >
    <div id="header-container" class="container">
      <div id="header-logo">
        <a href="/"><img src="/Images/logo.jpg" alt="Logo" /></a>
      </div>

      <div id="top-elements">    
        <div id="header-social-icons">
          
    <div id="login-show" style="cursor:pointer; font-size:14px;">STREFA DLA KOMORNIKÓW SĄDOWYCH <img src="../../Images/loginexpand.png"/></div>
    <div class="normal-button-logging" id="div-login" style="display:none;">
        <a href="/Account/LogOn">Logowanie</a>
        | <a href="/Account/Register">Rejestracja</a>
        | <a href="/Account/ResetPassword">Reset hasła</a>
        | <img id="login-hide" src="../../Images/loginclose.png" style="position: relative; top: 1px; opacity:1; background:Transparent; cursor:pointer;"/>
    </div>

        </div>
      </div>
      <div id="navigation-container">
        <div style="float: right;">
            <ul class="sf-menu">
                <li class="current"><a href="/Home/Index">Gł&#243;wna</a></li>
                <li class="current"><a href="/Notice/Search">Wyszukaj</a></li>
                <li class="current"><a href="/Home/Newsletter">Subskrypcja</a></li>
                <li><a href="/Home/ContactUs">Kontakt</a></li>
            </ul>
        </div>
      </div> 
    </div> 
  </div>
  <div id="header-wrapper2"></div>
  <div id="inner-page-wrapper"> 
   	<div id="inner-page-container" class="container vertical-space-pusher">
        <div id="sidebar" class="one_fourth"> 
            <div class="extra-space-bottom">
                <div>
                    <a href="/Notice/Filter/-3"><h6 class="linksMain" style="font-family: 'Lucida Sans Unicode', 'Lucida Grande', sans-serif;">Wszystkie nieruchomości</h6></a>
                </div>
                <div class="styled-list">
                    <ul>
                        <li><span></span><a href="/Notice/Filter/29">domy</a></li>
                        <li><span></span><a href="/Notice/Filter/27">garaże, miejsca postojowe</a></li>
                        <li><span></span><a href="/Notice/Filter/28">grunty</a></li>
                        <li><span></span><a href="/Notice/Filter/31">lokale użytkowe</a></li>
                        <li><span></span><a href="/Notice/Filter/32">magazyny i hale</a></li>
                        <li><span></span><a href="/Notice/Filter/30">mieszkania</a></li>
                        <li><span></span><a href="/Notice/Filter/34">nieruchomości</a></li>
                        <li><span></span><a href="/Notice/Filter/33">statki morskie</a></li>
                    </ul>
                </div>
                
                <div class="extra-space-bottom-header">
                    <a href="/Notice/Filter/-2"><h6 class="linksMain" style="font-family: 'Lucida Sans Unicode', 'Lucida Grande', sans-serif;">Wszystkie ruchomości</h6></a>
                </div>
                <div class="styled-list">
                    <ul>
                        <li><span></span><a href="/Notice/Filter/14">antyki, sztuka</a></li>
                    </ul>
                </div>

                <div class="extra-space-bottom-header">
                    <a href="/Decision"><h6 class="linksMain">Egzekucja przez sprzedaż przedsiębiorstwa lub gospodarstwa rolnego</h6></a>
                </div>
                <div class="styled-list">
                    <ul>
                        <li><a href="/Decision?type=1">postanowienie w zw. art. 1064 15 kpc</a></li>
                        <li><a href="/Decision?type=2">postanowienie w zw. art. 1064 19 kpc</a></li>
                    </ul>
                </div>
                
                <div class="extra-space-bottom-header">
                    <h6 class="linksMain">Opis i oszacowanie</h6>
                </div>
                <div class="styled-list">
                    <ul>
                        <li><a href="/Description/Published">Obwieszczenia o terminie opisu i oszacowania</a></li>
                    </ul>
                </div>

                <div class="extra-space-bottom-header">
                    <h6 class="linksMain">Egzekucja ze statków morskich</h6>
                </div>
                <div class="styled-list">
                    <ul>
                        <li><a href="/Declaration?type=1">ogłoszenia o wszczęciu egzekucji</a></li>
                    </ul>
                </div>
            </div>
            <div class="extra-space-bottom-header">
                <h6>Dokumenty do pobrania</h6>
                <a href="/File/PLKRK-IU.pdf">Instrukcja dla użytkownika</a><br />
                <a href="/File/PLKRK-IK.pdf">Instrukcja dla komornika</a>
            </div>
        </div>
        <div id="main-content" class="three_fourth last_column">
            


<h2>Wyniki wyszukiwania obwieszczeń o licytacjach</h2>

<p style="display:block">
    <a href="/Notice/Search">Powr&#243;t do wyszukiwania</a>
</p>

<fieldset id="SerachModelList" style="display:none">
 
				
				<form action="/Notice/Search" method="post">        <div style="background:#EBEBEB; border:1px solid #CCCCCC; color:Black;">
            <div style="width:100%;">
                <div style="float:left;">
                    <div class="editor-label2 topLabels" style="margin-right: 30px;width: 83px; margin-left:10px;">
                        <label for="Typ_mienia">Typ mienia</label>
                    </div>
                    <div class="editor-field" style="float: left;margin-right: 30px;">
<select class="auto-post" data-val="true" data-val-number="The field Type must be a number." id="Type" name="Type"><option value="">Wybierz typ mienia</option>
<option value="2">Ruchomość</option>
<option selected="selected" value="1">Nieruchomość</option>
</select>
                    </div>
                </div>
                <div id="GeneralCategory" style="float: left;margin-right: 30px;">
                    <div class="editor-field">
<select data-val="true" data-val-number="The field CategoryId must be a number." id="CategoryId" name="CategoryId"><option value="">Wybierz kategorię</option>
<option value="14">antyki, sztuka</option>

</select>
                    </div>
                </div>
            </div>
            <div style="clear: both; width:100%;">
                <div id="divMiasto" style="float:left;">
                    <div class="editor-label2 topLabels" style="margin-right: 18px; margin-left:10px;">
                        <label for="Data_i_miejsce">Data i miejsce</label>
                    </div>
                    <div class="editor-field" style="float:left;margin-right: 30px;">
                        <input autocomplete="off" class="city" data-val="true" data-val-length="Nieodpowiednia długość (maks. 50)" data-val-length-max="50" id="City" name="City" placeholder="Wybierz miasto" style="width: 220px; height: 37px;padding-left:10px;" type="text" value="Gdańsk" /><br />
                        <span class="field-validation-valid" data-valmsg-for="City" data-valmsg-replace="true"></span>
                    </div>
                </div>
                <div style="float:left; margin-right: 30px;">
                    <div class="editor-field">
                        <div id="map-poland" style="display:none;z-index: 1000; position: absolute; background-color: #eee; border: 5px solid #bbb; top: 155px;">
                            <div style="float:right; cursor:pointer;z-index: 1001;position: relative;font-size: 18px;font-weight: bold;padding: 2px;" id="province-map-hide">X</div>
                            <ul class="poland">
                                                                <li class="pl1"><a href="#dolnoslaskie" province-id="1">dolnośląskie</a></li>
                                                                <li class="pl2"><a href="#kujawsko-pomorskie" province-id="17">kujawsko-pomorskie</a></li>
                                                                <li class="pl3"><a href="#lubelskie" province-id="18">lubelskie</a></li>
                                                                <li class="pl4"><a href="#lubuskie" province-id="19">lubuskie</a></li>
                                                                <li class="pl5"><a href="#lodzkie" province-id="20">ł&#243;dzkie</a></li>
                                                                <li class="pl6"><a href="#malopolskie" province-id="21">małopolskie</a></li>
                                                                <li class="pl7"><a href="#mazowieckie" province-id="22">mazowieckie</a></li>
                                                                <li class="pl8"><a href="#opolskie" province-id="23">opolskie</a></li>
                                                                <li class="pl9"><a href="#podkarpackie" province-id="25">podkarpackie</a></li>
                                                                <li class="pl10"><a href="#podlaskie" province-id="24">podlaskie</a></li>
                                                                <li class="pl11"><a href="#pomorskie" province-id="26">pomorskie</a></li>
                                                                <li class="pl12"><a href="#slaskie" province-id="27">śląskie</a></li>
                                                                <li class="pl13"><a href="#swietokrzyskie" province-id="28">świętokrzyskie</a></li>
                                                                <li class="pl14"><a href="#warminsko-mazurskie" province-id="29">warmińsko-mazurskie</a></li>
                                                                <li class="pl15"><a href="#wielkopolskie" province-id="30">wielkopolskie</a></li>
                                                                <li class="pl16"><a href="#zachodniopomorskie" province-id="16">zachodniopomorskie</a></li>

                            </ul>
                        </div>
                        <input autocomplete="off" id="tbx-province" name="tbx-province" placeholder="Wybierz województwo" style="width: 200px; height: 37px; padding-left:10px;" type="text" value="" />
                        <input data-val="true" data-val-number="The field ProvinceId must be a number." id="ProvinceId" name="ProvinceId" type="hidden" value="" />
                    </div>
                </div>
                <div style="float:left;">
                    <div class="editor-field" style="background: white; height: 39px; margin-top: 14px; width: 140px; border: 1px solid #ddd;">
                        <input autocomplete="off" class="date" data-val="true" data-val-regex="Niepoprawny format daty (dd.mm.rrrr)." data-val-regex-pattern="[0-9]{2}\.[0-9]{2}\.[0-9]{4}" id="AuctionsDate" name="AuctionsDate" placeholder="Data licytacji" style="width: 100px; margin: 0px 0px 0px 10px; border-width:0px;height: 15px; position: relative; top: -1px;" type="text" value="" /><br />
                        <span class="field-validation-valid" data-valmsg-for="AuctionsDate" data-valmsg-replace="true"></span>
                    </div>
                </div>
            </div>
            <br style="clear:both;" />
        </div>
        <div style="padding-top: 4px;">
            <div class="editor-label2 w250" style="margin-left:10px;">
                <label for="Tagi">Tagi</label>
                <div style="font-size:10px;">Tagi możesz rozdzielać przecinkiem</div>
            </div>
            <div class="editor-field">
                <input autocomplete="off" data-val="true" data-val-length="Nieodpowiednia długość (maks. 50)" data-val-length-max="50" id="Words" name="Words" style="width: 250px;" type="text" value="" /><br />
                <span class="field-validation-valid" data-valmsg-for="Words" data-valmsg-replace="true"></span>
            </div>
        </div>
        <div style="clear:both;">
            <div class="editor-label2 w250" style="margin-left:10px; margin-top: -5px;">
                <div style="color:Black; font-size:14px;">Cena wywoławcza</div>
                <div style="font-size:10px;">Przesuwaj suwakiem by określić minimalną i maksymalną cenę wywoławczą</div>
            </div>
            <div class="editor-field">
                <div id="slider-price" style="margin-left:266px;margin-bottom: 2px;"></div>
                <input autocomplete="off" class="price" id="PriceFrom" name="PriceFrom" style="width: 100px;" type="text" value="" />PLN - 
                <input autocomplete="off" class="price" id="PriceTo" name="PriceTo" style="width: 100px;" type="text" value="" />PLN<br />
                <span class="field-validation-valid" data-valmsg-for="PriceFrom" data-valmsg-replace="true"></span>
                <span class="field-validation-valid" data-valmsg-for="PriceTo" data-valmsg-replace="true"></span>
            </div>
        </div>
        <div style="margin-top:25px;">
            <div class="editor-label2 w250" style="margin-left:10px; margin-top: -7px;">
                <div style="color:Black; font-size:14px;">Liczba licytowanych przedmiotów</div>
                <div style="font-size:10px;">Przesuwaj suwakiem by określić minimalną i maksymalną liczbę licytowanych przedmiotów</div>
            </div>
            <div class="editor-field">
                <div id="slider-item-count" style="margin-left:266px;margin-bottom: 2px;"></div>
                <input autocomplete="off" class="number" data-val="true" data-val-regex="Wprowadzona wartość musi być liczbą." data-val-regex-pattern="^[0-9]+$" id="ItemMin" name="ItemMin" style="width: 100px;" type="text" value="" /> -
                <input autocomplete="off" class="number" data-val="true" data-val-regex="Wprowadzona wartość musi być liczbą." data-val-regex-pattern="^[0-9]+$" id="ItemMax" name="ItemMax" style="width: 100px;" type="text" value="" /><br />
                <span class="field-validation-valid" data-valmsg-for="ItemMax" data-valmsg-replace="true"></span>
                <span class="field-validation-valid" data-valmsg-for="ItemMin" data-valmsg-replace="true"></span>
            </div>
        </div>
        <hr style="margin:30px 0px;" />
        <div>
            <div style="float:left;">
                <div class="editor-label2" style="width: 109px; margin: 20px 140px 0px 10px;">
                    <label for="Kancelaria">Kancelaria</label>
                </div>
                <div class="editor-field" style="float: left;">
<select data-val="true" data-val-number="The field OfficeId must be a number." id="OfficeId" name="OfficeId" style="width: 527px; margin:0px;"><option value="">Wybierz kancelarię</option>
<option value="3142"></option>

</select>
                </div>
            </div> 
        </div> 
        <div style="clear:both;">
            <div style="float:left;">
                <div class="editor-label2" style="width: 174px; margin: 20px 74px 0px 10px;">
                    <label for="Data_publikacji_obwieszczenia_o_licytacji">Data publikacji obwieszczenia o licytacji</label>
                </div>
                <div id="divPublicDate" style="display:inline; margin-right:25px;">
                    od
                    <div class="editor-field" style="margin-left:10px; display:inline-block; background: white; height: 40px; margin-top: 16px; width: 220px; border: 1px solid #ddd;"> 
                        <input autocomplete="off" class="date" data-val="true" data-val-regex="Niepoprawny format daty (dd.mm.rrrr)." data-val-regex-pattern="[0-9]{2}\.[0-9]{2}\.[0-9]{4}" id="PublicationDateFrom" name="PublicationDateFrom" style="width: 170px; border-width:0px; margin-left:10px;height: 15px; position: relative; top: -1px;" type="text" value="" /><br />
                        <span class="field-validation-valid" data-valmsg-for="PublicationDateFrom" data-valmsg-replace="true"></span>
                    </div>
                </div>
            </div>
            <div style="float: left;display:inline;">
                do
                <div class="editor-field" style="margin-left:10px; display:inline-block; background: white; height: 40px; margin-top: 16px; width: 220px; border: 1px solid #ddd;">
                    <input autocomplete="off" class="date" data-val="true" data-val-regex="Niepoprawny format daty (dd.mm.rrrr)." data-val-regex-pattern="[0-9]{2}\.[0-9]{2}\.[0-9]{4}" id="PublicationDateTo" name="PublicationDateTo" style="width: 170px; border-width:0px; margin-left:10px;height: 15px; position: relative; top: -1px;" type="text" value="" /><br />
                    <span class="field-validation-valid" data-valmsg-for="PublicationDateTo" data-valmsg-replace="true"></span>
                </div>
            </div> 
        </div> 
        <div style="clear:both;">
            <div style="float:left;">
                <div class="editor-label2" style="margin-right: 74px;width: 174px; margin-left:10px;">
                    <label for="Data_rozpocz_cia_licytacji">Data rozpoczęcia licytacji</label>
                </div>
                <div id="divStartDate2" style="display:inline; margin-right:25px;">
                    od
                    <div class="editor-field" style="margin-left:10px; display:inline-block; background: white; height: 40px; margin-top: 16px; width: 220px; border: 1px solid #ddd;"> 
                        <input autocomplete="off" class="date" data-val="true" data-val-regex="Niepoprawny format daty (dd.mm.rrrr)." data-val-regex-pattern="[0-9]{2}\.[0-9]{2}\.[0-9]{4}" id="StartDateFrom" name="StartDateFrom" style="width: 170px; border-width:0px; margin-left:10px;height: 15px; position: relative; top: -1px;" type="text" value="" /><br />
                        <span class="field-validation-valid" data-valmsg-for="StartDateFrom" data-valmsg-replace="true"></span>
                    </div>
                </div>
            </div>
            <div style="float: left;display:inline;">
                do
                <div class="editor-field" style="margin-left:10px; display:inline-block; background: white; height: 40px; margin-top: 16px; width: 220px; border: 1px solid #ddd;">
                    <input autocomplete="off" class="date" data-val="true" data-val-regex="Niepoprawny format daty (dd.mm.rrrr)." data-val-regex-pattern="[0-9]{2}\.[0-9]{2}\.[0-9]{4}" id="StartDateTo" name="StartDateTo" style="width: 170px; border-width:0px; margin-left:10px;height: 15px; position: relative; top: -1px;" type="text" value="" /><br />
                    <span class="field-validation-valid" data-valmsg-for="StartDateTo" data-valmsg-replace="true"></span>
                </div>
            </div> 
        </div> 
        <div style="clear:both; position: relative; top: 20px;">
            <div class="editor-label2 w250" style="width: 168px; margin-left:10px; margin-top: -10px; margin-right:80px;">
                <div style="color:Black; font-size:14px;">Suma oszacowania</div>
                <div style="font-size:10px;">Przesuwaj suwakiem by określić minimalną i maksymalną sumę oszacowania</div>
            </div>
            <div class="editor-field">
                <div id="slider-sum" style="margin-left:265px;margin-bottom: 2px;"></div>
                <input autocomplete="off" class="price" data-val="true" data-val-regex="Wprowadzona wartość musi być liczbą." data-val-regex-pattern="^[0-9 ]+,[0-9][0-9]$" id="SumMin" name="SumMin" style="width: 100px;" type="text" value="" />PLN - 
                <input autocomplete="off" class="price" data-val="true" data-val-regex="Wprowadzona wartość musi być liczbą." data-val-regex-pattern="^[0-9 ]+,[0-9][0-9]$" id="SumMax" name="SumMax" style="width: 100px;" type="text" value="" />PLN<br />
                <span class="field-validation-valid" data-valmsg-for="SumMin" data-valmsg-replace="true"></span>
                <span class="field-validation-valid" data-valmsg-for="SumMax" data-valmsg-replace="true"></span>
            </div>
        </div>
        <div style="clear:both; position: relative; margin-top: 55px;">
            <div class="editor-label2 w250" style="width: 170px; margin-left:10px; margin-top: -10px; margin-right:79px;">
                <div style="color:Black; font-size:14px;">Czy przedmiot podlega podatkowi VAT?</div>
            </div>
            <div class="editor-field">
                <input id="radio-vat-true" type="radio" name="vat-radios" value="1" />
                <label for="radio-vat-true">Tak</label>
                <input id="radio-vat-false" type="radio" name="vat-radios" value="0" />
                <label for="radio-vat-false">Nie</label>
                <input id="radio-vat-both" type="radio" name="vat-radios" value="" />
                <label for="radio-vat-both">Nieważne</label>
                <input data-val="true" data-val-number="The field Vat must be a number." id="Vat" name="Vat" type="hidden" value="" />
            </div>
        </div>
        <div style="clear:both; position: relative; margin-top: 20px;">
            <div class="editor-label2 w250" style="width: 170px; margin-left:10px; margin-top: 5px; margin-right:79px;">
                <div style="color:Black; font-size:14px;">Typ licytacji</div>
            </div>
            <div class="editor-field">
                <input id="radio-typ-true" type="radio" name="typ-radios" value="1" />
                <label for="radio-typ-true">Pierwsza</label>
                <input id="radio-typ-false" type="radio" name="typ-radios" value="2" />
                <label for="radio-typ-false">Druga</label>
                <input id="radio-typ-both" type="radio" name="typ-radios" value="" />
                <label for="radio-typ-both">Obie</label>
                <input id="TypeOfAuction" name="TypeOfAuction" type="hidden" value="" />
            </div>
        </div>
        <div style="clear:both;"></div>
        <hr style="margin:30px 0px;" />
        <p>
            <button class="button_next_active">
                Wyszukaj
            </button>
        </p>
</form>
</fieldset>
<div style="display:block">
    <table class="wMax">
        <tr>
            <th style="width: 20px;">
                Lp.
            </th>
            <th width="50px">
                Fotografia
            </th>
            <th style="width: 80px;">
                <a href="/Notice/Search?sortOrder=DataLicytacji">Data licytacji</a>
            </th>
            <th>
                <a href="/Notice/Search?sortOrder=Kategoria">Kategoria</a>
            </th>
            <th>
                <a href="/Notice/Search?sortOrder=Nazwa">Nazwa</a>
            </th>
            <th>
                <a href="/Notice/Search?sortOrder=Miasto">Miasto</a>
                (<a href="/Notice/Search?sortOrder=Wojew%C3%B3dztwo">Wojew&#243;dztwo</a>)
            </th>
            <th style="width: 110px;">
                <a href="/Notice/Search?sortOrder=Cena">Cena wywołania</a>
            </th>
            <th style="width: 28px;">
            </th>
        </tr>
<tr>                        <td>
                            1
                        </td>
                        <td>
                                <center>
                                    <img src='../../Images/icons/category-icons/30.png' alt="brak zdjęcia" />
                                </center>
                        </td>
                        <td>
                            12.12.2019
                        </td>
                        <td>
                            mieszkania
                        </td>
                        <td>
                                <div>lokal mieszkalny</div>
                        </td>
                        <td>
                            Gdańsk
                            <br />
                            (pomorskie)
                        </td>
                        <td style="text-align: right">
                            138&#160;750,00 zł
                        </td>
                        <td>
                            <a href="/Notice/Details/491902">
                                <div style = "background: url('../../Images/icons/application-iconset/32/Settings.png');center left;display:block; height:28px; width:28px;float:left;"></div>
                                <div>Więcej</div>
                            </a>
                        </td>
</tr><tr>                        <td>
                            2
                        </td>
                        <td>
                                <center>
                                    <img src='../../Images/icons/category-icons/30.png' alt="brak zdjęcia" />
                                </center>
                        </td>
                        <td>
                            12.12.2019
                        </td>
                        <td>
                            mieszkania
                        </td>
                        <td>
                                <div>nieruchomości</div>
                        </td>
                        <td>
                            Gdańsk
                            <br />
                            (pomorskie)
                        </td>
                        <td style="text-align: right">
                            140&#160;021,25 zł
                        </td>
                        <td>
                            <a href="/Notice/Details/494476">
                                <div style = "background: url('../../Images/icons/application-iconset/32/Settings.png');center left;display:block; height:28px; width:28px;float:left;"></div>
                                <div>Więcej</div>
                            </a>
                        </td>
</tr><tr>                        <td>
                            3
                        </td>
                        <td>
                                <center>
                                    <img src='../../Images/icons/category-icons/30.png' alt="brak zdjęcia" />
                                </center>
                        </td>
                        <td>
                            12.12.2019
                        </td>
                        <td>
                            mieszkania
                        </td>
                        <td>
                                <div>lokal mieszkalny</div>
                        </td>
                        <td>
                            GDAŃSK
                            <br />
                            (pomorskie)
                        </td>
                        <td style="text-align: right">
                            126&#160;006,75 zł
                        </td>
                        <td>
                            <a href="/Notice/Details/494944">
                                <div style = "background: url('../../Images/icons/application-iconset/32/Settings.png');center left;display:block; height:28px; width:28px;float:left;"></div>
                                <div>Więcej</div>
                            </a>
                        </td>
</tr><tr>                        <td>
                            4
                        </td>
                        <td>
                                <center>
                                    <img src='../../Images/icons/category-icons/31.png' alt="brak zdjęcia" />
                                </center>
                        </td>
                        <td>
                            12.12.2019
                        </td>
                        <td>
                            lokale użytkowe
                        </td>
                        <td>
                                <div>nieruchomość</div>
                        </td>
                        <td>
                            Gdańsk
                            <br />
                            (pomorskie)
                        </td>
                        <td style="text-align: right">
                            197&#160;680,50 zł
                        </td>
                        <td>
                            <a href="/Notice/Details/494475">
                                <div style = "background: url('../../Images/icons/application-iconset/32/Settings.png');center left;display:block; height:28px; width:28px;float:left;"></div>
                                <div>Więcej</div>
                            </a>
                        </td>
</tr><tr>                        <td>
                            5
                        </td>
                        <td>
                                <center>
                                    <img src='../../Images/icons/category-icons/30.png' alt="brak zdjęcia" />
                                </center>
                        </td>
                        <td>
                            13.12.2019
                        </td>
                        <td>
                            mieszkania
                        </td>
                        <td>
                                <div>mieszkanie</div>
                        </td>
                        <td>
                            Gdańsk
                            <br />
                            (pomorskie)
                        </td>
                        <td style="text-align: right">
                            231&#160;750,00 zł
                        </td>
                        <td>
                            <a href="/Notice/Details/491787">
                                <div style = "background: url('../../Images/icons/application-iconset/32/Settings.png');center left;display:block; height:28px; width:28px;float:left;"></div>
                                <div>Więcej</div>
                            </a>
                        </td>
</tr><tr>                        <td>
                            6
                        </td>
                        <td>
                                <center>
                                    <img src='../../Images/icons/category-icons/31.png' alt="brak zdjęcia" />
                                </center>
                        </td>
                        <td>
                            09.01.2020
                        </td>
                        <td>
                            lokale użytkowe
                        </td>
                        <td>
                                <div>lokal stanowiący odrębną nieruchomość</div>
                        </td>
                        <td>
                            Gdańsk
                            <br />
                            (pomorskie)
                        </td>
                        <td style="text-align: right">
                            223&#160;720,50 zł
                        </td>
                        <td>
                            <a href="/Notice/Details/492855">
                                <div style = "background: url('../../Images/icons/application-iconset/32/Settings.png');center left;display:block; height:28px; width:28px;float:left;"></div>
                                <div>Więcej</div>
                            </a>
                        </td>
</tr><tr>                        <td>
                            7
                        </td>
                        <td>
                                <center>
                                    <img src='../../Images/icons/category-icons/30.png' alt="brak zdjęcia" />
                                </center>
                        </td>
                        <td>
                            09.01.2020
                        </td>
                        <td>
                            mieszkania
                        </td>
                        <td>
                                <div>lokal mieszkalny</div>
                        </td>
                        <td>
                            Gdańsk
                            <br />
                            (pomorskie)
                        </td>
                        <td style="text-align: right">
                            319&#160;500,00 zł
                        </td>
                        <td>
                            <a href="/Notice/Details/495021">
                                <div style = "background: url('../../Images/icons/application-iconset/32/Settings.png');center left;display:block; height:28px; width:28px;float:left;"></div>
                                <div>Więcej</div>
                            </a>
                        </td>
</tr><tr>                        <td>
                            8
                        </td>
                        <td>
                                <center>
                                    <img src='../../Images/icons/category-icons/28.png' alt="brak zdjęcia" />
                                </center>
                        </td>
                        <td>
                            17.01.2020
                        </td>
                        <td>
                            grunty
                        </td>
                        <td>
                                <div>grunty</div>
                        </td>
                        <td>
                            Gdańsk
                            <br />
                            (pomorskie)
                        </td>
                        <td style="text-align: right">
                            49&#160;725,00 zł
                        </td>
                        <td>
                            <a href="/Notice/Details/495180">
                                <div style = "background: url('../../Images/icons/application-iconset/32/Settings.png');center left;display:block; height:28px; width:28px;float:left;"></div>
                                <div>Więcej</div>
                            </a>
                        </td>
</tr>
    </table>
</div>


        <p>Obwieszczenia koloru żółtego oznaczają prowadzenie egzekucji w trybie uproszczonym.</p>


<!-- paging -->
    <div>
        <div style="width: 200px; float: left; text-align: left;">
            &nbsp</div>
        <div style="width: 385px; float: left; text-align: center">
                    </div>
        
            <div style="width: 185px; float: left; text-align: right;">
                Stron 1
                z 1
            </div>
        <div class="clear">
        </div>
    </div>

        </div>
    </div>
  </div>
  <div id="footer-wrapper"  class="vertical-space-pusher">
    <div id="footer-container" class="container">
      <div id="contact-us" class="one_fourth">
        <h6>Kontakt</h6>
        <ul style="padding-bottom: 0; margin-bottom: 0;">
            <li><span style="margin-bottom: 5px; font-size: 13px;"><b>Infolinia dla komorników sądowych: </b>(58) 712-90-50</span></li>
        </ul>
      </div>
      <div id="contact-us2" class="one_fourth">
        <a href="http://www.komornik.pl"><img src="/Images/krk.jpg" alt="krk" width="50%" /></a>
      </div>
    </div>
  </div>
  <div id="footer-bottom-wrapper">
    <div id="footer-bottom-container" class="container">
      <div id="footer-copyright" class="one_half">
        <p class="text-align-center">
            Krajowa Rada Komornicza © 2019, 3.1.10.4
        </p>
      </div>
      <div id="footer-links" class="one_half last_column">
        <ul>
            <li><a href="/Home/Newsletter">Subskrypcja</a></li>
            <li><a href="/Home/Rules">Regulamin</a></li>
            <li><a href="/Home/AboutUs">Informacje</a></li>
            <li><a href="/Home/ContactUs">Kontakt</a></li>
        </ul>
      </div>
    </div>
  </div>
</body>
</html>
"""

    let singleAuction = """
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="description" content="Strona internetowa zawiera obwieszczenia o planowanych przez komorników sądowych licytacjach nieruchomości i ruchomości." />
    <meta name="keywords" 
        content="aukcje komornicze, komornik licytacje, licytacje komornik, krajowa rada komornicza, przetargi komornicze, komornik, licytacje, licytacja komornicza, komornik.pl, licytacje komornicze samochody,
            krajowa izba komornicza, licytację komornicze, komornicze licytacje, licytacje komornicze wrocław, domy, garaże, miejsca postojowe, grunty, lokale użytkowe, magazyny i hale, mieszkania,
            nieruchomości, statki morskie, antyki, sztuka, biżuteria, zegarki, łodzie, jachty, maszyny przemysłowe, maszyny rolnicze, materiały przemysłowe, meble, motocykle, skutery, quady,
            odzież, pozostałe ruchomości, przyczepy, naczepy, samochody ciężarowe, samochody osobowe, sprzęt agd, sprzęt komputerowy, sprzęt rtv, surowce, aukcja, obwieszczenie,
            obwieszczenie komornika, obwieszczenie komornicze, obwieszczenia, obwieszczenia komorników, licytacja nieruchomości, licytacja ruchomości, 
            Egzekucja przez sprzedaż przedsiębiorstwa lub gospodarstwa rolnego, Egzekucja ze statków morskich" />
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <title>Szczeg&#243;ły licytacji | Obwieszczenia o licytacjach</title>
    <link href="/Content/button.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/lightbox/css/lightbox.css" rel="stylesheet" type="text/css" />
    <link href="/Content/themes/base/jquery-ui-1.8.17.custom.css" rel="stylesheet" type="text/css" />
    <link href="/Content/Site.css" rel="stylesheet" type="text/css" />
    <link href="/favicon.ico" rel="icon" type="image/x-icon" />

    <style type="text/css">
      #map-canvas { height: 400px; }
    </style>
    
</head>
<body>

  

  <!-- HEADER -->
  <a id="top-region"></a>
  <div id="header-wrapper" >
    <div id="header-container" class="container">
      <div id="header-logo">
        <a href="/"><img src="/Images/logo.jpg" alt="Logo" /></a>
      </div>

      <div id="top-elements">    
        <div id="header-social-icons">
          
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("click", "#login-show", function () {
                document.querySelector("#div-login").style.display = "block";
                document.querySelector("#login-show").style.display = "none";
            });

            $(document).on("click", "#login-hide", function () {
                document.querySelector("#div-login").style.display = "none";
                document.querySelector("#login-show").style.display = "block";
            });
        });
    </script>
    <div id="login-show" style="cursor:pointer; font-size:14px;">STREFA DLA KOMORNIKÓW SĄDOWYCH <img src="../../Images/loginexpand.png"/></div>
    <div class="normal-button-logging" id="div-login" style="display:none;">
        <a href="/Account/LogOn">Logowanie</a>
        | <a href="/Account/Register">Rejestracja</a>
        | <a href="/Account/ResetPassword">Reset hasła</a>
        | <img id="login-hide" src="../../Images/loginclose.png" style="position: relative; top: 1px; opacity:1; background:Transparent; cursor:pointer;"/>
    </div>

        </div>
      </div>
      <div id="navigation-container">
        <div style="float: right;">
            <ul class="sf-menu">
                <li class="current"><a href="/Home/Index">Gł&#243;wna</a></li>
                <li class="current"><a href="/Notice/Search">Wyszukaj</a></li>
                <li class="current"><a href="/Home/Newsletter">Subskrypcja</a></li>
                <li><a href="/Home/ContactUs">Kontakt</a></li>
            </ul>
        </div>
      </div> 
    </div> 
  </div>
  <div id="header-wrapper2"></div>
  <div id="inner-page-wrapper"> 
   	<div id="inner-page-container" class="container vertical-space-pusher">
        <div id="sidebar" class="one_fourth"> 
            <div class="extra-space-bottom">
                <div>
                    <a href="/Notice/Filter/-3"><h6 class="linksMain" style="font-family: 'Lucida Sans Unicode', 'Lucida Grande', sans-serif;">Wszystkie nieruchomości</h6></a>
                </div>
                <div class="styled-list">
                    <ul>
                        <li><span></span><a href="/Notice/Filter/29">domy</a></li>
                        <li><span></span><a href="/Notice/Filter/27">garaże, miejsca postojowe</a></li>
                        <li><span></span><a href="/Notice/Filter/28">grunty</a></li>
                        <li><span></span><a href="/Notice/Filter/31">lokale użytkowe</a></li>
                        <li><span></span><a href="/Notice/Filter/32">magazyny i hale</a></li>
                        <li><span></span><a href="/Notice/Filter/30">mieszkania</a></li>
                        <li><span></span><a href="/Notice/Filter/34">nieruchomości</a></li>
                        <li><span></span><a href="/Notice/Filter/33">statki morskie</a></li>
                    </ul>
                </div>
                
                <div class="extra-space-bottom-header">
                    <a href="/Notice/Filter/-2"><h6 class="linksMain" style="font-family: 'Lucida Sans Unicode', 'Lucida Grande', sans-serif;">Wszystkie ruchomości</h6></a>
                </div>
                <div class="styled-list">
                    <ul>
                        <li><span></span><a href="/Notice/Filter/14">antyki, sztuka</a></li>
                        <li><span></span><a href="/Notice/Filter/15">biżuteria, zegarki</a></li>
                        <li><span></span><a href="/Notice/Filter/16">łodzie, jachty</a></li>
                        <li><span></span><a href="/Notice/Filter/17">maszyny przemysłowe</a></li>
                        <li><span></span><a href="/Notice/Filter/18">maszyny rolnicze</a></li>
                        <li><span></span><a href="/Notice/Filter/19">materiały przemysłowe</a></li>
                        <li><span></span><a href="/Notice/Filter/20">meble</a></li>
                        <li><span></span><a href="/Notice/Filter/21">motocykle, skutery, quady</a></li>
                        <li><span></span><a href="/Notice/Filter/22">odzież</a></li>
                        <li><span></span><a href="/Notice/Filter/37">pozostałe ruchomości</a></li>
                        <li><span></span><a href="/Notice/Filter/23">przyczepy, naczepy</a></li>
                        <li><span></span><a href="/Notice/Filter/2">samochody ciężarowe</a></li>
                        <li><span></span><a href="/Notice/Filter/24">samochody osobowe</a></li>
                        <li><span></span><a href="/Notice/Filter/25">sprzęt agd</a></li>
                        <li><span></span><a href="/Notice/Filter/26">sprzęt komputerowy</a></li>
                        <li><span></span><a href="/Notice/Filter/35">sprzęt rtv</a></li>
                        <li><span></span><a href="/Notice/Filter/36">surowce</a></li>
                    </ul>
                </div>

                <div class="extra-space-bottom-header">
                    <a href="/Decision"><h6 class="linksMain">Egzekucja przez sprzedaż przedsiębiorstwa lub gospodarstwa rolnego</h6></a>
                </div>
                <div class="styled-list">
                    <ul>
                        <li><a href="/Decision?type=1">postanowienie w zw. art. 1064 15 kpc</a></li>
                        <li><a href="/Decision?type=2">postanowienie w zw. art. 1064 19 kpc</a></li>
                    </ul>
                </div>
                
                <div class="extra-space-bottom-header">
                    <h6 class="linksMain">Opis i oszacowanie</h6>
                </div>
                <div class="styled-list">
                    <ul>
                        <li><a href="/Description/Published">Obwieszczenia o terminie opisu i oszacowania</a></li>
                    </ul>
                </div>

                <div class="extra-space-bottom-header">
                    <h6 class="linksMain">Egzekucja ze statków morskich</h6>
                </div>
                <div class="styled-list">
                    <ul>
                        <li><a href="/Declaration?type=1">ogłoszenia o wszczęciu egzekucji</a></li>
                    </ul>
                </div>
            </div>
            <div class="extra-space-bottom-header">
                <h6>Dokumenty do pobrania</h6>
                <a href="/File/PLKRK-IU.pdf">Instrukcja dla użytkownika</a><br />
                <a href="/File/PLKRK-IK.pdf">Instrukcja dla komornika</a>
            </div>
        </div>
        <div id="main-content" class="three_fourth last_column">
            


<h2>Szczegóły licytacji</h2>


<div id="Preview" class="schema-preview">
    <p>Komornik Sądowy<br /></p><p>przy Sądzie Rejonowym <strong></strong><strong>Gdańsk-Południe w Gdańsku</strong><strong></strong></p><p><strong>Marek Trocki</strong></p><p>Kancelaria Komornicza, <strong>Na Stoku 46,</strong><strong> Gdańsk, </strong><strong> 80-874 </strong><strong>Gdańsk</strong><strong></strong></p><p>tel. <strong>58 304-12-02</strong> / fax. <strong>58 304-12-02</strong></p><p style="text-align:left;">Sygnatura: <strong>Km 266/17</strong></p><p style="text-align:center;"><span style="text-decoration:underline;">OBWIESZCZENIE </span><span style="text-decoration:underline;">O PIERWSZEJ</span><span style="text-decoration:underline;"> LICYTACJI NIERUCHOMOŚCI</span></p><br /><p style="text-align:justify;"><strong>Komornik Sądowy przy Sądzie Rejonowym</strong> <strong></strong><strong>Gdańsk-Południe w Gdańsku </strong><strong></strong><strong>Marek Trocki</strong><span style="font-weight:bold;"> </span>na podstawie art. 953 kpc podaje do publicznej wiadomości, że w dniu <strong>12-12-2019</strong> o godz. <strong>10:00</strong><strong> w budynku Sądu Rejonowego Gdańsk-Południe w Gdańsku z siedzibą przy 3 Maja 9A, 80-802 Gdańsk, pokój E1.10A, </strong> odbędzie się <strong>pierwsza</strong> licytacja <strong>lokalu mieszkalnego</strong> należącego do dłużnika: <strong>KRYSTYNA ANNA JASTRZĘBSKA</strong>, położonego przy <strong>Piłkarska 2/7,</strong><strong> </strong><strong>80-180 </strong><strong>Gdańsk</strong>, dla którego<strong></strong> <strong>III Wydział Ksiąg Wieczystych Sąd Rejonowy  Gdańsk-Północ w Gdańsku</strong> prowadzi księgę wieczystą o numerze <strong>GD1G/00102202/4</strong>.</p><p style="text-align:justify;">Suma oszacowania wynosi <strong>185 000,00 zł</strong>, zaś cena wywołania jest równa <strong>3/4</strong> sumy oszacowania i wynosi <strong>138 750,00 </strong><strong>zł</strong>. <strong>Licytant przystępujący do przetargu powinien złożyć rękojmię w wysokości jednej dziesiątej sumy oszacowania, to jest 18 500,00 zł.</strong> Rękojmia powinna być złożona w gotówce albo w książeczce oszczędnościowej zaopatrzonej w upoważnienie właściciela książeczki do wypłaty całego wkładu stosownie do prawomocnego postanowienia sądu o utracie rękojmi. Rękojmię można uiścić także na konto komornika: Bank Zachodni WBK SA 13 O. w Gdańsku 17 15001025 1210 2010 6665 0000.</p><p style="text-align:justify;">Zgodnie z przepisem art. 976 § 1 kpc w przetargu nie mogą uczestniczyć: dłużnik, komornik, ich małżonkowie, dzieci, rodzice i rodzeństwo oraz osoby obecne na licytacji w charakterze urzędowym, licytant, który nie wykonał warunków poprzedniej licytacji, osoby, które mogą nabyć nieruchomość tylko za zezwoleniem organu państwowego, a zezwolenia tego nie przedstawiły.</p><p style="text-align:justify;"><span style="font:12px/18px Verdana, Geneva, sans-serif;text-align:justify;color:#000000;text-transform:none;text-indent:0px;letter-spacing:normal;word-spacing:0px;float:none;display:inline !important;white-space:normal;orphans:2;widows:2;font-size-adjust:none;font-stretch:normal;background-color:#ffffff;-webkit-text-size-adjust:auto;-webkit-text-stroke-width:0px;"></span>W ciągu dwóch tygodni przed licytacją wolno oglądać nieruchomość w dni powszednie od godz. 10:00 do godz. 13:00 <span style="font:12px/normal Verdana, sans-serif;text-align:justify;color:#484848;text-transform:none;text-indent:0px;letter-spacing:normal;word-spacing:0px;white-space:normal;orphans:2;widows:2;font-size-adjust:none;font-stretch:normal;background-color:#ffffff;-webkit-text-size-adjust:auto;-webkit-text-stroke-width:0px;"></span>oraz przeglądać akta postępowania egzekucyjnego <span style="font-size:small;"> </span>w kancelarii komornika.</p><p style="text-align:justify;">Prawa osób trzecich nie będą przeszkodą do licytacji i przysądzenia własności na rzecz nabywcy bez zastrzeżeń, jeżeli osoby te przed rozpoczęciem przetargu nie złożą dowodu, iż wniosły powództwo o zwolnienie nieruchomości lub przedmiotów razem z nią zajętych od egzekucji i uzyskały w tym zakresie orzeczenie wstrzymujące egzekucję.</p><p style="text-align:justify;">Użytkowanie, służebności i prawa dożywotnika, jeżeli nie są ujawnione w księdze wieczystej lub przez złożenie dokumentu do zbioru dokumentów i nie zostaną zgłoszone najpóźniej na trzy dni przed rozpoczęciem licytacji, nie będą uwzględnione w dalszym toku egzekucji i wygasną z chwilą uprawomocnienia się postanowienia o przysądzeniu własności.</p><p style="text-align:justify;">Zgodnie z art. 962 § 1. przystępujący do przetargu jest obowiązany złożyć rękojmię w wysokości jednej dziesiątej sumy oszacowania, najpóźniej w dniu poprzedzającym przetarg.</p><p style="text-align:justify;"> </p><p style="text-align:right;">Komornik Sądowy</p><p style="text-align:right;">Marek Trocki</p><p style="text-align:right;"><br /></p>
</div>

<div id="Picture" style="margin-bottom: 10px;">
</div>

<div id="Map" style="height:400px"></div>

<input type="hidden" id="hidden_address" value="80-180,Gdańsk,Piłkarska,2" />
<input type="hidden" id="hidden_lat" value="" />
<input type="hidden" id="hidden_lng" value="" />
<input type="hidden" id="hidden_zoom" value="" />

<a name="contact-form-region"></a>
<div id="contact-form" name="contact-form" style="display: none">
    
<style type="text/css">
    textarea { width: 340px; }
    fieldset { margin-top: 20px; }
    .editor-field { width: 400px; }
</style>


<form action="/Notice/Details/491902" method="post"><input data-val="true" data-val-number="The field BidId must be a number." data-val-required="The BidId field is required." id="BidId" name="BidId" type="hidden" value="491902" /><input data-val="true" data-val-required="The Token field is required." id="Token" name="Token" type="hidden" value="33e16954-1b9d-4294-b96f-fb1e07534b88" />    <fieldset>
        <h6>Wiadomość zostanie wysłana do komornika publikującego obwieszczenie</h6>

        <input id="Token" name="Token" type="hidden" value="33e16954-1b9d-4294-b96f-fb1e07534b88" />

        <div class="editor-label w150">
            <label for="Subject">Temat</label>
        </div>
        <div class="editor-field">
            <input class="text-box single-line" data-val="true" data-val-length="Nieodpowiednia długość (maks. 100)" data-val-length-max="100" data-val-required="To pole jest wymagane." id="Subject" name="Subject" type="text" value="" /><br />
            <span class="field-validation-valid" data-valmsg-for="Subject" data-valmsg-replace="true"></span>
        </div>
            
        <div class="editor-label w150">
            <label for="Text">Tekst wiadomości</label>
        </div>
        <div class="editor-field">
            
            <textarea cols="30" data-val="true" data-val-length="Nieodpowiednia długość (maks. 2000)" data-val-length-max="2000" data-val-required="To pole jest wymagane." id="Text" name="Text" rows="10">
</textarea><br />
            <span class="field-validation-valid" data-valmsg-for="Text" data-valmsg-replace="true"></span>
        </div>

        <div class="editor-label w150">
            <label for="Email">Adres email</label>
        </div>
        <div class="editor-field">
            <input class="text-box single-line" data-val="true" data-val-length="Nieodpowiednia długość (maks. 100)" data-val-length-max="100" data-val-required="To pole jest wymagane." id="Email" name="Email" type="text" value="" /><br />
            <span class="field-validation-valid" data-valmsg-for="Email" data-valmsg-replace="true"></span>
        </div>

        <div class="editor-field">
            Przepisz kod z obrazka: <br />
            <img alt="Are you a human being?" src="/MvcCaptcha/GetPicture/S0JJWkhBSVJZbDViWkFRaFpnSUdEMUVTZHdNaExobGdkQ0E5SUIwK09od2hIMUk5VDFVVkZDRWxEV3dBTEhRQUJqeGhKaTA1SW1reUtCbzFIME5GSUFRc1oybDdLaGNuUW04dlBqNC9OSG9QQ1dvOE9nbGVBQT09"></img><input id="MvcCaptchaHiddenInputId" name="MvcCaptchaHiddenInputId" type="hidden" value="S0JJWkhBSVJZbDViWkFRaFpnSUdEMUVTZHdNaExobGdkQ0E5SUIwK09od2hIMUk5VDFVVkZDRWxEV3dBTEhRQUJqeGhKaTA1SW1reUtCbzFIME5GSUFRc1oybDdLaGNuUW04dlBqNC9OSG9QQ1dvOE9nbGVBQT09"></input><br />
            <input autocomplete="off" data-val="true" data-val-length="Nieodpowiednia długość (maks. 100)" data-val-length-max="100" data-val-required="To pole jest wymagane." id="Captcha" name="Captcha" style="width: 150px;" type="text" value="" /><br />
            <span class="field-validation-valid" data-valmsg-for="Captcha" data-valmsg-replace="true"></span>
        </div>

        <div class="editor-label w150"></div>
        <div class="editor-field w700">
            <a href="#top-region" onclick="javascript:hideContactForm();" class="button_prev_active">Rezygnuj</a>
            <button type="submit" value="Wyślij" class="button_next_active">Wyślij</button>
        </div>
    </fieldset>
</form>
</div>

<p style="margin-top:10px;">
    <a href="javascript: history.go(-1)" class="button_prev_active">Powrót</a>
</p>



        </div>
    </div>
  </div>
  <div id="footer-wrapper"  class="vertical-space-pusher">
    <div id="footer-container" class="container">
      <div id="contact-us" class="one_fourth">
        <h6>Kontakt</h6>
        <ul style="padding-bottom: 0; margin-bottom: 0;">
            <li><span style="margin-bottom: 5px; font-size: 13px;"><b>Infolinia dla komorników sądowych: </b>(58) 712-90-50</span></li>
        </ul>
      </div>
      <div id="contact-us2" class="one_fourth">
        <a href="http://www.komornik.pl"><img src="/Images/krk.jpg" alt="krk" width="50%" /></a>
      </div>
    </div>
  </div>
  <div id="footer-bottom-wrapper">
    <div id="footer-bottom-container" class="container">
      <div id="footer-copyright" class="one_half">
        <p class="text-align-center">
            Krajowa Rada Komornicza © 2019, 3.1.10.4
        </p>
      </div>
      <div id="footer-links" class="one_half last_column">
        <ul>
            <li><a href="/Home/Newsletter">Subskrypcja</a></li>
            <li><a href="/Home/Rules">Regulamin</a></li>
            <li><a href="/Home/AboutUs">Informacje</a></li>
            <li><a href="/Home/ContactUs">Kontakt</a></li>
        </ul>
      </div>
    </div>
  </div>
</body>
</html>
"""

    [<Fact>]
    let ``when parsing html with list of data should return 8 of them``() =
        let result = Parser.parseHtml auctionsHtml
        
        result =! [
            { prize = 138750.00M; ``when`` = DateTime(2019, 12, 12); link = Uri("http://www.licytacje.komornik.pl/Notice/Details/491902") }
            { prize = 140021.25M; ``when`` = DateTime(2019, 12, 12); link = Uri("http://www.licytacje.komornik.pl/Notice/Details/494476") }
            { prize = 126006.75M; ``when`` = DateTime(2019, 12, 12); link = Uri("http://www.licytacje.komornik.pl/Notice/Details/494944") }
            { prize = 197680.50M; ``when`` = DateTime(2019, 12, 12); link = Uri("http://www.licytacje.komornik.pl/Notice/Details/494475") }
            { prize = 231750.00M; ``when`` = DateTime(2019, 12, 13); link = Uri("http://www.licytacje.komornik.pl/Notice/Details/491787") }
            { prize = 223720.50M; ``when`` = DateTime(2020, 01, 09); link = Uri("http://www.licytacje.komornik.pl/Notice/Details/492855") }
            { prize = 319500.00M; ``when`` = DateTime(2020, 01, 09); link = Uri("http://www.licytacje.komornik.pl/Notice/Details/495021") }
            { prize = 49725.00M; ``when`` = DateTime(2020, 01, 17); link = Uri("http://www.licytacje.komornik.pl/Notice/Details/495180") }
        ]
        
    [<Fact>]
    let ``when parsing single auction should return proper description and address which is equal to: "80-180,Gdańsk,Piłkarska,2"``() =
        let result = Parser.parseAuction singleAuction |> Async.RunSynchronously
        
        result =! Some { description = "Komornik Sądowy
przy Sądzie Rejonowym Gdańsk-Południe w GdańskuMarek TrockiKancelaria Komornicza, Na Stoku 46, Gdańsk,  80-874 Gdańsktel. 58 304-12-02 / fax. 58 304-12-02Sygnatura: Km 266/17OBWIESZCZENIE O PIERWSZEJ LICYTACJI NIERUCHOMOŚCI
Komornik Sądowy przy Sądzie RejonowymGdańsk-Południe w Gdańsku Marek Trockina podstawie art. 953 kpc podaje do publicznej wiadomości, że w dniu 12-12-2019 o godz. 10:00 w budynku Sądu Rejonowego Gdańsk-Południe w Gdańsku z siedzibą przy 3 Maja 9A, 80-802 Gdańsk, pokój E1.10A,  odbędzie się pierwsza licytacja lokalu mieszkalnego należącego do dłużnika: KRYSTYNA ANNA JASTRZĘBSKA, położonego przy Piłkarska 2/7,80-180 Gdańsk, dla któregoIII Wydział Ksiąg Wieczystych Sąd Rejonowy Gdańsk-Północ w Gdańsku prowadzi księgę wieczystą o numerze GD1G/00102202/4.Suma oszacowania wynosi 185 000,00 zł, zaś cena wywołania jest równa 3/4 sumy oszacowania i wynosi 138 750,00 zł. Licytant przystępujący do przetargu powinien złożyć rękojmię w wysokości jednej dziesiątej sumy oszacowania, to jest 18 500,00 zł. Rękojmia powinna być złożona w gotówce albo w książeczce oszczędnościowej zaopatrzonej w upoważnienie właściciela książeczki do wypłaty całego wkładu stosownie do prawomocnego postanowienia sądu o utracie rękojmi. Rękojmię można uiścić także na konto komornika: Bank Zachodni WBK SA 13 O. w Gdańsku 17 15001025 1210 2010 6665 0000.Zgodnie z przepisem art. 976 § 1 kpc w przetargu nie mogą uczestniczyć: dłużnik, komornik, ich małżonkowie, dzieci, rodzice i rodzeństwo oraz osoby obecne na licytacji w charakterze urzędowym, licytant, który nie wykonał warunków poprzedniej licytacji, osoby, które mogą nabyć nieruchomość tylko za zezwoleniem organu państwowego, a zezwolenia tego nie przedstawiły.W ciągu dwóch tygodni przed licytacją wolno oglądać nieruchomość w dni powszednie od godz. 10:00 do godz. 13:00 oraz przeglądać akta postępowania egzekucyjnego w kancelarii komornika.Prawa osób trzecich nie będą przeszkodą do licytacji i przysądzenia własności na rzecz nabywcy bez zastrzeżeń, jeżeli osoby te przed rozpoczęciem przetargu nie złożą dowodu, iż wniosły powództwo o zwolnienie nieruchomości lub przedmiotów razem z nią zajętych od egzekucji i uzyskały w tym zakresie orzeczenie wstrzymujące egzekucję.Użytkowanie, służebności i prawa dożywotnika, jeżeli nie są ujawnione w księdze wieczystej lub przez złożenie dokumentu do zbioru dokumentów i nie zostaną zgłoszone najpóźniej na trzy dni przed rozpoczęciem licytacji, nie będą uwzględnione w dalszym toku egzekucji i wygasną z chwilą uprawomocnienia się postanowienia o przysądzeniu własności.Zgodnie z art. 962 § 1. przystępujący do przetargu jest obowiązany złożyć rękojmię w wysokości jednej dziesiątej sumy oszacowania, najpóźniej w dniu poprzedzającym przetarg.Komornik SądowyMarek Trocki
"; address = "80-180,Gdańsk,Piłkarska,2"; point = { longitude = 18.58252845m; latitude = 54.30561535m } }
