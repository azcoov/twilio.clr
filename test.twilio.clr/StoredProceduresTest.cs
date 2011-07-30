using Microsoft.VisualStudio.TestTools.UnitTesting;
using twilio.clr;

namespace test.twilio.clr
{
    [TestClass]
    public class StoredProceduresTest
    {
        public TestContext TestContext { get; set; }

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

        [TestMethod]
        public void SendSMSMessageTest()
        {
            const string apiVersion = "2010-04-01";
            const string accountSid = "fake_sid";
            const string authToken = "fake_auth_token";
            const string @from = "fake_from";
            const string to = "fake_to";
            const string body = "fake_body";
            string sid; //sid for new text message
            const string sidExpected = "SM90c6fc909d8504d45ecdb3a3d5b3556e"; // sid from sample xml http://www.twilio.com/docs/api/rest/sending-smsdocumentation

            var procedures = new StoredProcedures(new AccountMock());
            procedures.SendSMSMessage(apiVersion, accountSid, authToken, from, to, body, out sid);
            Assert.AreEqual(sidExpected, sid);
        }
    }
}
