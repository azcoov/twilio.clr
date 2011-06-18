using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using twilio.clr;

namespace test.twilio.clr
{
    [TestClass()]
    public class StoredProceduresTest
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void SendSMSMessageTest()
        {
            string apiVersion = "2010-04-01";
            string accountSid = "fake_sid";
            string authToken = "fake_auth_token";
            string from = "fake_from";
            string to = "fake_to";
            string body = "fake_body";
            string sid = String.Empty; //sid for new text message
            string sidExpected = "SM90c6fc909d8504d45ecdb3a3d5b3556e"; // sid from sample xml http://www.twilio.com/docs/api/rest/sending-smsdocumentation

            StoredProcedures procedures = new StoredProcedures(new Account_Mock());
            procedures.SendSMSMessage(apiVersion, accountSid, authToken, from, to, body, out sid);
            Assert.AreEqual(sidExpected, sid);
        }
    }
}
