using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ImageControl.Controllers
{
    public class HomeController : Controller
    {
        public string ImagePath
        {
            get { return Server.MapPath("~/Files/profile.jpg"); }
        }

        public ActionResult Index(string cmd = "Original")
        {
            ViewBag.cmd = cmd;
            return View();
        }

        public void Process(string p)
        {
            WebImage image = new WebImage(this.ImagePath);

            switch (p)
            {
                case "Original":
                    break;
                case "RotateLeft":
                    image.RotateLeft();
                    break;
                case "RotateRight":
                    image.RotateRight();
                    break;
                case "FlipHorizontal":
                    image.FlipHorizontal();
                    break;
                case "FlipVertical":
                    image.FlipVertical();
                    break;
                case "Resize":
                    image.Resize(image.Width / 2, image.Height / 2, preserveAspectRatio: true);
                    break;
                case "AddTextWatermark":
                    image.AddTextWatermark("Ishak Alpfidan", fontColor: "White", fontSize: 14, horizontalAlign: "Center", verticalAlign: "Bottom");
                    break;
                case "AddImageWatermark":
                    WebImage watermark = new WebImage(this.ImagePath);
                    watermark.Resize(50, 50);
                    watermark = watermark.Save(Server.MapPath("~/Files/watermark.jpeg"), imageFormat: "jpeg");

                    image.AddImageWatermark(watermark.FileName, 50, 50, verticalAlign: "Top", horizontalAlign: "Right", opacity: 75);
                    break;
                case "Crop":
                    image.Crop(0, 0, 100, 100);
                    break;
                default:
                    break;
            }

            string savePath = Server.MapPath("~/Files/last.jpeg");
            WebImage savedImage = image.Save(savePath, imageFormat: "jpeg");

            image.Write();
        }
    }
}