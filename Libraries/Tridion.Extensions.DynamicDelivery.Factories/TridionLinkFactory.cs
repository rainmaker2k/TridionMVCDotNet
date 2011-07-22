using Tridion.Extensions.DynamicDelivery.ContentModel.Factories;

namespace Tridion.Extensions.DynamicDelivery.Factories
{
    using System.ComponentModel.Composition;

    [Export(typeof(ILinkFactory))]
    public class TridionLinkFactory : TridionLinkFactoryBase
    {
        
    }
}

