using Tridion.Extensions.DynamicDelivery.ContentModel.Factories;

namespace Tridion.Extensions.DynamicDelivery.Factories
{
    using System.ComponentModel.Composition;

    [Export(typeof(ITaxonomyFactory))]
    public class TridionTaxonomyFactory : TridionTaxonomyFactoryBase
    {
    }
}

