/*
Copyright 2018 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*/

using System;
using System.IO;
using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Fluent;
using PitneyBowes.Developer.ShippingApi.Model;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace tests
{
    public class TestShipment : TestSession
    {
        public TestShipment(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            InitializeFramework();
        }

        [Fact]
        public void TestShipmentDetailed()
        {
            var shipment = ShipmentFluent<Shipment>.Create()
                .ToAddress((Address)AddressFluent<Address>.Create()
                    .AddressLines("643 Greenway RD")
                    .PostalCode("28607")
                    .CountryCode("US")
                    .Verify() // calls the service for address validation - will populate city and state from the zip
                    )
                .MinimalAddressValidation("true")
                //.ShipperRatePlan(Globals.DefaultSession.GetConfigItem("RatePlan")) // use if you have a custom rate plan
                .FromAddress((Address)AddressFluent<Address>.Create()
                    .Company("Pitney Bowes Inc.")
                    .AddressLines("27 Waterview Drive")
                    .Residential(false)
                    .CityTown("Shelton")
                    .StateProvince("CT")
                    .PostalCode("06484")
                    .CountryCode("US")
                    .Person("Paul Wright", "203-555-1213", "john.publica@pb.com")
                    )
                .Parcel((Parcel)ParcelFluent<Parcel>.Create()
                    .Dimension(12, 12, 10)
                    .Weight(16m, UnitOfWeight.OZ)
                    )
                .Rates(RatesArrayFluent<Rates>.Create()
                    .USPSPriority<Rates, Parameter>()
                    .InductionPostalCode("06484")
                    )
                .Documents((List<IDocument>)DocumentsArrayFluent<Document>.Create()
                    .ShippingLabel(ContentType.BASE64, Size.DOC_4X6, FileFormat.ZPL2)
                    )
                .ShipmentOptions(ShipmentOptionsArrayFluent<ShipmentOptions>.Create()
                    .ShipperId(Globals.DefaultSession.GetConfigItem("ShipperID"))
                    .AddToManifest()
                    )
                .TransactionId(Guid.NewGuid().ToString().Substring(15));


            var label = Api.CreateShipment((Shipment)shipment).GetAwaiter().GetResult();

            Assert.NotNull(label);
            Assert.True(label.Success);
            Assert.NotNull(label.ResponseObject as Shipment);
            Assert.NotNull(label.APIResponse.ParcelTrackingNumber);
            Assert.True(label.APIResponse.ParcelTrackingNumber != string.Empty);

            // var trackingRequest = new TrackingRequest
            // {
            //     Carrier = Carrier.USPS,
            //     TrackingNumber = label.APIResponse.ParcelTrackingNumber
            // };
            // var trackingResponse = Api.Tracking<TrackingStatus>(trackingRequest).GetAwaiter().GetResult();

            // Parcel Reprint
            var reprintRequest = new ReprintShipmentRequest() { Shipment = label.APIResponse.ShipmentId };

            var reprintResponse = Api.ReprintShipment<Shipment>(reprintRequest).GetAwaiter().GetResult();

            Assert.NotNull(reprintResponse);
            Assert.True(reprintResponse.Success);
            Assert.NotNull(reprintResponse.ResponseObject as Shipment);
            Assert.NotNull(reprintResponse.APIResponse.ParcelTrackingNumber);
            Assert.True(reprintResponse.APIResponse.ParcelTrackingNumber != string.Empty);

            using (var labelStream = new MemoryStream())
            {
                foreach (var d in reprintResponse.APIResponse.Documents)
                {
                    if (d.ContentType == ContentType.BASE64 && d.FileFormat == FileFormat.PNG)
                    {
                        // Multiple page png document
                        Api.WriteToStream(d, null,

                            (stream, page) => // callback for each page
                            {
                                // append to the memory stream
                                return labelStream;
                            },
                            disposeStream: false
                            ).GetAwaiter().GetResult();
                    }
                    else
                    {
                        Api.WriteToStream(d, labelStream ).GetAwaiter().GetResult();
                    }
                }
                Assert.True(labelStream.Length > 0);
            }

            // manifest
            var manifest = ManifestFluent<Manifest>.Create()
                .Carrier(Carrier.USPS)
                .FromAddress(((Shipment)shipment).FromAddress)
                .InductionPostalCode("06484")
                .SubmissionDate(DateTime.Now)
                .AddParameter<Parameter>(ManifestParameter.SHIPPER_ID, Globals.DefaultSession.GetConfigItem("ShipperID"))
                .TransactionId(Guid.NewGuid().ToString().Substring(15));
            var manifestResponse = Api.CreateManifest<Manifest>(manifest).GetAwaiter().GetResult();

            Assert.NotNull(manifestResponse);
            if (manifestResponse.Success)
            {
                Assert.NotNull(manifestResponse.ResponseObject);
            }
            else
            {
                // making a shipment available to manifest seems to be an async process and the shipment just created might 
                // not be available
                Assert.True(manifestResponse.Errors.Count == 1);
                Assert.True(manifestResponse.Errors[0].ErrorCode == "1110019");
            }

            using (var manifestStream = new MemoryStream())
            {
                foreach (var d in manifestResponse.APIResponse.Documents)
                {
                    Api.WriteToStream(d, manifestStream ).GetAwaiter().GetResult();
                }
                Assert.True(manifestStream.Length > 0);
            }

            // Cancel the label 

            var cancelRequest = new CancelShipmentRequest
            {
                Carrier = Carrier.USPS,
                CancelInitiator = CancelInitiator.SHIPPER,
                TransactionId = Guid.NewGuid().ToString().Substring(15),
                ShipmentToCancel = label.APIResponse.ShipmentId
            };
            var cancelResponse = Api.CancelShipment(cancelRequest).GetAwaiter().GetResult();
            Assert.NotNull(cancelResponse);
            Assert.True(cancelResponse.Success);
            Assert.NotNull(cancelResponse.ResponseObject);
        }
    }
}
