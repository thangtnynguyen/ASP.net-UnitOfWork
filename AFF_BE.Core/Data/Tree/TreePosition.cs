using AFF_BE.Core.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Data.Tree
{
    public class TreePosition
    {
        public int Id { get; set; }            
        public int TreeNodeId { get; set; }       
        public int Level { get; set; }          
        public string Position { get; set; }    
        public int LeftOrRightPriority { get; set; } 

        public virtual TreeNode? TreeNode { get; set; }
    }
}
