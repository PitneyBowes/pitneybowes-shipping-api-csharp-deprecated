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
using System.Collections.Generic;
using System.Text;
using PitneyBowes.Developer.ShippingApi;

namespace PitneyBowes.Developer.ShippingApi
{
    /// <summary>
    /// Additional carrier account for a merchant to use with the PB Shipping APIs. The merchant 
    /// must have an existing account with the carrier. Currently, the API supports integration with UPS.
    /// </summary>
    public interface ICarrierAccount
    {
        /// <summary>
        /// Your Pitney Bowes developer ID. To retrieve your developer ID, log into Developer Hub and click your 
        /// username and select Profile.
        /// </summary>
        /// <value></value>
        string DeveloperId { get; set; }

        /// <summary>
        /// The unique ID used to identify the merchant. To retrieve the merchant’s postalReportingNumber, issue t
        /// he Get All Merchants API call.
        /// </summary>
        /// <value></value>
        string PostalReportingNumber { get; set; }

        /// <summary>
        /// The carrier to add to the merchant’s account.
        /// </summary>
        /// <value></value>
        Carrier Carrier { get; set; }

        /// <summary>
        /// A unique identifier for the transaction, up to 25 characters.
        /// </summary>
        /// <value></value>
        string TransactionId { get; set; }

        /// <summary>
        /// The merchant’s account number with the carrier.
        /// </summary>
        /// <value></value>
        string AccountNumber { get; set; }

        /// <summary>
        /// Required. The merchant’s “ship from” address as it appears on the carrier account.
        ///For UPS, this address must exactly match the “ship from” address on the merchant’s UPS invoice.
        /// </summary>
        /// <value></value>
        IAddress AccountAddress { get; set; }
        
        /// <summary>
        /// Required. The merchant’s contact address.
        /// </summary>
        /// <value></value>
        IAddress ContactAddress { get; set; }

        /// <summary>
        /// The two-character ISO country code for the merchant’s country.
        /// </summary>
        /// <value></value>
        string AccountCountryCode { get; set; }

        /// <summary>
        /// The merchant’s postal code.
        /// </summary>
        /// <value></value>
        string AccountPostalCode { get; set; }

        /// <summary>
        /// The title of the primary contact that the merchant provided to the carrier.
        /// </summary>
        /// <value></value>
        string ContactTitle { get; set; }

        /// <summary>
        /// The black-box identity of the merchant’s machine.
        /// For help in retrieving this information, consult your Pitney Bowes implementation engineer.
        /// </summary>
        /// <value></value>
        string DeviceIdentity { get; set; }
        
        /// <summary>
        /// The IP address of the merchant’s machine.
        /// </summary>
        /// <value></value>
        string EndUserIP { get; set; }

        /// <summary>
        /// The amount charged on a recent invoice. The invoice must be from within the last 90 days. 
        /// Use the same invoice for all the INVOICE_* parameters in this table.
        /// The value for this parameter can have a maximum of 16 digits before the decimal and 2 digits after the decimal.
        /// </summary>
        /// <value></value>
        decimal InvoiceAmount { get; set; }

        /// <summary>
        /// The Control ID displayed on the invoice.
        /// </summary>
        /// <value></value>
        string InvoiceControlId { get; set; } 

        /// <summary>
        /// The currency code used on the invoice.
        /// </summary>
        /// <value></value>
        string InvoiceCurrenyCode { get; set; }

        /// <summary>
        /// The date of the invoice. The invoice date should be within the last 90 days.
        /// </summary>
        /// <value></value>
        DateTimeOffset InvoiceDate { get; set; }

        /// <summary>
        /// The invoice number.
        /// </summary>
        /// <value></value>
        string InvoiceNumber { get; set; }

        /// <summary>
        /// The text of the merchant’s license agreement with the carrier, as retrieved through the Get Carrier License Agreement API.
        /// </summary>
        /// <value></value>
        string LicenseText { get; set; }

        /// <summary>
        /// User Id for subsequest API calls
        /// </summary>
        /// <value></value>
        string UserId { get; set; }
      
        /// <summary> 
        /// Password for subsequest API calls
        /// </summary>
        /// <value></value>
        string Password { get; set; } 

        /// <summary>
        /// API Key  for subsequest API calls
        /// </summary>
        /// <value></value>
        string Key { get; set; }

        /// <summary>
        /// Response Only. The identifier to use when the merchant performs an operation that uses this 
        /// carrier account. The identifier is passed in the X-PB-Shipper-Carrier-AccountId request header 
        /// of the API request.
        /// </summary>
        /// <value></value>
        string ShipperCarrierAccountId { get; set; }
    }
}