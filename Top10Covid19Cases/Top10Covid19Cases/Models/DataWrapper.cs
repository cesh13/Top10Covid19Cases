using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Top10Covid19Cases.Models
{
    public class DataWrapper<T>
    {
        public List<T> data { get; set; }
    }
}
