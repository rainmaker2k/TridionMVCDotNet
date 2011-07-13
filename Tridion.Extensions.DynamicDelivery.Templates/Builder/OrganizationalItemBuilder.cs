using System;
using System.Collections.Generic;
using System.Text;
using Dynamic = Tridion.Extensions.DynamicDelivery.ContentModel;
using TComm = Tridion.ContentManager.CommunicationManagement;
using TCM = Tridion.ContentManager.ContentManagement;
using Tridion.Extensions.DynamicDelivery.Templates.Utils;

namespace Tridion.Extensions.DynamicDelivery.Templates.Builder {
	public class OrganizationalItemBuilder {
		public static Dynamic.OrganizationalItem BuildOrganizationalItem(TComm.StructureGroup tcmStructureGroup) {
         GeneralUtils.TimedLog("start BuildOrganizationalItem");
         Dynamic.OrganizationalItem oi = new Dynamic.OrganizationalItem();
			oi.Title = tcmStructureGroup.Title;
         oi.Id = tcmStructureGroup.Id.ToString();
         oi.PublicationId = tcmStructureGroup.ContextRepository.Id.ToString();
         GeneralUtils.TimedLog("finished BuildOrganizationalItem");
         return oi;
		}
      public static Dynamic.OrganizationalItem BuildOrganizationalItem(TCM.Folder tcmFolder)
      {
         GeneralUtils.TimedLog("start BuildOrganizationalItem");
         Dynamic.OrganizationalItem oi = new Dynamic.OrganizationalItem();
         oi.Title = tcmFolder.Title;
         oi.Id = tcmFolder.Id.ToString();
         oi.PublicationId = tcmFolder.ContextRepository.Id.ToString();
         GeneralUtils.TimedLog("finished BuildOrganizationalItem");
         return oi;
      }
   }
}
