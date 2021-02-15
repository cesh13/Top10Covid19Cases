using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Top10Covid19Cases.Helpers;
using Top10Covid19Cases.Models;

namespace Top10Covid19Cases.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public JsonResult RegionStatistics(string id)
        {
            DataWrapper<RegionStatisticFlattendData> regionData = new DataWrapper<RegionStatisticFlattendData>();
            regionData.data = StatisticsProviderHelper.getRegionStatistics(id);
            return Json(regionData);
        }

        public IActionResult JSONExport(string id)
        {
            DataWrapper<RegionStatisticFlattendData> regionData = new DataWrapper<RegionStatisticFlattendData>();
            regionData.data = StatisticsProviderHelper.getRegionStatistics(id);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(regionData));
            return File(bytes, "application/json", DateTime.Now.ToString("yyyyMMdd") + "-covid-cases.json");
        }

        public IActionResult XMLExport(string id)
        {
            DataWrapper<RegionStatisticFlattendData> regionData = new DataWrapper<RegionStatisticFlattendData>();
            regionData.data = StatisticsProviderHelper.getRegionStatistics(id);

            //serialize data to xml
            XmlSerializer xsSubmit = new XmlSerializer(typeof(DataWrapper<RegionStatisticFlattendData>));
            var subReq = new DataWrapper<RegionStatisticFlattendData>();
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, regionData);
                    xml = sww.ToString();
                }
            }
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(xml);
            return File(bytes, "application/xml", DateTime.Now.ToString("yyyyMMdd") + "-covid-cases.xml");
        }

        public IActionResult CSVExport(string id)
        {
            DataWrapper<RegionStatisticFlattendData> regionData = new DataWrapper<RegionStatisticFlattendData>();
            regionData.data = StatisticsProviderHelper.getRegionStatistics(id);

            string csv = MiscHelper.ListToCSV(regionData.data);

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", DateTime.Now.ToString("yyyyMMdd") + "-covid-cases.csv");
        }

        public JsonResult Regions()
        {
            DataWrapper<Region> regionData = new DataWrapper<Region>();
            regionData.data = StatisticsProviderHelper.getRegions();
            return Json(regionData);
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
