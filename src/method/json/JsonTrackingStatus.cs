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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class JsonTrackingStatus<T> : JsonWrapper<T>, ITrackingStatus where T : ITrackingStatus, new()
    {
        public JsonTrackingStatus() : base() { }
        public JsonTrackingStatus(T t) : base(t) { }

        [JsonProperty("packageCount")]
        public string PackageCount
        {
            get => Wrapped.PackageCount;
            set { Wrapped.PackageCount = value; }
        }
        [JsonProperty("carrier")]
        public string Carrier
        {
            get => Wrapped.Carrier;
            set { Wrapped.Carrier = value; }
        }
        [JsonProperty("trackingNumber")]
        public string TrackingNumber
        {
            get => Wrapped.TrackingNumber;
            set { Wrapped.TrackingNumber = value; }
        }
        [JsonProperty("referenceNumber")]
        public string ReferenceNumber
        {
            get => Wrapped.ReferenceNumber;
            set { Wrapped.ReferenceNumber = value; }
        }
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TrackingStatusCode Status
        {
            get => Wrapped.Status;
            set { Wrapped.Status = value; }
        }
        [JsonIgnore]
        public DateTimeOffset UpdatedDateTime
        {
            get;
            set;
        }

        [JsonProperty("updatedDate")]
        public DateTimeOffset UpdatedDate
        {
            get => Wrapped.UpdatedDateTime.Date;
            set
            {
                var t = Wrapped.UpdatedDateTime.TimeOfDay;
                Wrapped.UpdatedDateTime = value.Date + t;
            }
        }
        [JsonProperty("updatedTime")]
        public DateTimeOffset UpdatedTime
        {
            get => DateTimeOffset.MinValue + Wrapped.UpdatedDateTime.TimeOfDay;
            set
            {
                Wrapped.UpdatedDateTime = Wrapped.UpdatedDateTime.Date + value.TimeOfDay;
            }
        }
        [JsonProperty("shipDate")]
        public DateTimeOffset ShipDateTime
        {
            get => Wrapped.ShipDateTime;
            set { Wrapped.ShipDateTime = value; }
        }
        [JsonIgnore]
        public DateTimeOffset EstimatedDeliveryDateTime
        {
            get;
            set;
        }
        [JsonProperty("estimatedDeliveryDate")]
        public DateTimeOffset EstimatedDeliveryDate
        {
            get => Wrapped.EstimatedDeliveryDateTime.Date;
            set
            {
                var t = Wrapped.EstimatedDeliveryDateTime.TimeOfDay;
                Wrapped.EstimatedDeliveryDateTime = value.Date + t;
            }

        }
        [JsonProperty("estimatedDeliveryTime")]
        public DateTimeOffset EstimatedDeliveryTime
        {
            get => DateTimeOffset.MinValue + Wrapped.EstimatedDeliveryDateTime.TimeOfDay;
            set
            {
                Wrapped.EstimatedDeliveryDateTime = Wrapped.EstimatedDeliveryDateTime.Date + value.TimeOfDay;
            }
        }
        [JsonIgnore]
        public DateTimeOffset DeliveryDateTime
        {
            get;
            set;
        }
        [JsonProperty("deliveryDate")]
        public DateTimeOffset DeliveryDate
        {
            get => Wrapped.DeliveryDateTime.Date;
            set
            {
                var t = Wrapped.DeliveryDateTime.TimeOfDay;
                Wrapped.DeliveryDateTime = value.Date + t;
            }
        }
        [JsonProperty("deliveryTime")]
        public DateTimeOffset DeliveryTime
        {
            get => DateTimeOffset.MinValue + Wrapped.DeliveryDateTime.TimeOfDay;
            set
            {
                Wrapped.DeliveryDateTime = Wrapped.DeliveryDateTime.Date + value.TimeOfDay;
            }
        }
        [JsonProperty("deliveryLocation")]
        public string DeliveryLocation
        {
            get => Wrapped.DeliveryLocation;
            set { Wrapped.DeliveryLocation = value; }
        }
        [JsonProperty("deliveryLocationDescription")]
        public string DeliveryLocationDescription
        {
            get => Wrapped.DeliveryLocationDescription;
            set { Wrapped.DeliveryLocationDescription = value; }
        }
        [JsonProperty("signedBy")]
        public string SignedBy
        {
            get => Wrapped.SignedBy;
            set { Wrapped.SignedBy = value; }
        }
        [JsonProperty("weight")]
        public Decimal Weight
        {
            get => Wrapped.Weight;
            set { Wrapped.Weight = value; }
        }
        [JsonProperty("weightOUM")]
        public UnitOfWeight? WeightOUM
        {
            get => Wrapped.WeightOUM;
            set { Wrapped.WeightOUM = value; }
        }
        [JsonProperty("reattemptDate")]
        public string ReattemptDate
        {
            get => Wrapped.ReattemptDate;
            set { Wrapped.ReattemptDate = value; }
        }
        [JsonProperty("reattemptTime")]
        public DateTime ReattemptTime
        {
            get => Wrapped.ReattemptTime;
            set { Wrapped.ReattemptTime = value; }
        }
        [JsonProperty("destinationAddress")]
        public IAddress DestinationAddress
        {
            get => Wrapped.DestinationAddress;
            set { Wrapped.DestinationAddress = value; }
        }
        [JsonProperty("senderAddress")]
        public IAddress SenderAddress
        {
            get => Wrapped.SenderAddress;
            set { Wrapped.SenderAddress = value; }
        }
        [JsonProperty("scanDetailsList")]
        public IEnumerable<ITrackingEvent> ScanDetailsList
        {
            get => Wrapped.ScanDetailsList;
            set { Wrapped.ScanDetailsList = value; }
        }
    }
}
