// for staff/admin to upload/edit products imges
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HanShop.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        // Implementation for asynchronous upload functionality.
        [HttpPost]
        public ActionResult file(HttpPostedFileBase pic)
        {
            try
            {
                if (pic != null)
                {
                    // if pic content is null, return this error
                    if (pic.ContentLength == 0)
                    {
                        return Content("209"); 
                    }
                    else
                    {
                        //"Check if the file's extension meets the criteria
                        string backFix = Path.GetExtension(pic.FileName);
                        if (backFix != ".gif" && backFix != ".png" && backFix != ".jpg" && backFix != ".jpeg")
                        {
                            return Content("210"); //if fails
                        }
                        // just in case that different staff uploads picture from their computer, however the picture in their computers got the same name.
                        // so i added "MMddHHmmss" here to make sure that each filename is different.
                        string fileName = DateTime.Now.ToString("MMddHHmmss") + backFix;
                        string strPath = Server.MapPath("~/Content/Pics/" + fileName);
                        pic.SaveAs(strPath);
                        //return the string img content
                        return Content("/Content/Pics/" + fileName);
                    }
                }
                else
                {
                    return Content("300"); //picture cannot be an empty one
                }
            }
            catch (Exception)
            {
                return Content("400"); //upload failed
            }
        }
    }
}