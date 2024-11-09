using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Content.ProductVariant
{
    public class ProductVariantDto
    {
        public static int TotalRecordsCount = 0;
        public int Id { get; set; }
        public string? Sku { get; set; }
        public string? UnitName { get; set; }
        public string? ProductName { get; set; }
        public int ProductType { get; set; }
        public decimal? Price { get; set; }
        public decimal? Quantity { get; set; }
        public string? Propeties1 { get; set; }
        public string? ValuePropeties1 { get; set; }
        public string? Propeties2 { get; set; }
        public string? ValuePropeties2 { get; set; }
        public int? Status { get; set; }
        public int? Version { get; set; }
        public ProductVariantDto() { }
        public ProductVariantDto(DataRow row)
        {
            if (row == null) return;

            Id = row.Table.Columns.Contains("Id") && row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0;
            Sku = row.Table.Columns.Contains("Sku") ? row["Sku"].ToString() : null;
            UnitName = row.Table.Columns.Contains("UnitName") ? row["UnitName"].ToString() : null;
            ProductName = row.Table.Columns.Contains("ProductName") ? row["ProductName"].ToString() : null;
            Price = row.Table.Columns.Contains("Price") && row["Price"] != DBNull.Value ? (decimal?)row["Price"] : null;
            Quantity = row.Table.Columns.Contains("Quantity") && row["Quantity"] != DBNull.Value ? (int?)row["Quantity"] : null;
            Propeties1 = row.Table.Columns.Contains("Propeties1") ? row["Propeties1"].ToString() : null;
            ValuePropeties1 = row.Table.Columns.Contains("ValuePropeties1") ? row["ValuePropeties1"].ToString() : null;
            Propeties2 = row.Table.Columns.Contains("Propeties2") ? row["Propeties2"].ToString() : null;
            ValuePropeties2 = row.Table.Columns.Contains("ValuePropeties2") ? row["ValuePropeties2"].ToString() : null;
            Status = row.Table.Columns.Contains("Status") && row["Status"] != DBNull.Value ? (int?)row["Status"] : null;
            Version = row.Table.Columns.Contains("Version") && row["Version"] != DBNull.Value ? (int?)row["Version"] : null;
        }

    }

}

