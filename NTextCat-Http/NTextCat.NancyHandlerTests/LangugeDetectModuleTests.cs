using Nancy;
using Nancy.Testing;
using NTextCat.NancyHandler;
using NUnit.Framework;

namespace NTextCat.NancyHandlerTests
{
    [TestFixture]
    public class LangugeDetectModuleTests
    {
        [Test]
        public void WhenNoBodyBadRequest()
        {
            var browser = new Browser(with => with.Module(new LanguageDetectModule()));

            var response = browser.Post("/language-detector");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public void WhenBodyInvalidBadRequest()
        {
            var browser = new Browser(with => with.Module(new LanguageDetectModule()));

            var response = browser.Post("/language-detector", with =>
            {
                with.Body("random");
            });
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public void WhenNotCorrectFormatBadRequest()
        {

        }

        [Test]
        public void WhenBodyCorrectOkResponse()
        {
            var browser = new Browser(with => with.Module(new LanguageDetectModule()));

            var response = browser.Post("/language-detector", with =>
            {
                with.Header("Accept", "application/json");
                with.JsonBody(new LanguageDetectRequest() { TextForLanguageClassification = "some words" });
            });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void TestSupportsXml()
        {
            var browser = new Browser(with => with.Module(new LanguageDetectModule()));

            var response = browser.Post("/language-detector", with =>
            {
                with.Header("Accept", "application/xml");
                with.JsonBody(new LanguageDetectRequest() { TextForLanguageClassification = "some words" });
            });

            var body = response.Body.DeserializeXml<DetectedLanguageResponse>();

            Assert.That(body, Is.Not.Null);
        }

        [Test]
        public void TestSupportsJson()
        {
            var browser = new Browser(with => with.Module(new LanguageDetectModule()));

            var response = browser.Post("/language-detector", with =>
            {
                with.Header("Accept", "application/json");
                with.JsonBody(new LanguageDetectRequest() { TextForLanguageClassification = "some words" });
            });

            var body = response.Body.DeserializeJson<DetectedLanguageResponse>();

            Assert.That(body, Is.Not.Null);
        }

        [Test]
        public void TestName()
        {

        }
    }
}
