using AFF_BE.Core.Data.Address;
using AFF_BE.Core.Models.Adress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.IRepositories
{
    public interface IAddressRepository
    {

        Task<List<City>> GetCity(int id);
        Task<List<District>> GetDistrict(int id);
        Task<List<Ward>> GetWard(int id);
        Task<List<City>> GetCitiesByIdCountry(int countryId);
        Task<List<District>> GetDistrictByIdCity(int cityId);
        Task<List<District>> GetDistricts(GetDistrictRequest request);
        Task<List<Ward>> GetWards(GetWardRequest request);
        Task<List<Ward>> GetWardByIdDistrict(int districtId);
        Task Add();
    }

}
