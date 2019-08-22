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
using System.Collections.Generic;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    class CarrierRegistration : ICarrierRegistration
    {
        public string TransactionId { get; set; }

        public string ContentType => throw new NotImplementedException();

        public StringBuilder Authorization { get; set; }
        /// <summary>
        /// Required.Your Pitney Bowes developer ID. To retrieve your developer ID, log into Developer Hub and click your 
        /// username and select Profile.
        /// </summary>
        public string DeveloperId { get; set; }
        /// <summary>
        /// Required. The unique ID used to identify the merchant. To retrieve the merchant’s postalReportingNumber, issue the Get All Merchants API call.
        /// </summary>
        public string PostalReportingNumber { get; set; }
        /// <summary>
        /// Required.The carrier to add to the merchant’s account.
        /// Valid value: UPS
        /// </summary>
        public Carrier Carrier { get; set; }
        /// <summary>
        /// The merchant’s account number with the carrier.
        /// </summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// Carrier Registration Address Object. The merchant’s “ship from” address as it appears on the carrier account.
        /// For UPS, this address must exactly match the “ship from” address on the merchant’s UPS invoice.
        /// </summary>
        public IAddress AccountAddress { get; set; }
        /// <summary>
        /// Carrier Registration Address Object The merchant’s contact address.
        /// </summary>
        public IAddress ContactAddress { get; set; }
        /// <summary>
        /// Carrier Registration Parameters.The parameters include information the merchant 
        /// received from the carrier and must provide to the developer. Each object in the array defines a parameter 
        /// in the form of a name-value pair. 
        /// 
        /// </summary>
        public IEnumerable<IParameter> InputParameters { get; set; }
        /// <summary>
        /// Response Only.When Pitney Bowes verifies the merchant’s account with the carrier, the carrier generates 
        /// and sends back a set of credentials for use when the merchant accesses the carrier through the PB Shipping APIs. 
        /// These are not required for future API operations.Instead, Pitney Bowes provides the shipperCarrierAccountId 
        /// (below) to be passed with API calls.
        /// </summary>
        public IEnumerable<IParameter> CarrierAccount { get; set; }
        /// <summary>
        /// The identifier to use when the merchant performs an operation that uses this carrier account. The identifier is 
        /// passed in the X-PB-Shipper-Carrier-AccountId request header of the API request.
        /// </summary>
        public string ShipperCarrierAccountId { get; set; }

        /// <summary>
        /// Add parameter to InputParameters
        /// </summary>
        /// <param name="p"></param>
        public IParameter AddCarrierAccount(IParameter p)
        {
            return ModelHelper.AddToEnumerable<IParameter, Parameter>(p, () => CarrierAccount, (x) => CarrierAccount = x);
        }

        /// <summary>
        /// Add a carrier account parameter
        /// </summary>
        /// <param name="p"></param>
        public IParameter AddInputParameter(IParameter p)
        {
            return ModelHelper.AddToEnumerable<IParameter, Parameter>(p, () => InputParameters, (x) => InputParameters = x);
        }
    }
}
