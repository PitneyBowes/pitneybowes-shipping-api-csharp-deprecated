// /*
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
// */
using System;
using System.Collections.Generic;
using PitneyBowes.Developer.ShippingApi;


namespace PitneyBowes.Developer.ShippingApi.Fluent
{
    /// <summary>
    /// UPS extensions.
    /// </summary>
    public static class UPSExtensions
    {
        /// <summary>
        /// Add special services without parameters
        /// </summary>
        /// <typeparam name="T">Rates type</typeparam>
        /// <typeparam name="S">Special Service Type</typeparam>
        /// <param name="ratesArray"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static RatesArrayFluent<T> UPSService<T,S>(this RatesArrayFluent<T> ratesArray, SpecialServiceCodes c) 
            where S : ISpecialServices, new()
            where T : class, IRates, new()
        {
            var s = new S() { SpecialServiceId = c };
            return ratesArray;
        }
        /// <summary>
        /// Cash on delivery paid by cashier check.
        /// </summary>
        /// <typeparam name="T">Rate type</typeparam>
        /// <typeparam name="S">Special Service Type</typeparam>
        /// <typeparam name="P">Parameter Type</typeparam>
        /// <param name="ratesArray">this</param>
        /// <param name="currency">Currency</param>
        /// <param name="val">Amount</param>
        /// <returns></returns>
        public static RatesArrayFluent<T> CODCashier<T, S, P>(this RatesArrayFluent<T> ratesArray, string currency, decimal val)
            where S : ISpecialServices, new()
            where T : class, IRates, new()
            where P : IParameter, new()
        {
            var p = new List<IParameter>(2) { new P() { Name = "CURRENCY", Value = currency }, new P() { Name = "INPUT_VALUE", Value = val.ToString() } };
            var s = new S() { SpecialServiceId = SpecialServiceCodes.COD_CASHIER, InputParameters = p };
            ratesArray.SpecialService<S>(s);
            return ratesArray;
        }

        /// <summary>
        /// Cash on delivery paid by check.
        /// </summary>
        /// <typeparam name="T">Rate type</typeparam>
        /// <typeparam name="S">Special Service Type</typeparam>
        /// <typeparam name="P">Parameter Type</typeparam>
        /// <param name="ratesArray">this</param>
        /// <param name="currency">Currency</param>
        /// <param name="val">Amount</param>
        /// <returns></returns>
        public static RatesArrayFluent<T> CODCheck<T, S, P>(this RatesArrayFluent<T> ratesArray, string currency, decimal val)
            where S : ISpecialServices, new()
            where T : class, IRates, new()
            where P : IParameter, new()
        {
            var p = new List<IParameter>(2) { new P() { Name = "CURRENCY", Value = currency }, new P() { Name = "INPUT_VALUE", Value = val.ToString() } };
            var s = new S() { SpecialServiceId = SpecialServiceCodes.COD_CHECK, InputParameters = p };
            ratesArray.SpecialService<S>(s);
            return ratesArray;
        }
        /// <summary>
        /// Dry Ice service
        /// </summary>
        /// <typeparam name="T">Rate type</typeparam>
        /// <typeparam name="S">Special Service Type</typeparam>
        /// <typeparam name="P">Parameter Type</typeparam>
        /// <param name="ratesArray">this</param>
        /// <param name="weightUOM">Unit of measure "OZS" for ounces</param>
        /// <param name="val">Weight</param>
        /// <returns></returns>
        public static RatesArrayFluent<T> DryIce<T, S, P>(this RatesArrayFluent<T> ratesArray, string weightUOM, decimal val)
            where S : ISpecialServices, new()
            where T : class, IRates, new()
            where P : IParameter, new()
        {
            var p = new List<IParameter>(2) { new P() { Name = "WEIGHT_UOM", Value = weightUOM }, new P() { Name = "WEIGHT", Value = val.ToString() } };
            var s = new S() { SpecialServiceId = SpecialServiceCodes.DRY_ICE, InputParameters = p };
            ratesArray.SpecialService<S>(s);
            return ratesArray;
        }
        /// <summary>
        /// Declared value for insurance
        /// </summary>
        /// <typeparam name="T">Rate type</typeparam>
        /// <typeparam name="S">Special Service Type</typeparam>
        /// <typeparam name="P">Parameter Type</typeparam>
        /// <param name="ratesArray">this</param>
        /// <param name="val">Decalred value</param>
        /// <returns></returns>
        public static RatesArrayFluent<T> DeclaredValue<T, S, P>(this RatesArrayFluent<T> ratesArray, decimal val)
            where S : ISpecialServices, new()
            where T : class, IRates, new()
            where P : IParameter, new()
        {
            var p = new List<IParameter>(1) { new P() { Name = "INPUT_VALUE", Value = val.ToString() } };
            var s = new S() { SpecialServiceId = SpecialServiceCodes.INS, InputParameters = p };
            ratesArray.SpecialService<S>(s);
            return ratesArray;
        }
        /// <summary>
        /// Print a return label
        /// </summary>
        /// <typeparam name="T">Rate type</typeparam>
        /// <typeparam name="S">Special Service Type</typeparam>
        /// <typeparam name="P">Parameter Type</typeparam>
        /// <param name="ratesArray">this</param>
        /// <param name="val">Description of returned itemse</param>
        /// <returns></returns>
        public static RatesArrayFluent<T> PrintReturnLabel<T, S, P>(this RatesArrayFluent<T> ratesArray, string val)
            where S : ISpecialServices, new()
            where T : class, IRates, new()
            where P : IParameter, new()
        {
            var p = new List<IParameter>(1) { new P() { Name = "RETURN_PKG_DESCRIPTION", Value = val } };
            var s = new S() { SpecialServiceId = SpecialServiceCodes.PRL, InputParameters = p };
            ratesArray.SpecialService<S>(s);
            return ratesArray;
        }
        /// <summary>
        /// Verbal confirmation of delivery
        /// </summary>
        /// <typeparam name="T">Rate type</typeparam>
        /// <typeparam name="S">Special Service Type</typeparam>
        /// <typeparam name="P">Parameter Type</typeparam>
        /// <param name="ratesArray">this</param>
        /// <param name="val">Phone number</param>
        /// <returns></returns>
        public static RatesArrayFluent<T> VerbalConfirmation<T, S, P>(this RatesArrayFluent<T> ratesArray, string val)
            where S : ISpecialServices, new()
            where T : class, IRates, new()
            where P : IParameter, new()
        {
            var p = new List<IParameter>(1) { new P() { Name = "VERBAL_CONF_PHONE", Value = val } };
            var s = new S() { SpecialServiceId = SpecialServiceCodes.PRL, InputParameters = p };
            ratesArray.SpecialService<S>(s);
            return ratesArray;
        }
    }

}

