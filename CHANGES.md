# Pitney Bowes Shipping API C# SDK Changes

## 1.2.1

## Fixes
* Fix of Label support size DOC_4X4.

## 1.2.0

## New Feature
* Support for FedEx carrier 


## Version 1.1.2

## Fixes
* Fix of Version issue in log.
* DPI information not getting parse when sending request.
* Context issue with web application.

## Version 1.1.1
### Framework and supporting library version updates
* JSON.net version downgraded to 10.0.3

### Fixes
* Removed the DotNetFramework example project and made the Myship example project target both dotnet core and .net framewotk
* Fixed MyShip project. 
* Removed redundant items from the shippingapisettings.json file
* Fixed issue where the recording folder and file was created when recording option was off.
* Fixed CancelPickup
* Made header lookup case insensitve in file request processing
* Fixed Pickup PackageLocation mapping

### New Features
* Carrier surcharges
* Document DocTab
* PB Presort Labels
* UPS Labels
* Carrier account license API Call
* Carrier account registration API call
* SoldToAddress in shipment
* Added irregular parcel girth to ParcelFluent
* Added event date and event time to Tracking object
* Changed default user agent string

### Tests
* Added Carrier Lincense Test
* Added Pickup Test
* Added Label Test
* Added Transaction Report Test
* Made ShipmentFromFile tests into separate tests that can be run individually
* Added standard mail and UPS tests. These require special account setup by the support and/or implementation team.

### Other
Switched main development environment to VSCode (from Visual Studio and Visual Studio for Mac)


### Known Issues
* Running 'dotnet test' hangs. I think it is something to do with the XUnit console runner.

## Version 1.1.0
### Framework and supporting library version updates
* Shipping API framework changed from .net standard 1.3 to .net standard 2.0
* Framework version for example programs changed to .net core 2.1
* JSON.net version 11.0.2
* xUnit version 2.4.1
### Fixes
* Fixed config file issues on macOS for sample programs. Default config file name on macOS is $HOME/shippingapisettings.json
* Session setting properties have been obsoleted and will be removed in the next version. Settings should instead be made in the config.

Property | Config Setting
---------|---------------
session.ThrowExceptions = false| "ThrowExceptions" : "false"
session.Record = false| "Record" : "false"
session.RecordOverwrite = false | "RecordOverwrite" : "false"
session.Retries = 3 | "Retries" : 3
session.RecordPath | "RecordRoot" : "/var/shippingAPI"

### New Features
* Ability to specify the maximum number of pages retrieved by the reports. Used in testing or as a failsafe to prevent enormous reports being downloaded.
* Added fluent extension methods for Newgistics labels
* Recording files now include both the full http request and response. The format of the file is multipart MIME. This file format was chosen because it allows the full http request and response to be captured in plain text, making it easy to cut, paste and perform other edits. This has made developing on macOS a lot easier due tot he ability to capture requests and responses. *Fiddler is no longer required to work on the API*.
* Added ability to deserialize the http request from a recording file (or other correctly formatted multipart MIME file).
* Recording file path is now simplified to be the first 2 levels of URI path, followed by the request name and a specific suffix for each request type - e.g. TransactionId for the CreateShipment method. 

### API Updates
Object      | Changes
------------|--------------------------------------------------
IAddress    | Added DeliveryPoint
<i></i>| Added CarrierRoute
 <i></i>| Added TaxId
 IDocTab| Added new interface and classes
 IDocument| Added Resolution
 <i></i>| Added DocTab
 IMerchant| Added ParcelProtection
 <i></i>| Added PaymentMethod
 IReference| Added new interface and classes
 IShipment | Added References
 ITransaction| Added PrintStatus
 <i></i>| Added RefundRequestor
 <i></i>| Added AdjustmentReason
 <i></i>| Added ExternalId
* Large number of enums added. See file diff for details.
### Tests
* Added Rates test
* Added tests from requests stored in a files in the testData folder
### Other
* Removed GenerateWrapper project. Wasn't functional enough to keep up with API changes. Requires a new code generation approach.
