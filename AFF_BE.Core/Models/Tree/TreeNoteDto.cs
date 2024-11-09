using AFF_BE.Core.Models.Identity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Tree
{
    public class TreeNodeDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ParentId { get; set; }
        //public int? LeftChildId { get; set; }
        //public int? RightChildId { get; set; }
        public int Level { get; set; }
        //public string Position { get; set; }
        //public string BranchPath { get; set; }

        //[JsonIgnore] 
        public TreeNodeDto Parent { get; set; }  
        public List<TreeNodeDto> Children { get; set; } = new List<TreeNodeDto>();

        public UserTreeDto User { get; set; }

    }

}
