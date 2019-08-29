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

using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PitneyBowes.Developer.ShippingApi.Json;

namespace PitneyBowes.Developer.ShippingApi
{

    public static partial class Api
    {
        /// <summary>
        /// This operation retrieves a carrier’s license agreement.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public async static Task<ShippingApiResponse<T>> CarrierLicenseAgreements<T>(T request, ISession session = null) where T:ICarrierLicense, new()
        {
            var wrapper = new JsonCarrierLicense<T>(request);
            return await WebMethod.Get<T, JsonCarrierLicense<T>> ("/shippingservices/v1/carrier/license-agreements", wrapper, session);
        }

        /// <summary>
        /// This operation registers a carrier account.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public async static Task<ShippingApiResponse<T>> RegisterCarrierAccount<T>(T request, ISession session = null) where T:ICarrierAccount, new()
        {
            var wrapper = new JsonCarrierAccount<T>(request);
            return await WebMethod.Post<T, JsonCarrierAccount<T>> ("/shippingservices/v1/developers/{DeveloperId}/merchants/{PostalReportingNumber}/carrier-accounts/register", wrapper, session);
        }        
    }

}