using Tridion.Extensions.DynamicDelivery.Utilities;

namespace Tridion.Extensions.DynamicDelivery.Factories
{
    /// <summary>
    /// Base class for all TridionFactorys
    /// </summary>
    public abstract class TridionFactoryBase 
    {             
        /// <summary>
        /// Returns the current publicationId
        /// </summary>  
        protected virtual int PublicationId 
        {
            get
            {
                return TridionHelper.PublicationId;
            }
        }

    }
}
