using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCClient.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace TCClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            TitlesModel model = new TitlesModel();
            ViewBag.Titles = model.GetTitles("");
            return View();
        }

        [HttpGet]
        public JsonResult Search(string id)
        {
            TitlesModel model = new TitlesModel();

            return Json(model.GetTitles(id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Details(int id)
        {
            TitlesModel model = new TitlesModel();
            BsonDocument doc = model.GetData(id);
            //return doc.ToList();
            JsonResult result = Json(doc.ToJson(), JsonRequestBehavior.AllowGet);
            return result;
        }
    }
}