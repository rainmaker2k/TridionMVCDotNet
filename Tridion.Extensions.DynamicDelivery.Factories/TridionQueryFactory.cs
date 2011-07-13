using System;
using System.ComponentModel.Composition;
using System.Xml;
using System.Xml.Serialization;
using Tridion.Extensions.DynamicDelivery.Factories;
using Tridion.Extensions.DynamicDelivery.ContentModel;
using Tridion.Extensions.DynamicDelivery.ContentModel.Exceptions;
using Tridion.Extensions.DynamicDelivery.ContentModel.Factories;
using Tridion.ContentDelivery.DynamicContent.Query;

namespace Tridion.Extensions.DynamicDelivery.Factories
{
    [Export(typeof(IQueryFactory))]
    /// <summary>
    /// 
    /// </summary>
    public class TridionQueryFactory : TridionFactoryBase, IQueryFactory
    {
        /// <summary>
        /// Finds components based on the given schema's
        /// </summary>
        /// <param name="basedOnSchemas"></param>
        /// <returns>Array with URI's of components</returns>

        public System.Collections.Generic.IList<string> FindComponents(string[] basedOnSchemas, DateTime lastPublishedDate)
        {
            Query q = null;
            PublicationCriteria publicationAndLastPublishedDateCriteria = new PublicationCriteria(PublicationId);
            //format DateTime // 00:00:00.000
            ItemLastPublishedDateCriteria dateLastPublished = new ItemLastPublishedDateCriteria(lastPublishedDate.ToString(), Criteria.GreaterThanOrEqual);
            publicationAndLastPublishedDateCriteria.AddCriteria(dateLastPublished);

            Criteria[] schemaCriterias = new Criteria[basedOnSchemas.Length];
            int i = 0;
            foreach (var schema in basedOnSchemas)
            {
                TcmUri schemaUri = new TcmUri(schema);
                schemaCriterias.SetValue(new ItemSchemaCriteria(schemaUri.ItemId), i);
                i++;
            }
            Criteria basedOnSchema = CriteriaFactory.Or(schemaCriterias);
            Criteria basedOnSchemaAndInPublication = CriteriaFactory.And(publicationAndLastPublishedDateCriteria, basedOnSchema);


            q = new Query(basedOnSchemaAndInPublication);

            SortParameter sortParameter = new SortParameter(SortParameter.ItemLastPublishedDate, SortParameter.Descending);
            q.AddSorting(sortParameter);
            string[] results = q.ExecuteQuery();


            return results;
        }

        /// <summary>
        /// Gets the meta information for a given component
        /// </summary>
        /// <param name="componentUri"></param>
        /// <returns>ComponentMeta object holding the meta information</returns>
        public IComponentMeta GetComponentMeta(string componentUri)
        {
            ComponentMeta compMeta = new ComponentMeta();
            using (Com.Tridion.Util.TCMURI uri = new Com.Tridion.Util.TCMURI(componentUri))
            {
                using (Com.Tridion.Meta.ComponentMetaFactory fac = new Com.Tridion.Meta.ComponentMetaFactory(PublicationId))
                {
                    Com.Tridion.Meta.ComponentMeta componentMeta = fac.GetMeta(uri.GetItemId());
                    //Convert Java.Util.Date to System.Date //TODO: check if correct
                    string creationDate = componentMeta.GetCreationDate().ToString();
                    string modificationDate = componentMeta.GetModificationDate().ToString();

                    compMeta.CreationDate = Convert.ToDateTime(creationDate);
                    compMeta.ModificationDate = Convert.ToDateTime(modificationDate);

                    return compMeta;
                }
            }
        }
    }
}
