﻿namespace Tridion.Extensions.DynamicDelivery.ContentModel
{
    #region Usings
    using System.Collections.Generic;
    #endregion Usings

    public interface IPageTemplate : IRepositoryLocal
    {
        string FileExtension { get; }
        IOrganizationalItem Folder { get; }
        IDictionary<string, IField> MetadataFields { get; }
    }
}
