using System;
using System.Collections.Generic;

#nullable disable

namespace NantechCargo.az.Models
{
    public partial class Mehsul
    {
        public Mehsul()
        {
            Sekils = new HashSet<Sekil>();
        }

        public int MehsulId { get; set; }
        public string MehsulName { get; set; }
        public string MehsulDescription { get; set; }
        public int? MehsulCategoryId { get; set; }
        public string MehsulConfirm { get; set; }

        public virtual Category MehsulCategory { get; set; }
        public virtual ICollection<Sekil> Sekils { get; set; }
    }
}
