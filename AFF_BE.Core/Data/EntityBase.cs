using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Data
{
    public abstract class EntityBase<TKey>
    {

        public TKey Id { get; set; }

        public int? CreatedBy { get; set; }

        public string? CreatedName { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public string? UpdatedName { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool? IsDeleted { get; set; } = false;
    }

}
