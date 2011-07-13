using Tridion.Extensions.DynamicDelivery.ContentModel;
using Tridion.Extensions.DynamicDelivery.ContentModel.Factories;

namespace Tridion.Extensions.DynamicDelivery.Factories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using Tridion.ContentDelivery.DynamicContent;
    using Tridion.ContentDelivery.DynamicContent.Query;
    using Tridion.Extensions.DynamicDelivery.ContentModel.Exceptions;
    using Tridion.ContentDelivery.Taxonomies;

    [Export(typeof(ITaxonomyFactory))]
    public class TridionTaxonomyFactory : TridionFactoryBase, ITaxonomyFactory
    {

        public bool TryGetKeyword(string categoryUriToLookIn, string keywordName, out IKeyword keyword)
        {
            using (TaxonomyFactory taxFactory = new TaxonomyFactory())
            {
                
                var taxonomyUri = categoryUriToLookIn; 

                //Create filter to retrieve all keywords in a taxonomy
                CompositeFilter compFilter = new CompositeFilter();
                compFilter.DepthFiltering(DepthFilter.UnlimitedDepth, DepthFilter.FilterUp);
                compFilter.DepthFiltering(DepthFilter.UnlimitedDepth, DepthFilter.FilterDown);

                //Get keywords in taxonomy (hierarchically)
                IEnumerable<Keyword> taxonomy = null;
                try
                {
                    //Ugly way to see if a taxonomy exists. Alternative is to loop through all taxonomys in Tridion and check if the categoryUriToLookIn exists...
                    taxonomy = taxFactory.GetTaxonomyKeywords(categoryUriToLookIn, compFilter, new TaxonomyHierarchyFormatter()).KeywordChildren.Cast<Keyword>();
                }
                catch (Exception)
                {
                    //TODO: Trace
                    keyword = null;
                    return false;
                }

                //Search in taxonomy
                Keyword foundKeyword = null;
                foreach (var currentKeyword in taxonomy)
                {
                    string currentKeywordName = currentKeyword.KeywordName;
                    if (currentKeywordName != keywordName)
                    {
                        foundKeyword = recursive(currentKeyword.KeywordChildren.Cast<Keyword>().ToList(), keywordName);
                    }
                    else
                    {
                        foundKeyword = currentKeyword;
                    }
                    if (foundKeyword != null)
                    {
                        Tridion.Extensions.DynamicDelivery.ContentModel.Keyword returnKeyword = new ContentModel.Keyword();
                        
                        Keyword par = foundKeyword.ParentKeyword;
                        do
                        {            
                            Tridion.Extensions.DynamicDelivery.ContentModel.Keyword newParentKeyword = new ContentModel.Keyword();
                            newParentKeyword.Id = par.KeywordUri;
                            newParentKeyword.TaxonomyId = par.TaxonomyUri;
                            newParentKeyword.Title = par.KeywordName;                        
                            returnKeyword.ParentKeywords.Add(newParentKeyword); //Add the parentkeyword to the list
                            par = par.ParentKeyword;

                        } while (par != null);
                        
                        //Add remaining properties to the returnKeyword
                        returnKeyword.Id = foundKeyword.KeywordUri;                        
                        returnKeyword.TaxonomyId = foundKeyword.TaxonomyUri;
                        returnKeyword.Title = foundKeyword.KeywordName;


                        keyword = returnKeyword;
                        return true;
                    }
                }

                keyword = null;
                return false; //Keyword not found
            }

        }

        public IKeyword GetKeyword(string categoryUriToLookIn, string keywordName)
        {
            throw new NotImplementedException();
        }

        private Keyword recursive(List<Keyword> keywords, string valueToLookFor)
        {
            Keyword returnValue = null;
            foreach (var item in keywords)
            {

                if (item.KeywordName == valueToLookFor)
                {
                    returnValue = item;
                }
                else
                {
                    if (item.HasChildren)
                    {
                        returnValue = recursive(item.KeywordChildren.Cast<Keyword>().ToList(), valueToLookFor);
                    }
                }
                if (returnValue != null)
                {
                    break;
                }
            }
            return returnValue;
        }
    }
}

