using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace SampleWebsite.Controllers
{
    public class ManageController : Controller
    {

        [HandleError]
        public virtual ActionResult Page(string pageId)
        {
            return new ContentResult
            {
                Content = String.Format("Thanks for coming to this page. I am {0}", pageId),
                ContentType = "text/html",
                ContentEncoding = Encoding.UTF8
            };
        }
    }
}
