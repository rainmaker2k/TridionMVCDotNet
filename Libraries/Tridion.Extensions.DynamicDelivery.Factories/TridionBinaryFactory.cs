using System;
using System.ComponentModel.Composition;
using System.Web;
using Tridion.Extensions.DynamicDelivery.ContentModel;
using Tridion.Extensions.DynamicDelivery.ContentModel.Exceptions;
using Tridion.Extensions.DynamicDelivery.ContentModel.Factories;

namespace Tridion.Extensions.DynamicDelivery.Factories
{
    [Export(typeof(IBinaryFactory))]
    /// <summary>
    /// This is a temporary PageFactory, to be replaced by the DynamicDelivery.PageFactory
    /// </summary>
    public class TridionBinaryFactory : TridionBinaryFactoryBase
    {
        
        
    }
}