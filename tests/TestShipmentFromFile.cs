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

using PitneyBowes.Developer.ShippingApi;
using System;
using System.IO;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace tests
{
    public class TestShipmentFromFile : TestSession
    {
        public TestShipmentFromFile(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            InitializeFramework();
        }

        [Fact]
        public void TestAddressesVerify()
        {
            ShippingApiResponse response = TestFile("addresses_verify.txt");
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseObject);
        }

        [Fact]
        public void TestCubaEMIPkg()
        {
            ShippingApiResponse response = TestFile("cuba_emi_pkg.txt");
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseObject);
        }        
        
        [Fact]
        public void TestNewgistics()
        {
            ShippingApiResponse response = TestFile("Newgistics.txt");
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseObject);
        }        
        
        [Fact]
        public void TestPrintLabelCubic()
        {
            ShippingApiResponse response = TestFile("printLabel_Cubic.txt");
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseObject);
        }        
        
        [Fact]
        public void TestPrintLabelDomesticAdhoc()
        {
            ShippingApiResponse response = TestFile("printLabel_Domestic_Adhoc.txt");
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseObject);
        }        
        
        [Fact]
        public void TestPrintLabelDomesticValueOfGoods()
        {
            ShippingApiResponse response = TestFile("printLabel_Domestic_valueOfGoods.txt");
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseObject);
        }        
        
        [Fact]
        public void TestPrintLabelSurchargeAdhoc()
        {
            ShippingApiResponse response = TestFile("printLabel_surcharge_Adhoc.txt");
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseObject);
        }

        [Fact]
        public void TestStandardMail()
        {
            ShippingApiResponse response = TestFile("StdMail.txt");
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseObject);
        }

        //[Fact]   // need to have UPS account set up
        public void TestUPS()
        {
            ShippingApiResponse response = TestFile("UPS.txt");
            Assert.NotNull(response);
            Assert.NotNull(response.ResponseObject);
        }
        private static ShippingApiResponse TestFile(string fileName)
        {
            var pwd = Directory.GetCurrentDirectory();
            var directories = pwd.Split(Path.DirectorySeparatorChar);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < directories.Length - 3; i++)
            {
                sb.Append(directories[i]);
                sb.Append(Path.DirectorySeparatorChar);
            }
            sb.Append("testData/");
            sb.Append(fileName);
            var file = sb.ToString();

            Console.Write("Testing: ");
            Console.WriteLine(file);
            ShippingApiResponse response;
            try
            {
                response = FileRequest.Request(file, Globals.DefaultSession);
            }
            catch (Exception)
            {

                throw;
            }

            return response;
        }
    }
}
