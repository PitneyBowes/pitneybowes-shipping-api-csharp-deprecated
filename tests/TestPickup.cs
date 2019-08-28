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
using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Fluent;
using PitneyBowes.Developer.ShippingApi.Model;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace tests
{
    public class TestPickup : TestSession
    {
        public TestPickup(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            InitializeFramework();
        }

        [Fact]
        public void ScheduleAndCancel()
        {

            var address = (Address)AddressFluent<Address>.Create()
                .Company("Pitney Bowes Inc.")
                .AddressLines("27 Waterview Drive")
                .Residential(false)
                .CityTown("Shelton")
                .StateProvince("CT")
                .PostalCode("06484")
                .CountryCode("US")
                .Person("Paul Wright", "203-555-1213", "john.publica@pb.com");

            // Schedule a pickup

            var pickup = PickupFluent<Pickup>.Create()
                .Carrier(Carrier.USPS)
                .PackageLocation(PackageLocation.MailRoom)
                .PickupAddress(address)
                .PickupDate(DateTime.Now.AddDays(1))
                .SpecialInstructions("N/A")
                .AddPickupSummary<PickupCount, ParcelWeight>(PickupService.PM, 1, 16M, UnitOfWeight.OZ)
                .TransactionId(Guid.NewGuid().ToString().Substring(15));
            var pickupResponse = Api.Schedule<Pickup>(pickup).GetAwaiter().GetResult();

            Assert.NotNull(pickupResponse);
            Assert.True(pickupResponse.Success);
            Assert.NotNull(pickupResponse.ResponseObject);

            // Cancel pickup
            var p = pickupResponse.ResponseObject as Pickup;
            Assert.NotNull(p);

            var cancelPickup = new PickupCancelRequest()
            {
                PickupId = p.PickupId,
                TransactionId = Guid.NewGuid().ToString().Substring(15)
            };

            var pickupCancelResponse = Api.CancelPickup(cancelPickup).GetAwaiter().GetResult();
            Assert.NotNull(pickupCancelResponse);
            Assert.True(pickupCancelResponse.Success);
            Assert.NotNull(pickupCancelResponse.ResponseObject);

        }
    }
}
