using System;
using System.Collections.Generic;
using System.Text;
using Dynamic = Tridion.Extensions.DynamicDelivery.ContentModel;
using TCM = Tridion.ContentManager.ContentManagement;

namespace Tridion.Extensions.DynamicDelivery.Templates.Builder {
	public class SchemaBuilder {
        public static Dynamic.Schema BuildSchema(TCM.Schema tcmSchema, BuildManager manager)
        {
			if (tcmSchema == null) {
				return null;
			}
			Dynamic.Schema s = new Dynamic.Schema();
			s.Title = tcmSchema.Title;
			s.Id = tcmSchema.Id.ToString();
            s.Folder = manager.BuildOrganizationalItem((TCM.Folder)tcmSchema.OrganizationalItem);
            s.Publication = manager.BuildPublication(tcmSchema.ContextRepository);

			return s;
		}
	}
}
