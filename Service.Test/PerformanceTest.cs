using System;
using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Service.Test
{
    [TestClass]
    public class PerformanceTest
    {
        [TestMethod]
        public void TestServiceStack()
        {
            Test("http://localhost:100/user/{0}?format=json");
        }

        [TestMethod]
        public void TestODataOptimized()
        {
            Test("http://localhost:300/ODataService.svc/Users({0}L)?$expand=Contacts/ContactGroups&$format=json");
        }

        [TestMethod]
        public void TestOData()
        {
            Test("http://localhost:200/ODataService.svc/Users({0}L)?$expand=Contacts/ContactGroups&$format=json");
        }

        private void Test(string url)
        {
            int i = 5000;

            var rand = new Random();

            DateTime start = DateTime.UtcNow;
            int errorCount = 0;
            while (i-- > 0)
            {

                int randUser = rand.Next(0, 100000);

                var request = (HttpWebRequest) WebRequest.Create(string.Format(url, randUser));

                try
                {
                    // execute the request
                    using (var response = (HttpWebResponse) request.GetResponse())
                    {
                        using (var sr = new StreamReader(response.GetResponseStream()))
                        {
                            sr.ReadToEnd();
                        }
                    }
                }
                catch (Exception)
                {
                    errorCount++;
                }
            }

            var timeSpan = DateTime.UtcNow.Subtract(start);

            Assert.Inconclusive("test took: " + timeSpan + " - Error Count:" + errorCount);
        }
    }
}
