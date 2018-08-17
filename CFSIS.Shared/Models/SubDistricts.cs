using System;
using System.Collections.Generic;

namespace CFSIS.Shared.Models
{
    public partial class SubDistricts
    {
        public int SubDistrictsId { get; set; }
        public string SubDistrictsName { get; set; }
        public int DistrictId { get; set; }
    }
}
