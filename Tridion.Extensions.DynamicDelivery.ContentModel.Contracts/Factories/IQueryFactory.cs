using System;
using System.Collections.Generic;
namespace Tridion.Extensions.DynamicDelivery.ContentModel.Factories
{
    public interface IQueryFactory
    {
        IList<string> FindComponents(string[] basedOnSchemas, DateTime lastPublishedDate);
        IComponentMeta GetComponentMeta(string componentUri);
    }
}
