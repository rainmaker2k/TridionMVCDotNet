﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamic = Tridion.Extensions.DynamicDelivery.ContentModel;
using Tridion.ContentManager.ContentManagement;

namespace Tridion.Extensions.DynamicDelivery.Templates.Builder
{
   public class KeywordBuilder
   {
      public static Dynamic.Keyword BuildKeyword(Keyword keyword)
      {
         Dynamic.Keyword dk = new Dynamic.Keyword();
         dk.Id = keyword.Id;
         dk.Title = keyword.Title;
         dk.Path = FindKeywordPath(keyword);
         dk.TaxonomyId = keyword.OrganizationalItem.Id;
         return dk;
      }
      private static string FindKeywordPath(Keyword keyword)
      {
         IList<Keyword> parentKeywords = keyword.ParentKeywords;
         string path = @"\" + keyword.Title;
         while (parentKeywords.Count > 0) {
            path = @"\" + parentKeywords[0].Title + path;
            parentKeywords = parentKeywords[0].ParentKeywords;
         }
         return @"\" + keyword.OrganizationalItem.Title + path;
      }
   }
}
