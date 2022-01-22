using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchParkingsMicroservice.Models
{
    public class Municipality
    {
        public int MunicipalityId { get; set; }
        public string MunicipalityName { get; set; }
        public Municipality() { }
        public Municipality(string MunicipalityName)
        {
            this.MunicipalityName = MunicipalityName;
        }
    }
}