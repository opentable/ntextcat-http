# ntextcat-http
A NancyFx wrapper around the NTextCat project to expose language translations as a web service. The original project can be found at http://ntextcat.codeplex.com/. 

[![Build status](https://ci.appveyor.com/api/projects/status/u2riv0c1dlxvvx3f?svg=true)](https://ci.appveyor.com/project/DafyddGiddins/ntextcat-http)

## Getting Started
You can install this from nuget into any hosting project that supports nancy hosting via nuget.

```
PM> Install-Package NTextCat.Http
```

The nuget package will install a view into the view directory and add the correct settings to your web.config or app.config for the location of the language profile. This will also be copied to your project. You can however use the ntextcat project to generate new profiles based on your own data or use the additional profiles the original ntextcat project comes with.

To learn about Nancy hosting, please check out the Nancy documentation:  https://github.com/NancyFx/Nancy/wiki/Documentation#hosting

## Usage
The language detection service is exposed at the endpoint /language-detector. If you HTTP GET this endpoint you will get a test page with a form to allow you submit text for language classification. This same endpoing supports application/xml and application/json for API integration. When using the API you will need to HTTP POST to the endpoint with a simple model in either xml or Json as shown below:

Example in Json

```json
{"TextForLanguageClassification":"this is some english"}
```

Example in xml

```xml
<?xml version="1.0" encoding="utf-8"?>
<LanguageDetectRequest>
<TextForLanguageClassification>this is some english</TextForLanguageClassification>
</LanguageDetectRequest>
```

The response object will be an ordered list of Iso6393 language codes with the most likley match being the topmost in the response list. A truncated example of a response is show below:

```json
{
    "originalText": "this is some english",
    "rankedMatches": [
        {
            "iso6393LanguageCode": "eng",
            "matchScore": 3953.2965
        },
        {
            "iso6393LanguageCode": "nor",
            "matchScore": 3965.98075
        },
        ...
    ]
}
```

and in xml

```xml
<?xml version="1.0"?>
<DetectedLanguageResponse 
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
    xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <OriginalText>this is some english</OriginalText>
    <RankedMatches>
        <DetectedLangage>
            <Iso6393LanguageCode>eng</Iso6393LanguageCode>
            <MatchScore>3953.2965</MatchScore>
        </DetectedLangage>
        <DetectedLangage>
            <Iso6393LanguageCode>nor</Iso6393LanguageCode>
            <MatchScore>3965.98075</MatchScore>
        </DetectedLangage>
        ...
    </RankedMatches>
</DetectedLanguageResponse>
```