using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmogAppWeb.Models;
using SmogAppWeb.Models.Parsers;

namespace SmogAppWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Ta strona jeszcze nie powstała.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Ta strona jeszcze nie powstała.";

            return View();
        }
        
        [HttpPost]
        public ActionResult Measurement(LocationModel location)
        {
            String address = location.Address;
            if (address == null)
            {
                ViewBag.Message = "Pole adresu nie może być puste.";
                return View("Index");
            }

            GeocodingParser gp = new GeocodingParser();
            LocationModel loc;
            try
            {
                loc = gp.GeocodeFromAddress(address);
            }
            catch (Exception e)
            {
                ViewBag.Message = "Nie udało się znaleźć wprowadzonego przez Ciebie adresu. Spróbuj ponownie.";
                return View("Index");
            }


            AqiParser ap = new AqiParser();
            MeasurementModel measurement;
            try
            {
                measurement = ap.ParseAirQuality(loc);
            }
            catch(Exception e)
            {
                ViewBag.Message = "Ta lokacja jest aktualnie niedostępna.";
                return View("Index");
            }
            if (measurement == null) return Index();

            return View(measurement);
        }
    }
}