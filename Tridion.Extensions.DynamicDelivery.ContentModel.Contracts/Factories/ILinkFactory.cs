using System;
namespace Tridion.Extensions.DynamicDelivery.ContentModel.Factories
{
    public interface ILinkFactory
    {
        string ResolveLink(string componentUri);
        string ResolveLink(string sourcePageUri, string componentUri, string excludeComponentTemplateUri);
    }
}
