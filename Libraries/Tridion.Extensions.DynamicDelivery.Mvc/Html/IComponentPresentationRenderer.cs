using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Tridion.Extensions.DynamicDelivery.ContentModel;
using System.Web.Mvc;

namespace Tridion.Extensions.DynamicDelivery.Mvc.Html
{
    public interface IComponentPresentationRenderer
    {
        MvcHtmlString ComponentPresentations(IPage tridionPage, HtmlHelper htmlHelper, string[] includeComponentTemplate, string includeSchema);
    }
}
