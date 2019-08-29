/*
Copyright 2019 Pitney Bowes Inc.

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
using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Fluent;
using PitneyBowes.Developer.ShippingApi.Model;
using Xunit;
using Xunit.Abstractions;

namespace tests
{
    public class TestCarrierAccount : TestSession
    {
        public TestCarrierAccount(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            InitializeFramework();
        }

        [Fact]
        public void TestLicenseAgreement()
        {

            var license = new CarrierLicense();
            license.Carrier = Carrier.UPS;
            license.OriginCountryCode = "US";
            
            var response = Api.CarrierLicenseAgreements(license, Globals.DefaultSession).GetAwaiter().GetResult();

            Assert.True(response.Success);
            var responseLicense = response.ResponseObject as CarrierLicense;
            Assert.NotNull(responseLicense);
            Assert.NotNull(responseLicense.LicenseText);
        }

        //[Fact] // Need to have UPS account
        public void TestRegistration()
        {

            var license = new CarrierLicense();
            license.Carrier = Carrier.UPS;
            license.OriginCountryCode = "US";
            
            var licResponse = Api.CarrierLicenseAgreements(license, Globals.DefaultSession).GetAwaiter().GetResult();

            var responseLicense = licResponse.ResponseObject as CarrierLicense;

            var registration = new CarrierAccount();
            registration.DeveloperId = Globals.DefaultSession.GetConfigItem("DeveloperID");
            registration.Carrier = Carrier.UPS;
            registration.PostalReportingNumber = "123456789";
            registration.TransactionId = Guid.NewGuid().ToString().Substring(15);
            registration.AccountNumber = "56V7A6";

            var accountAddress = AddressFluent<Address>.Create()
                .Company("Widgets")
                .Name("James Smith")
                .Phone("303-555-1213")
                .Email("js@example.com")
                .Residential(false)
                .AddressLines("4750 Walnut Street")
                .CityTown("Boulder")
                .StateProvince("CO")
                .PostalCode("80301")
                .CountryCode("US");
            
            registration.AccountAddress = (Address)accountAddress;
            registration.ContactAddress = (Address)accountAddress;
            registration.AccountPostalCode = "80301";
            registration.AccountCountryCode = "US";
            registration.ContactTitle = "Manager";
            registration.EndUserIP = "10.100.20.20";
            registration.DeviceIdentity = "84315tr3M5e4n";
            registration.LicenseText = responseLicense.LicenseText;

            var response = Api.RegisterCarrierAccount(registration, Globals.DefaultSession).GetAwaiter().GetResult();
            Assert.True(response.Success);
            var responseAccount = response.ResponseObject as CarrierAccount;
            Assert.NotNull(responseAccount);

        }
    }
}
