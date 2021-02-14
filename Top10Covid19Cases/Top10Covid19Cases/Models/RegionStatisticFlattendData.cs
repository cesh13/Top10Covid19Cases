using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Top10Covid19Cases.Models
{
    public class RegionStatisticFlattendData
    {
        public string region { get; set; }
        public long cases { get; set; }
        public long deaths { get; set; }
    }
}
