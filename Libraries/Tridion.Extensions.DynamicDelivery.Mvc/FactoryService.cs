using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tridion.Extensions.DynamicDelivery.ContentModel.Factories;

namespace Tridion.Extensions.DynamicDelivery.Mvc
{
    public static class FactoryService
    {
        public static IBinaryFactory BinaryFactory 
        { 
            get 
            {
                return ServiceLocator.GetInstance<IBinaryFactory>();
            }
        }

        public static ILinkFactory LinkFactory 
        {
            get
            {
                return ServiceLocator.GetInstance<ILinkFactory>();
            }
        }

        public static IComponentFactory ComponentFactory
        {
            get 
            {
                return ServiceLocator.GetInstance<IComponentFactory>();
            }
            
        }

        public static IPageFactory PageFactory
        {
            get 
            {
                return ServiceLocator.GetInstance<IPageFactory>();
            }
        }

        public static IQueryFactory QueryFactory
        {
            get
            {
                return ServiceLocator.GetInstance<IQueryFactory>();
            }
        }

        public static IModelFactory ModelFactory
        {
            get
            {
                return ServiceLocator.GetInstance<IModelFactory>();
            }
        }

        public static ITaxonomyFactory TaxonomyFactory
        {
            get
            {
                return ServiceLocator.GetInstance<ITaxonomyFactory>();
            }
        }
    }
}
