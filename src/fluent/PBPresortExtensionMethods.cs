//
// Copyright 2016 Pitney Bowes Inc.
//
// Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
// You may obtain a copy of the License in the README file or at
//    https://opensource.org/licenses/MIT 
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
// for the specific language governing permissions and limitations under the License.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
// THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
// OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//


namespace PitneyBowes.Developer.ShippingApi.Fluent
{
    /// <summary>
    /// PBPresort extensions.
    /// </summary>
    public static class PBPresortExtensions
    {
        /// <summary>
        /// PBPresort options.
        /// </summary>
        /// <returns>The options.</returns>
        /// <param name="f">The object.</param>
        /// <param name="permitNumber">Client facility.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static ShipmentOptionsArrayFluent<T> PBPresortPermit<T>(
            this ShipmentOptionsArrayFluent<T> f,
            string permitNumber 
            ) where T : class, IShipmentOptions, new()
        {
            f.Option(ShipmentOption.PERMIT_NUMBER, permitNumber);
            return f;
        }
        /// <summary>
        /// Set necessary parameters for PB Presort shipment
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="groupId">ShipmentGroupId</param>
        /// <param name="integratorCarrier">IntegratorCarrierId</param>
        /// <returns></returns>
        public static ShipmentFluent<T> PBPresortShipment<T>(
            this ShipmentFluent<T> s,
            string groupId,
            string integratorCarrier ) where T: class, IShipment, new()
        {
            var shipment = (IShipment)s;
            shipment.IntegratorCarrierId = integratorCarrier;
            shipment.ShipmentGroupId = groupId;
            return s;
        }
    }
}
