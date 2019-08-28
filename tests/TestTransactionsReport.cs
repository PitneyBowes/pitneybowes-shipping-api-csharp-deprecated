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
using System.Linq;
using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Model;
using Xunit;
using Xunit.Abstractions;

namespace tests
{
    public class TestTransactionsReport : TestSession
    {
        public TestTransactionsReport(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            InitializeFramework();
        }

        [Fact]
        public void TransactionReport()
        {
            var transactionsReportRequest = new ReportRequest()
            {
                FromDate = DateTimeOffset.Parse("6/30/2019"),
                ToDate = DateTimeOffset.Now,
                DeveloperId = Globals.DefaultSession.GetConfigItem("DeveloperID")
            };
            int count = 0;
            foreach (var t in TransactionsReport<Transaction>.Report(transactionsReportRequest, x => x.CreditCardFee == null || x.CreditCardFee > 10.0M, maxPages: 2))
            {
                count++;
            }
        }

        [Fact]
        public void TransactionReportLinq()
        {
            // Transaction report with LINQ
            TransactionsReport<Transaction> report = new TransactionsReport<Transaction>(Globals.DefaultSession.GetConfigItem("DeveloperID"), maxPages: 2);
            var query = from transaction in report
                        where transaction.TransactionDateTime >= DateTimeOffset.Parse("6/30/2019") && transaction.TransactionDateTime <= DateTimeOffset.Now && transaction.TransactionType == TransactionType.POSTAGE_PRINT
                        select new { transaction.TransactionId, transaction.ParcelTrackingNumber };
            int count = 0;
            string tracking;
            foreach (var obj in query)
            {
                count++;
                tracking = obj.ParcelTrackingNumber;
            }
        }
    }
}
