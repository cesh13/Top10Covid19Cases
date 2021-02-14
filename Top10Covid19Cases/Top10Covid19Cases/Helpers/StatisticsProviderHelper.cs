using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Top10Covid19Cases.Models;

namespace Top10Covid19Cases.Helpers
{
    public static class StatisticsProviderHelper
    {
        public static List<RegionStatisticFlattendData> getRegionStatistics()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            string response = "";

            //check if cache data available
            
            //if not pull data from api
            var responseTask = ApiCallerHelper.makeGetRequest("https://covid-19-statistics.p.rapidapi.com", "reports", headers, parameters);
            responseTask.Wait();

            response = responseTask.Result;

            DataWrapper<RegionStatistic> report = JsonConvert.DeserializeObject<DataWrapper<RegionStatistic>>(response);
            var collapsedReport = collapseData(report);

            //sort and trim collapsedReport
            var collapsedOrderedTrimedReport = collapsedReport.OrderByDescending(x => x.cases).Take(10).ToList();

            return collapsedOrderedTrimedReport;
        }

        private  static List<RegionStatisticFlattendData> collapseData(DataWrapper<RegionStatistic> report)
        {
            var collapsedData = new List<RegionStatisticFlattendData>();
            
            foreach (var provinceStatistic in report.data)
            {
                //check if the same region is already in the collapsed data, if it is sum it
                var searchResult = collapsedData.Find(x => x.region == provinceStatistic.region.name);

                if (searchResult != null)
                {
                    searchResult.cases += provinceStatistic.confirmed;
                    searchResult.deaths += provinceStatistic.deaths;                    
                }
                else
                {
                    collapsedData.Add(new RegionStatisticFlattendData
                    {
                        region = provinceStatistic.region.name,
                        cases = provinceStatistic.confirmed,
                        deaths = provinceStatistic.deaths
                    });;
                }
            }

            return collapsedData;
        }

        public static List<Region> getRegions()
        {
            var responseTask = ApiCallerHelper.makeGetRequest("https://covid-19-statistics.p.rapidapi.com", "regions", new Dictionary<string, string>(), new Dictionary<string, string>() );
            responseTask.Wait();
            var response = responseTask.Result;
            return JsonConvert.DeserializeObject<DataWrapper<Region>>(response).data;
        }
    }
}
