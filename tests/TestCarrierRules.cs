﻿
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

using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Rules;
using Xunit;
using Xunit.Abstractions;

namespace tests
{
    public class TestCarrierRules: TestSession
    {
        public TestCarrierRules(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Fact]
        void ParseCarrierRules()
        {
            InitializeFramework();

            var req = new RatingServicesRequest()
            {

                Carrier = Carrier.USPS,
                OriginCountryCode = "US",
                DestinationCountryCode = "US"
            };
            var res = CarrierRulesMethods.RatingServices(req).GetAwaiter().GetResult();

        }

    }
}