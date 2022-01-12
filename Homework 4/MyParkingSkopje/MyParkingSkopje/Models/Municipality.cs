using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyParkingSkopje.Models
{
    public class Municipality
    {
        [Key]
        public int MunicipalityId { get; set; }
        public string MunicipalityName { get; set; }
        public Municipality() { }
        public Municipality(string MunicipalityName)
        {
            this.MunicipalityName = MunicipalityName;
        }
    }
}