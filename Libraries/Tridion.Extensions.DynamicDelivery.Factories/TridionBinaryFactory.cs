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
    public class TridionBinaryFactory : TridionFactoryBase,  IBinaryFactory
    {
        public bool TryFindBinary(string url, out IBinary binary)
        {
            string encodedUrl = HttpUtility.UrlPathEncode(url);
            binary = null;            
            using (var sqlBinMetaHome = new Com.Tridion.Broker.Binaries.Meta.SQLBinaryMetaHome())
            {
                Com.Tridion.Meta.BinaryMeta binaryMeta = sqlBinMetaHome.FindByURL(PublicationId, encodedUrl); // "/Images/anubis_pecunia160_tcm70-520973.jpg"                            
                if (binaryMeta != null)
                { 
                    using (var sqlBinaryHome = new Com.Tridion.Broker.Binaries.SQLBinaryHome())
                    {
                        Com.Tridion.Data.BinaryData binData = sqlBinaryHome.FindByPrimaryKey(PublicationId, (int)binaryMeta.GetId());
                        if (binData != null)
                        {
                            binary = new Binary(this)
                            {
                                Id = String.Format("tcm:{0}-{1}", binData.GetPublicationId(), binData.GetId()),
                                Url = url,
                                LastPublishedDate = DateTime.Now,
                                Multimedia = null,
                                VariantId = binData.GetVariantId()
                            };
                            return true;
                        }                        
                    }
                }
                return false;
            }

           
        }

        public IBinary FindBinary(string url)
        {
           IBinary binary;
           if(!TryFindBinary(url, out binary))
           {
               throw new BinaryNotFoundException();
           }

           return binary;
        }

        public bool TryGetBinary(string tcmUri, out IBinary binary)
        {
            binary = null;
            using (var uri = new Com.Tridion.Util.TCMURI(tcmUri))
            {
                using (var sqlBinHome = new Com.Tridion.Broker.Binaries.SQLBinaryHome())
                {
                    var binData = sqlBinHome.FindByPrimaryKey(uri.GetPublicationId(), uri.GetItemId());
                    using (var sqlBinMetaHome = new Com.Tridion.Broker.Binaries.Meta.SQLBinaryMetaHome())
                    {
                        using (Com.Tridion.Util.TCDURI tcdUri = new Com.Tridion.Util.TCDURI(uri))
                        {
                            Com.Tridion.Meta.BinaryMeta binaryMeta = sqlBinMetaHome.FindByPrimaryKey(tcdUri);

                            if (binData != null)
                            {
                                binary = new Binary(this)
                                {
                                    Url = binaryMeta.GetURLPath(),
                                    LastPublishedDate = DateTime.Now,
                                    Multimedia = null,
                                    VariantId = binData.GetVariantId()
                                };

                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public IBinary GetBinary(string tcmUri)
        {
            IBinary binary;
            if (!TryGetBinary(tcmUri, out binary))
            {
                throw new BinaryNotFoundException();
            }
            return binary;
        }

        public bool TryFindBinaryContent(string url, out byte[] bytes)
        {
            string encodedUrl = HttpUtility.UrlPathEncode(url);
            bytes = null;
            using (var sqlBinMetaHome = new Com.Tridion.Broker.Binaries.Meta.SQLBinaryMetaHome())
            {
                var coll = sqlBinMetaHome.FindByURL(encodedUrl); 
                using (var d = new Com.Tridion.Broker.Binaries.SQLBinaryHome())
                {
                    foreach (Com.Tridion.Meta.BinaryMeta item in coll)
                    {
                        if (!item.GetPublicationId().Equals(PublicationId)) continue;

                        Com.Tridion.Data.BinaryData binData = d.FindByPrimaryKey(PublicationId, (int)item.GetId());
                        if (binData != null)
                        {
                            bytes = binData.GetBytes();
                            return true;
                        }                                              
                    }
                }
            }
            
            return false;  
        }

        public byte[] FindBinaryContent(string url)
        {
            byte[] bytes;
            if (!TryFindBinaryContent(url, out bytes))
            {
                throw new BinaryNotFoundException();
            }

            return bytes;
        }

        public bool TryGetBinaryContent(string tcmUri, out byte[] bytes)
        {
            bytes = null;
            using (var uri = new Com.Tridion.Util.TCMURI(tcmUri))
            {
                using (var sqlBinHome = new Com.Tridion.Broker.Binaries.SQLBinaryHome())
                {
                    var binData = sqlBinHome.FindByPrimaryKey(uri.GetPublicationId(), uri.GetItemId());
                    if (binData != null)
                    {
                        bytes = binData.GetBytes();
                        return true;
                    }
                }
                return false;
            }
            
        }

        public byte[] GetBinaryContent(string tcmUri)
        {
            byte[] bytes;
            if (!TryGetBinaryContent(tcmUri, out bytes))
            {
                throw new BinaryNotFoundException();
            }

            return bytes;
        }

        public bool HasBinaryChanged(string url)
        {
            return true;
        }
        
    }
}