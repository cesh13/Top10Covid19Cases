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
        public static DataWrapper<ProvinceStatistic> getCollapsedStatisticsData()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            string response = "";

            //check if cache data available
            
            //if not pull data from api
            var responseTask = ApiCallerHelper.makeGetRequest("https://covid-19-statistics.p.rapidapi.com", "reports", headers, parameters);
            responseTask.Wait();

            response = responseTask.Result;

            DataWrapper<ProvinceStatistic> data = JsonConvert.DeserializeObject<DataWrapper<ProvinceStatistic>>(response);

            return data;
        }

    }
}
