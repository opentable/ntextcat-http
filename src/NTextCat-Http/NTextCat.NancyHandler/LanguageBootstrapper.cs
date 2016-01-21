using System;
using Nancy;
using Nancy.TinyIoc;
using NTextCat.NancyHandler.LanguageDetection;

namespace NTextCat.NancyHandler
{
    public class LanguageBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<LanguageDetector>().AsSingleton();
        }
    }
}

