using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.ProductCategory
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
        public string? ParentName { get; set; }
        public int? CollectionId { get; set; }
        public string? CollectionName { get; set; }
        public int? IsDeleted { get; set; }
        public int? Status { get; set; }
        public int? Version { get; set; }
        public int? Level { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreateName { get; set; }
        public ProductCategoryDto() { }
        public ProductCategoryDto(DataRow row)
        {
            if (row == null) { return; }
            Id = row.Table.Columns.Contains("Id") && row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0;
            Code = row.Table.Columns.Contains("Code") ? row["Code"].ToString() : null;
            Name = row.Table.Columns.Contains("Name") ? row["Name"].ToString() : null;
            Description = row.Table.Columns.Contains("Description") ? row["Description"].ToString() : null;
            ParentId = row.Table.Columns.Contains("ParentId") && row["ParentId"] != DBNull.Value ? (int?)row["ParentId"] : null;
            ParentName = row.Table.Columns.Contains("ParentName") ? row["ParentName"].ToString() : null;
            CollectionId = row.Table.Columns.Contains("CollectionId") && row["CollectionId"] != DBNull.Value ? (int?)row["CollectionId"] : null;
            CollectionName = row.Table.Columns.Contains("CollectionName") ? row["CollectionName"].ToString() : null;
            IsDeleted = row.Table.Columns.Contains("IsDeleted") && row["IsDeleted"] != DBNull.Value ? (int?)row["IsDeleted"] : null;
            Version = row.Table.Columns.Contains("Version") && row["Version"] != DBNull.Value ? (int?)row["Version"] : null;
            Level = row.Table.Columns.Contains("Level") && row["Level"] != DBNull.Value ? (int?)row["Level"] : null;
            Status = row.Table.Columns.Contains("Status") && row["Status"] != DBNull.Value ? (int?)row["Status"] : null;
        }

    }

}
