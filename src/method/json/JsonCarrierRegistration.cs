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
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class JsonCarrierRegistration<T> : JsonWrapper<T>, ICarrierRegistration, IShippingApiRequest where T : ICarrierRegistration, new()
    {
        public JsonCarrierRegistration() : base() { }

        public JsonCarrierRegistration(T t) : base(t) { }

        [ShippingApiHeader("x-pb-transactionId", true)]
        public string TransactionId
        {
            get => Wrapped.TransactionId;
            set => Wrapped.TransactionId = value;
        }

        [ShippingApiQuery("developerId")]
        public string DeveloperId
        {
            get => Wrapped.DeveloperId;
            set => Wrapped.DeveloperId = value;
        }
        [ShippingApiQuery("postalReportingNumber")]
        public string PostalReportingNumber
        {
            get => Wrapped.PostalReportingNumber;
            set => Wrapped.PostalReportingNumber = value;
        }
        [ShippingApiQuery("carrier")]
        public Carrier Carrier {
            get => Wrapped.Carrier;
            set => Wrapped.Carrier = value;
        }
        [JsonProperty("accountNumber")]
        public string AccountNumber
        {
            get => Wrapped.AccountNumber;
            set => Wrapped.AccountNumber = value;
        }
        [JsonProperty("accountAddress")]
        public IAddress AccountAddress
        {
            get => Wrapped.AccountAddress;
            set => Wrapped.AccountAddress = value;
        }
        [JsonProperty("contactAddress")]
        public IAddress ContactAddress
        {
            get => Wrapped.ContactAddress;
            set => Wrapped.ContactAddress = value;
        }
        [JsonProperty("inputParameters")]
        public IEnumerable<IParameter> InputParameters
        {
            get => Wrapped.InputParameters;
            set => Wrapped.InputParameters = value;
        }
        [JsonProperty("carrierAccount")]
        public IEnumerable<IParameter> CarrierAccount
        {
            get => Wrapped.CarrierAccount;
            set => Wrapped.CarrierAccount = value;
        }
        [JsonProperty("shipperCarrierAccountId")]
        public string ShipperCarrierAccountId
        {
            get => Wrapped.ShipperCarrierAccountId;
            set => Wrapped.ShipperCarrierAccountId = value;
        }

        public IParameter AddCarrierAccount(IParameter parameter)
        {
            return Wrapped.AddCarrierAccount(parameter);
        }

        public IParameter AddInputParameter(IParameter parameter)
        {
            return Wrapped.AddInputParameter(parameter);
        }

        public string ContentType { get => "application/json"; }

        public string RecordingSuffix => TransactionId;

        public IEnumerable<Tuple<ShippingApiHeaderAttribute, string, string>> GetHeaders()
        {
            return ShippingApiRequest.GetHeaders(this);
        }

        [ShippingApiHeader("Bearer", true)]
        public StringBuilder Authorization { get; set; }

        public string GetUri(string baseUrl)
        {
            StringBuilder uri = new StringBuilder(baseUrl);
            ShippingApiRequest.SubstitueResourceParameters(this, uri);
            ShippingApiRequest.AddRequestQuery(this, uri);
            return uri.ToString();
        }

        public string RecordingFullPath(string resource, ISession session)
        {
            return ShippingApiRequest.RecordingFullPath(this, resource, session);
        }

        public void SerializeBody(StreamWriter writer, ISession session)
        {
            ShippingApiRequest.SerializeBody(this, writer, session);
        }
    }
}
