namespace AFF_BE.Core.Models.Identity.User
{
    public class EditUserInfoRequest
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }

        //public int? BranchId { get; set; }

        //public List<int>? BranchIds { get; set; }// chi  nhánh quản lý

        //public string? BranchName { get; set; }

        public string? Email { get; set; }

        public int? CityId { get; set; }

        public string? CityName { get; set; }

        public int? DistrictId { get; set; }

        public string? DistrictName { get; set; }

        public int? WardId { get; set; }

        public string? WardName { get; set; }

    }
}
