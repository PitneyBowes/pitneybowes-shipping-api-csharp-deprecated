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

using System.Threading.Tasks;
using PitneyBowes.Developer.ShippingApi.Json;

namespace PitneyBowes.Developer.ShippingApi
{
    public static partial class Api
    {
        /// <summary>
        /// This operation retrieves a carrier’s license agreement. The operation is used in the Carrier Registration Tutorial.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public async static Task<ShippingApiResponse<T>> GetCarrierLicense<T>(T request, ISession session = null) where T : ICarrierLicense, new()
        {
            var license = new JsonCarrierLicense<T>(request);
            return await WebMethod.Post<T, JsonCarrierLicense<T>>("/v1/carrier/licenseagreements?carrier={carrier}&originCountryCode={originCountryCode}", license, session);
        }

        /// <summary>
        /// Use these steps to register a merchant’s existing carrier account for use with the PB Shipping APIs. The steps use 
        /// two API operations: Get Carrier License Agreement and Register an Existing Carrier Account. Currently, the APIs 
        /// support integration with UPS® (United Parcel Service).
        /// 1. Obtain the merchant’s carrier account information.
        /// Your application must retrieve the information about the merchant’s existing carrier account that is required by 
        /// the Register an Existing Carrier Account API.
        /// 2. Obtain the merchant’s approval for the carrier’s license agreement.
        /// Your application must present the merchant with the text of the carrier’s license agreement, as retrieved by the 
        /// Carrier License Agreement API, and must obtain the merchant’s approval for the agreement.
        /// Keep the text of the carrier’s license agreement for use in the Register an Existing Carrier Account API.
        /// 3. Register the merchant’s carrier account with Pitney Bowes.
        /// Using the information retrieved in Step 1, register the carrier account using the Register an Existing Carrier 
        /// Account API.
        /// 4. Store the shipper-carrier account ID for use in future API calls.
        /// The Register an Existing Carrier Account API returns a shipper-carrier account ID in the shipperCarrierAccountId 
        /// field.Store this ID.You will pass it in the X-PB-Shipper-Carrier-AccountId request header whenever the merchant performs an operation that uses this carrier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="session"></param>
        /// <returns></returns>
    public async static Task<ShippingApiResponse<T>> RegisterCarrierAccount<T>(T request, ISession session = null) where T : ICarrierRegistration, new()
        {
            var CarrierRegistration = new JsonCarrierRegistration<T>(request);
            return await WebMethod.Post<T, JsonCarrierRegistration<T>>("/v1/developers/{developerId}/merchants/{postalReportingNumber}/carrier-accounts/register?carrier={carrier}", CarrierRegistration, session);
        }
    }
}

