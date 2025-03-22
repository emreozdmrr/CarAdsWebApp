using AutoMapper;
using CarAdsWebApp.Business.Mappings.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Business.Helpers
{
    public static class ProfileHelper
    {
        public static List<Profile> GetProfiles()
        {
            return new List<Profile>
            {
                new AppUserProfile(),
                new BodyTypeProfile(),
                new CityProfile(),
                new GearBoxProfile(),
                new MakeProfile(),
                new MessageProfile(),
                new ModelProfile()
            };
        }
    }
}
