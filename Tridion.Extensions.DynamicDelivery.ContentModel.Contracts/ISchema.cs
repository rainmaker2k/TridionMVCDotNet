namespace Tridion.Extensions.DynamicDelivery.ContentModel
{
    public interface ISchema : IRepositoryLocal
    {
        IOrganizationalItem Folder { get; }
    }
}
