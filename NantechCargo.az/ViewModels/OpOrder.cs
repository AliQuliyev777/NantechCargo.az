using System;
using System.Collections.Generic;

namespace NantechCargo.az.ViewModels
{
    public class OpOrder
    {
        public int OrderId { get; set; }
        public string ClientFullName { get; set; }
        public string OrderDate { get; set; }
        public int ProductCount { get; set; }
        public int? LevelId { get; set; }
        public string LevelName { get; set; }
        public List<string> ProductNames { get; set; }
        public List<string> ProductUrl { get; set; }
        //public List<int> ProductLevel { get; set; }
    }
}
