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

namespace PitneyBowes.Developer.ShippingApi
{
    /// <summary>
    /// This operation authorizes a merchant to use an additional carrier for use with a merchant’s Pitney Bowes Account. 
    /// The merchant must have an existing account with the carrier. Currently, this API supports integration with UPS.
    /// For step-by-step implementation of the API, please see the Carrier Registration Tutorial.
    /// When you issue this API call, Pitney Bowes verifies the merchant’s account with the carrier using information you 
    /// send in the API request.The carrier sends back to Pitney Bowes a unique set of credentials to use with the merchant’s 
    /// Pitney Bowes account.Pitney Bowes returns the credentials in the response to this API call but also returns a unique 
    /// shipperCarrierAccountId you use instead of the credentials. When the merchant performs an action that uses this carrier, 
    /// you need only pass the shipperCarrierAccountId. Pitney Bowes uses the ID to locate and send the required credentials 
    /// to the carrier.You pass the ID in the X-PB-Shipper-Carrier-AccountId request header of an API request.
    /// </summary>
    public interface ICarrierLicense
    {
        /// <summary>
        /// Required. The carrier.
        /// Valid value:
        ///   UPS
        /// </summary>
        Carrier Carrier { get; set; }
        /// <summary>
        /// ISO Country code
        /// </summary>
        string OriginCountryCode { get; set; }
        /// <summary>
        /// License Text
        /// </summary>
        string LicenseText { get; set; }
    }
}
