using AFF_BE.Core.Data.Address;
using AFF_BE.Core.Models.Adress;
using AFF_BE.Core.Models.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.IRepositories
{

    public interface ITreeRepository
    {

        Task<TreeNodeDto> GetTree(GetPositionTreeRequest request);
        Task<bool> AddUserToTree(CreateNoteToTreeRequest request);
        Task<bool> AddUserToTreeUseBranchPath(CreateNoteToTreeRequest request);

    }
}
