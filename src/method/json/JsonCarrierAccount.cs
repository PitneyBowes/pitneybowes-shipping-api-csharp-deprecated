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
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Globalization;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    /// <summary>
    /// Additional carrier account for a merchant to use with the PB Shipping APIs. The merchant 
    /// must have an existing account with the carrier. Currently, the API supports integration with UPS.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    internal class JsonCarrierAccount<T> : JsonWrapper<T>, ICarrierAccount, IShippingApiRequest where T : ICarrierAccount, new()
    {
        public class CarrierAccountParameter
        {
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("value")]
            public string Value { get; set; }
        }

        /// <summary>
        /// Your Pitney Bowes developer ID. To retrieve your developer ID, log into Developer Hub and click your 
        /// username and select Profile.
        /// </summary>
        /// <value></value>
        public string DeveloperId
        {
            get => Wrapped.DeveloperId;
            set => Wrapped.DeveloperId = value;
        }

        /// <summary>
        /// The unique ID used to identify the merchant. To retrieve the merchant’s postalReportingNumber, issue t
        /// he Get All Merchants API call.
        /// </summary>
        /// <value></value>
        public string PostalReportingNumber
        {
            get => Wrapped.PostalReportingNumber;
            set => Wrapped.PostalReportingNumber = value;
        }

        /// <summary>
        /// The carrier to add to the merchant’s account.
        /// </summary>
        /// <value></value>
        [ShippingApiQuery("carrier")]
        public Carrier Carrier
        {
            get => Wrapped.Carrier;
            set => Wrapped.Carrier = value;
        }

        /// <summary>
        /// A unique identifier for the transaction, up to 25 characters.
        /// </summary>
        /// <value></value>
        [ShippingApiHeader("x-pb-transactionId", true)]
        public string TransactionId
        {
            get => Wrapped.TransactionId;
            set => Wrapped.TransactionId = value;
        }        

        /// <summary>
        /// The merchant’s account number with the carrier.
        /// </summary>
        /// <value></value>
        [JsonProperty("accountNumber")]
        public string AccountNumber
        {
            get => Wrapped.AccountNumber;
            set => Wrapped.AccountNumber = value;
        }

        /// <summary>
        /// Required. The merchant’s “ship from” address as it appears on the carrier account.
        ///For UPS, this address must exactly match the “ship from” address on the merchant’s UPS invoice.
        /// </summary>
        /// <value></value>
        [JsonProperty("accountAddress")]
        public IAddress AccountAddress
        {
            get => Wrapped.AccountAddress;
            set => Wrapped.AccountAddress = value;
        }

        /// <summary>
        /// Required. The merchant’s contact address.
        /// </summary>
        /// <value></value>
        [JsonProperty("contactAddress")]
        public IAddress ContactAddress
        {
            get => Wrapped.ContactAddress;
            set => Wrapped.ContactAddress = value;
        }

        [JsonProperty("inputParameters")]
        public List<CarrierAccountParameter> inputParameters
        {
            get
            {
                var ip = new List<CarrierAccountParameter>();
                if (AccountCountryCode != null && AccountCountryCode != string.Empty)
                {
                    ip.Add( new CarrierAccountParameter() { Name = "ACCOUNT_COUNTRY_CODE", Value = AccountCountryCode } );
                }
                if (AccountPostalCode != null && AccountPostalCode != string.Empty)
                {
                    ip.Add( new CarrierAccountParameter() { Name = "ACCOUNT_POSTAL_CODE", Value = AccountPostalCode } );
                }
                if (ContactTitle != null && ContactTitle != string.Empty)
                {
                    ip.Add( new CarrierAccountParameter() { Name = "CONTACT_TITLE", Value = ContactTitle } );
                }
                if (DeviceIdentity != null && DeviceIdentity != string.Empty)
                {
                    ip.Add( new CarrierAccountParameter() { Name = "DEVICE_IDENTITY", Value = DeviceIdentity } );
                }
                if (EndUserIP != null && EndUserIP != string.Empty)
                {
                    ip.Add( new CarrierAccountParameter() { Name = "END_USER_IP", Value = EndUserIP } );
                }
                if (InvoiceAmount != 0.00M)
                {
                    ip.Add( new CarrierAccountParameter() { Name = "INVOICE_AMOUNT", Value = InvoiceAmount.ToString("#.00") } );
                }
                if (InvoiceControlId != null && InvoiceControlId != string.Empty)
                {
                    ip.Add( new CarrierAccountParameter() { Name = "INVOICE_CONTROL_ID", Value = InvoiceControlId } );
                }
                if (InvoiceCurrenyCode != null && InvoiceCurrenyCode != string.Empty)
                {
                    ip.Add( new CarrierAccountParameter() { Name = "INVOICE_CURRENCY_CODE", Value = InvoiceCurrenyCode } );
                }
                if (InvoiceDate != DateTimeOffset.MinValue)
                {
                    ip.Add( new CarrierAccountParameter() { Name = "INVOICE_DATE", Value = InvoiceDate.ToString("yyyyMMdd") } );
                }
                if (InvoiceNumber != null && InvoiceNumber != string.Empty)
                {
                    ip.Add( new CarrierAccountParameter() { Name = "INVOICE_NUMBER", Value = InvoiceNumber } );
                }
                if (LicenseText != null && LicenseText != string.Empty)
                {
                    ip.Add( new CarrierAccountParameter() { Name = "LICENSE_TEXT", Value = LicenseText } );
                }
                return ip;
            }
            set
            {
                foreach (var p in value)
                {
                    switch(p.Name)
                    {
                        case "ACCOUNT_COUNTRY_CODE":
                            AccountCountryCode = p.Value;
                        break;
                        case "ACCOUNT_POSTAL_CODE":
                            AccountPostalCode = p.Value;
                        break;
                        case "CONTACT_TITLE":
                            ContactTitle = p.Value;
                        break;
                        case "DEVICE_IDENTITY":
                            DeviceIdentity = p.Value;
                        break;
                        case "END_USER_IP":
                            EndUserIP = p.Value;
                        break;
                        case "INVOICE_AMOUNT":
                            InvoiceAmount = decimal.Parse(p.Value);
                        break;
                        case "INVOICE_CONTROL_ID":
                            InvoiceControlId = p.Value;
                        break;
                        case "INVOICE_CURRENCY_CODE":
                            InvoiceCurrenyCode = p.Value;
                        break;
                        case "INVOICE_DATE":
                            InvoiceDate = DateTimeOffset.ParseExact(p.Value, "yyyyMMdd", CultureInfo.InvariantCulture);
                        break;
                        case "INVOICE_NUMBER":
                            InvoiceNumber = p.Value;
                        break;
                        case "LICENSE_TEXT":
                            LicenseText = p.Value;
                        break;
                    }
                }
            }
        }

        public bool ShouldSerializeCarrierAccount() => false;

        [JsonProperty("carrierAccount")]
        public List<CarrierAccountParameter> CarrierAccount
        {
            get
            {
                var ip = new List<CarrierAccountParameter>();
                if (AccountNumber != null && AccountNumber != string.Empty)
                {
                    ip.Add( new CarrierAccountParameter() { Name = "ACCOUNT_NUMBER", Value = AccountNumber } );
                }
                if (UserId != null && UserId != string.Empty)
                {
                    ip.Add( new CarrierAccountParameter() { Name = "USER_ID", Value = UserId } );
                }
                if (Password != null && Password != string.Empty)
                {
                    ip.Add( new CarrierAccountParameter() { Name = "PASSWORD", Value = Password } );
                }
                if (Key != null && Key != string.Empty)
                {
                    ip.Add( new CarrierAccountParameter() { Name = "KEY", Value = Key } );
                }
                return ip;
            }
            set
            {
                foreach (var p in value)
                {
                    switch(p.Name)
                    {
                        case "ACCOUNT_NUMBER":
                            AccountNumber = p.Value;
                        break;
                        case "USER_ID":
                            UserId = p.Value;
                        break;
                        case "PASSWORD":
                            Password = p.Value;
                        break;
                        case "KEY":
                            Key = p.Value;
                        break;
                    }
                }
            }
        }


        /// <summary>
        /// The two-character ISO country code for the merchant’s country.
        /// </summary>
        /// <value></value>
        public string AccountCountryCode
        {
            get => Wrapped.AccountCountryCode;
            set => Wrapped.AccountCountryCode = value;
        }

        /// <summary>
        /// The merchant’s postal code.
        /// </summary>
        /// <value></value>
        public string AccountPostalCode
        {
            get => Wrapped.AccountPostalCode;
            set => Wrapped.AccountPostalCode = value;
        }

        /// <summary>
        /// The title of the primary contact that the merchant provided to the carrier.
        /// </summary>
        /// <value></value>
        public string ContactTitle
        {
            get => Wrapped.ContactTitle;
            set => Wrapped.ContactTitle = value;
        }

        /// <summary>
        /// The black-box identity of the merchant’s machine.
        /// For help in retrieving this information, consult your Pitney Bowes implementation engineer.
        /// </summary>
        /// <value></value>
        public string DeviceIdentity
        {
            get => Wrapped.DeviceIdentity;
            set => Wrapped.DeviceIdentity = value;
        }        

        /// <summary>
        /// The IP address of the merchant’s machine.
        /// </summary>
        /// <value></value>
        public string EndUserIP
        {
            get => Wrapped.EndUserIP;
            set => Wrapped.EndUserIP = value;
        }
    
        /// <summary>
        /// The amount charged on a recent invoice. The invoice must be from within the last 90 days. 
        /// Use the same invoice for all the INVOICE_* parameters in this table.
        /// The value for this parameter can have a maximum of 16 digits before the decimal and 2 digits after the decimal.
        /// </summary>
        /// <value></value>
        public decimal InvoiceAmount
        {
            get => Wrapped.InvoiceAmount;
            set => Wrapped.InvoiceAmount = value;
        }
    
        /// <summary>
        /// The Control ID displayed on the invoice.
        /// </summary>
        /// <value></value>
        public string InvoiceControlId
        {
            get => Wrapped.InvoiceControlId;
            set => Wrapped.InvoiceControlId = value;
        }

        /// <summary>
        /// The currency code used on the invoice.
        /// </summary>
        /// <value></value>
        public string InvoiceCurrenyCode
        {
            get => Wrapped.InvoiceCurrenyCode;
            set => Wrapped.InvoiceCurrenyCode = value;
        }

        /// <summary>
        /// The date of the invoice. The invoice date should be within the last 90 days.
        /// </summary>
        /// <value></value>
        public DateTimeOffset InvoiceDate
        {
            get => Wrapped.InvoiceDate;
            set => Wrapped.InvoiceDate = value;
        }

        /// <summary>
        /// The invoice number.
        /// </summary>
        /// <value></value>
        public string InvoiceNumber
        {
            get => Wrapped.InvoiceNumber;
            set => Wrapped.InvoiceNumber = value;
        }

        /// <summary>
        /// The text of the merchant’s license agreement with the carrier, as retrieved through the Get Carrier License Agreement API.
        /// </summary>
        /// <value></value>
        public string LicenseText
        {
            get => Wrapped.LicenseText;
            set => Wrapped.LicenseText = value;
        }

        /// <summary>
        /// User Id for subsequest API calls
        /// </summary>
        /// <value></value>
        public string UserId
        {
            get => Wrapped.UserId;
            set => Wrapped.UserId = value;
        }
      
        /// <summary> 
        /// Password for subsequest API calls
        /// </summary>
        /// <value></value>
        public string Password 
        {
            get => Wrapped.Password;
            set => Wrapped.Password = value;
        }

        /// <summary>
        /// API Key  for subsequest API calls
        /// </summary>
        /// <value></value>
        public string Key
        {
            get => Wrapped.Key;
            set => Wrapped.Key = value;
        }

        /// <summary>
        /// Response Only. The identifier to use when the merchant performs an operation that uses this 
        /// carrier account. The identifier is passed in the X-PB-Shipper-Carrier-AccountId request header 
        /// of the API request.
        /// </summary>
        /// <value></value>
        [JsonProperty("shipperCarrierAccountId")]
        public string ShipperCarrierAccountId
        {
            get => Wrapped.ShipperCarrierAccountId;
            set => Wrapped.ShipperCarrierAccountId = value;
        }

        public JsonCarrierAccount() : base() 
        {
            InvoiceDate = DateTimeOffset.MinValue;
        }

        public JsonCarrierAccount(T t) : base(t) 
        { 
            InvoiceDate = DateTimeOffset.MinValue;
        }

        public string RecordingSuffix => TransactionId;
        public string RecordingFullPath(string resource, ISession session)
        {
            return ShippingApiRequest.RecordingFullPath(this, resource, session);
        }

        public string GetUri(string baseUrl)
        {
            StringBuilder uri = new StringBuilder(baseUrl);
            ShippingApiRequest.SubstitueResourceParameters(this, uri);
            ShippingApiRequest.AddRequestQuery(this, uri);
            return uri.ToString();
        }

        public IEnumerable<Tuple<ShippingApiHeaderAttribute, string, string>> GetHeaders()
        {
            return ShippingApiRequest.GetHeaders(this);
        }

        public void SerializeBody(StreamWriter writer, ISession session)
        {
            ShippingApiRequest.SerializeBody(this, writer, session);
        }

        public string ContentType { get => "application/json"; }

        [ShippingApiHeader("Bearer", true)]
        public StringBuilder Authorization { get; set; }
    }
}