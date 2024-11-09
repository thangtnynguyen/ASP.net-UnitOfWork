using AFF_BE.Core.Data.Address;
using AFF_BE.Core.IRepositories;
using AFF_BE.Core.Models.Adress;
using AFF_BE.Data.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Data.Repositories
{
    public class AddressRepository : RepositoryBase<City, int>, IAddressRepository
    {
        public AddressRepository(AffContext dbContext, IHttpContextAccessor httpContext) : base(dbContext, httpContext)
        {
        }

        public class ProvinceApiResponse
        {
            public int Count { get; set; }
            public string Next { get; set; }
            public string Previous { get; set; }
            public List<ProvinceResult> Results { get; set; }
        }

        public class ProvinceResult
        {
            public string name { get; set; }
            public string full_name { get; set; }
            public string name_en { get; set; }
            public int id { get; set; }
            public string type { get; set; }
            public int province_id { get; set; }
        }

        public async Task Add()
        {
           
        }



        public async Task<List<City>> GetCity(int id)
        {
            List<City> city = await _dbContext.Cities.Where(c => c.Id == id).ToListAsync();
            return city;
        }
        public async Task<List<District>> GetDistrict(int id)
        {
            List<District> district = await _dbContext.Districts.Where(c => c.Id == id).ToListAsync();
            return district;
        }
        public async Task<List<Ward>> GetWard(int id)
        {
            List<Ward> ward = await _dbContext.Wards.Where(c => c.Id == id).ToListAsync();
            return ward;
        }



        public async Task<List<City>> GetCitiesByIdCountry(int countryId)
        {
            List<City> cities = await _dbContext.Cities.Where(c => c.CountryId == countryId).ToListAsync();
            return cities;
        }

        public async Task<List<District>> GetDistrictByIdCity(int cityId)
        {

            List<District> districts = await _dbContext.Districts.Where(c => c.CityId == cityId).ToListAsync();
            return districts;
        }

        public async Task<List<District>> GetDistricts(GetDistrictRequest request)
        {
            var query = _dbContext.Districts.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.CityName))
            {

                query = query.Where(c => c.CityId == request.CityId);
            }

            // Kiểm tra nếu CityName không rỗng hoặc null thì thực hiện tìm kiếm
            if (!string.IsNullOrWhiteSpace(request.CityName))
            {
                query = query.Where(c => c.CityName.StartsWith(request.CityName));
            }

            if (request.CityId.HasValue)
            {
                query = query.Where(c => c.CityId == request.CityId);
            }

            List<District> districts = await query.ToListAsync();

            return districts;
        }



        public async Task<List<Ward>> GetWardByIdDistrict(int districtId)
        {
            List<Ward> wards = await _dbContext.Wards.Where(c => c.DistrictId == districtId).ToListAsync();
            return wards;
        }


        public async Task<List<Ward>> GetWards(GetWardRequest request)
        {
            var query = _dbContext.Wards.AsQueryable();

            // Kiểm tra nếu CityName không rỗng hoặc null thì thực hiện tìm kiếm
            query = query.Where(c => c.DistrictId == request.DistrictId);

            List<Ward> wards = await query.ToListAsync();

            return wards;
        }
    }
}
