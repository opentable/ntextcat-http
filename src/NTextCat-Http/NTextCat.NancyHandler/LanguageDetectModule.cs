using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using NTextCat.NancyHandler.LanguageDetection;

namespace NTextCat.NancyHandler
{
    public class LanguageDetectModule : NancyModule
    {
        public const string LanguageDetectionViewName = "language-detection-test-page";

        public LanguageDetectModule(LanguageDetector languageDetector)
            : base("/language-detector")
        {
            Get["/"] = _ =>
            {
                return View[LanguageDetectionViewName, new DetectedLanguageResponse()];
            };

            Post["/"] = _ =>
            {
                var model = this.Bind<LanguageDetectRequest>();
                if (ModelIsInvalid(model))
                    return BadRequest();

                DetectedLanguageResponse detectedLanguageResponse = languageDetector.DetectLanguage(model);

                //return Response.AsXml(detectedLanguageResponse);

                return Negotiate
                    .WithModel(detectedLanguageResponse)
                    .WithView(LanguageDetectionViewName);
            };

        }
        
        private Negotiator BadRequest()
        {
            return Negotiate.
                WithView(LanguageDetectionViewName).
                WithStatusCode(HttpStatusCode.BadRequest);
        }

        private static bool ModelIsInvalid(LanguageDetectRequest model)
        {
            return model == null || string.IsNullOrWhiteSpace(model.TextForLanguageClassification);
        }
    }
}
