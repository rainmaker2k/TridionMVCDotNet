using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tridion.Extensions.DynamicDelivery.ContentModel.Factories;
using System.ComponentModel.Composition;

namespace Tridion.Extensions.DynamicDelivery.Factories.LocalDatabase
{
    [Export(typeof(IComponentFactory))]
    public class ComponentFactory : IComponentFactory
    {
        #region IComponentFactory Members

        public bool TryGetComponent(string componentUri, out ContentModel.IComponent component)
        {
            throw new NotImplementedException();
        }

        public ContentModel.IComponent GetComponent(string componentUri)
        {
            throw new NotImplementedException();
        }

        public IList<ContentModel.IComponent> FindComponents(string schemaUri)
        {
            throw new NotImplementedException();
        }

        public IList<ContentModel.IComponent> FindComponents(string[] schemaUris)
        {
            throw new NotImplementedException();
        }

        public IList<ContentModel.IComponent> FindComponents(string[] schemaUris, DateTime sinceLastPublished)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, ContentModel.IComponentMeta> FindComponentMetas(string[] schemaUri)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, ContentModel.IComponentMeta> FindComponentMetas(string[] schemaUri, DateTime sinceLastPublished)
        {
            throw new NotImplementedException();
        }

        public ContentModel.IComponent GetLastPublishedComponent(string schemaUri)
        {
            throw new NotImplementedException();
        }

        public DateTime LastPublished(string[] schemaUris)
        {
            throw new NotImplementedException();
        }

        #endregion

		#region IComponentFactory Members


		public IList<string> FindComponents(ExtendedQueryParameters queryParameters) {
			throw new NotImplementedException();
		}

		#endregion
	}
}
