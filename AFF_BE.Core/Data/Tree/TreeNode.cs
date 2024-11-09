using AFF_BE.Core.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Data.Tree
{
    public class TreeNode:EntityBase<int>
    {
        public int? UserId { get; set; }        
        public int? ParentId { get; set; }       
        public int? LeftChildId { get; set; }    
        public int? RightChildId { get; set; }   
        public int Level { get; set; }           
        public string Position { get; set; }     
        public string BranchPath { get; set; } 

        public virtual TreeNode? Parent { get; set; }
        public virtual TreeNode? LeftChild { get; set; }
        public virtual TreeNode? RightChild { get; set; }

        public virtual User? User { get; set; }
    }
}
