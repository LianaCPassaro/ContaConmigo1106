using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContaConmigo.Model;

namespace ContaConmigo.ViewModel
{
    public class CityViewModel
    {
        public City CitiesVM { get; set; }
        public Province ProvincesVM { get; set; }
        public Donor DonorsVM { get; set; }
        public GroupFactorBlood GroupFactorBloodVM { get; set; }
    }
}