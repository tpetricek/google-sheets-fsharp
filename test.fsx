#r @"packages\Google.Apis.Sheets.v4\lib\net45\Google.Apis.Sheets.v4.dll"
#r @"packages\Google.Apis\lib\net45\Google.Apis.dll"
#r @"packages\Google.Apis\lib\net45\Google.Apis.PlatformServices.dll"
#r @"packages\Google.Apis.Core\lib\net45\Google.Apis.Core.dll"
#r @"packages\Google.Apis.Auth\lib\net45\Google.Apis.Auth.dll"
#r @"packages\Google.Apis.Auth\lib\net45\Google.Apis.Auth.PlatformServices.dll"
open Google.Apis.Auth.OAuth2
open Google.Apis.Sheets.v4
open Google.Apis.Sheets.v4.Data
open Google.Apis.Services
open Google.Apis.Util.Store
open System.Threading

let scopes = [| SheetsService.Scope.SpreadsheetsReadonly |]
let app = "Google Sheets API .NET Quickstart"

let cred = 
  GoogleWebAuthorizationBroker.AuthorizeAsync
    ( GoogleClientSecrets.Load(System.IO.File.OpenRead(__SOURCE_DIRECTORY__ + "/credentials.json")).Secrets,
      scopes, "user", CancellationToken.None,  new FileDataStore("token.json", true))
let res = cred.Result

let service = new SheetsService(new BaseClientService.Initializer(HttpClientInitializer = res, ApplicationName = app))
let spreadsheetId = "1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms"
let range = "Class Data!A2:E";
let req = service.Spreadsheets.Values.Get(spreadsheetId, range)

let ff = req.Execute()
