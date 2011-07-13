using System.Collections.Generic;
namespace Tridion.Extensions.DynamicDelivery.ContentModel
{
    public interface IKeyword : IRepositoryLocal
    {
        string Path { get; }
        string TaxonomyId { get; }
        IList<IKeyword> ParentKeywords { get;}        
    }
}
