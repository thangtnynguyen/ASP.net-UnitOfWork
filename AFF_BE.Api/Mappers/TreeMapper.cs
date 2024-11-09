using AFF_BE.Core.Data.Tree;
using AFF_BE.Core.Models.Tree;
using AutoMapper;

namespace AFF_BE.Api.Mappers
{
    public class TreeMapper:Profile
    {
        public TreeMapper()
        {
            CreateMap<TreeNode, TreeNodeDto>();
        }
    }
}
