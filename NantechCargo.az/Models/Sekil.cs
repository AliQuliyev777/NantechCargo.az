using System;
using System.Collections.Generic;

#nullable disable

namespace NantechCargo.az.Models
{
    public partial class Sekil
    {
        public int SekilId { get; set; }
        public string SekilAd { get; set; }
        public int? SekilMehsulId { get; set; }

        public virtual Mehsul SekilMehsul { get; set; }
    }
}
