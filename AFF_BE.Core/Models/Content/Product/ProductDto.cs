using AFF_BE.Core.Models.Content.Brand;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.Product
{
    public class ProductDto
    {
        public static int TotalRecordsCount = 0;
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Sku { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CollectionName { get; set; }
        public string? CategoryName { get; set; }
        public string? StoreName { get; set; }
        public string? UnitName { get; set; }
        public string? BrandName { get; set; }
        public string? Term { get; set; }
        public string? TermType { get; set; }
        public decimal? TotalQuantity { get; set; }
        public decimal? Mass { get; set; }
        public decimal? SellingPrice { get; set; }
        public decimal? ImportPrice { get; set; }
        public string? Content { get; set; }
        public string? LinkVideo { get; set; }
        public int? ViewCounts { get; set; }
        public int? SellCounts { get; set; }
        public decimal? Width { get; set; }
        public decimal? Hight { get; set; }
        public decimal? Length { get; set; }
        public decimal? Weight { get; set; }
        public string? Origin { get; set; }
        public string? Tag { get; set; }
        public int? Status { get; set; }
        public int? WarrantyPolicyId { get; set; }
        public int? Version { get; set; }
        public int? NumberDay { get; set; }
        public int? Warning { get; set; }
        public int? ProductType { get; set; }
        public ProductDto() { }


        public ProductDto(DataRow row)
        {
            if (row == null) return;

            Id = row.Table.Columns.Contains("Id") && row["Id"] != DBNull.Value ? Convert.ToInt64(row["Id"]) : 0;
            Code = row.Table.Columns.Contains("Code") ? row["Code"].ToString() : null;
            Description = row.Table.Columns.Contains("Description") ? row["Description"].ToString() : null;
            CategoryName = row.Table.Columns.Contains("CategoryName") ? row["CategoryName"].ToString() : null;
            SellingPrice = row.Table.Columns.Contains("SellingPrice") && row["SellingPrice"] != DBNull.Value ? (decimal?)row["SellingPrice"] : null;
            ImportPrice = row.Table.Columns.Contains("ImportPrice") && row["ImportPrice"] != DBNull.Value ? (decimal?)row["ImportPrice"] : null;
            Content = row.Table.Columns.Contains("Content") ? row["Content"].ToString() : null;
            LinkVideo = row.Table.Columns.Contains("LinkVideo") ? row["LinkVideo"].ToString() : null;
            BrandName = row.Table.Columns.Contains("BrandName") ? row["BrandName"].ToString() : null;
            CollectionName = row.Table.Columns.Contains("CollectionName") ? row["CollectionName"].ToString() : null;
            ViewCounts = row.Table.Columns.Contains("ViewCounts") && row["ViewCounts"] != DBNull.Value ? (int?)row["ViewCounts"] : null;
            SellCounts = row.Table.Columns.Contains("SellCounts") && row["SellCounts"] != DBNull.Value ? (int?)row["SellCounts"] : null;
            UnitName = row.Table.Columns.Contains("UnitName") ? row["UnitName"].ToString() : null;
            Width = row.Table.Columns.Contains("Width") && row["Width"] != DBNull.Value ? (decimal?)row["Width"] : null;
            Hight = row.Table.Columns.Contains("Hight") && row["Hight"] != DBNull.Value ? (decimal?)row["Hight"] : null;
            Length = row.Table.Columns.Contains("Length") && row["Length"] != DBNull.Value ? (decimal?)row["Length"] : null;
            Weight = row.Table.Columns.Contains("Weight") && row["Weight"] != DBNull.Value ? (decimal?)row["Weight"] : null;
            Origin = row.Table.Columns.Contains("Origin") ? row["Origin"].ToString() : null;
            StoreName = row.Table.Columns.Contains("StoreName") ? row["StoreName"].ToString() : null;
            Tag = row.Table.Columns.Contains("Tag") ? row["Tag"].ToString() : null;
            Status = row.Table.Columns.Contains("Status") && row["Status"] != DBNull.Value ? (int?)row["Status"] : null;
            Version = row.Table.Columns.Contains("Version") && row["Version"] != DBNull.Value ? (int?)row["Version"] : null;
        }
    }
}
