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

namespace PitneyBowes.Developer.ShippingApi
{
    internal class ServicesConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Services).Equals(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch(reader.Value)
            {
                case "2DA":
                    return Services.UPS_2DA;
                case "2DA_AM":
                    return Services.UPS_2DA_AM;
                case "3DA":
                    return Services.UPS_3DA;
                case "3DA_USA":
                    return Services.UPS_3DA_USA;
                default:
                    var converter = new StringEnumConverter();
                    return converter.ReadJson(reader, objectType, existingValue, serializer);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!typeof(Services).Equals(value.GetType())) throw new InvalidOperationException(string.Format("Can't use a ServicesConverter to serialize {0}", value.GetType().ToString())); 
            var s = (Services)value;
            switch(s)
            {
                case Services.UPS_2DA:
                    writer.WriteValue("2DA");
                    break;
                case Services.UPS_2DA_AM:
                    writer.WriteValue("2DA_AM");
                    break;
                case Services.UPS_3DA:
                    writer.WriteValue("3DA");
                    break;
                case Services.UPS_3DA_USA:
                    writer.WriteValue("3DA_USA");
                    break;
                default:
                    var converter = new StringEnumConverter();
                    converter.WriteJson(writer, value, serializer);
                    break;
            }
        }
    }

}