using AFF_BE.Core.Constants;
using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.Data.Tree;
using AFF_BE.Core.ISeedWorks;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Content.Banner;
using AFF_BE.Core.Models.Tree;
using AFF_BE.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AFF_BE.Api.Controllers.Tree
{
    [Route("api/tree")]
    [ApiController]
    public class TreeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TreeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("get-position-tree")]
        public async Task<ApiResult<TreeNodeDto>> GetTree([FromQuery] GetPositionTreeRequest request)
        {
            var tree = await _unitOfWork.Tree.GetTree(request);
            if (tree == null)
                return new ApiResult<TreeNodeDto>()
                {
                    Status=false,
                    Message="Không tìm thấy nốt trong cây",
                    Data=null
                };

            return new ApiResult<TreeNodeDto>()
            {
                Status = true,
                Message = "Thành công",
                Data = tree
            };
        }

       

        //[HttpPost("add-user-to-tree")]
        //public async Task<ApiResult<bool>> Create([FromBody] CreateNoteToTreeRequest request)
        //{
        //    #region ignore
        //    //await _unitOfWork.Tree.AddUserToTree(request);

        //    //return new ApiResult<bool>()
        //    //{
        //    //    Status = true,
        //    //    Message = "Thêm người dùng vào cây thành công!",
        //    //    Data = true
        //    //};
        //    #endregion

        //    return new ApiResult<bool>()
        //    {
        //        Status = true,
        //        Message = "Api không dùng nữa !",
        //        Data = true
        //    };
        //}

        [HttpPost("add-user-to-tree-use-branch-path")]
        public async Task<ApiResult<bool>> CreateUseBranchPath([FromBody] CreateNoteToTreeRequest request)
        {

            await _unitOfWork.Tree.AddUserToTreeUseBranchPath(request);

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thêm người dùng vào cây thành công!",
                Data = true
            };
        }

























        #region test
        //private async Task<TreeNodeDto> GetTreeAsync(int nodeId, int downLevels)
        //{
        //    downLevels = Math.Min(downLevels, 5);

        //    // Tìm node hiện tại
        //    var currentNode = await _dbContext.TreeNodes.FindAsync(nodeId);
        //    if (currentNode == null) return null;

        //    // Tạo DTO cho node hiện tại
        //    var rootDto = new TreeNodeDto
        //    {
        //        Id = currentNode.Id,
        //        UserId = currentNode.UserId,
        //        ParentId = currentNode.ParentId
        //    };

        //    // Lấy node cha (1 tầng cha)
        //    if (currentNode.ParentId.HasValue)
        //    {
        //        var parentNode = await _dbContext.TreeNodes.FindAsync(currentNode.ParentId);
        //        if (parentNode != null)
        //        {
        //            rootDto.Parent = new TreeNodeDto
        //            {
        //                Id = parentNode.Id,
        //                UserId = parentNode.UserId,
        //                ParentId = parentNode.ParentId,
        //                //Children = new List<TreeNodeDto> { rootDto } // Node hiện tại là con của node cha
        //            };
        //        }
        //    }

        //    // Lấy các node con xuống dưới trong `downLevels` tầng
        //    await AddDescendants(rootDto, downLevels);

        //    return rootDto;
        //}
        //private async Task AddAncestors(TreeNodeDto rootDto, int? parentId, int levelsRemaining)
        //{
        //    if (parentId == null || levelsRemaining <= 0) return;

        //    var parentNode = await _dbContext.TreeNodes.FindAsync(parentId);
        //    if (parentNode != null)
        //    {
        //        var parentDto = new TreeNodeDto
        //        {
        //            Id = parentNode.Id,
        //            UserId = parentNode.UserId,
        //            ParentId = parentNode.ParentId,
        //            Children = new List<TreeNodeDto> { rootDto } // Thêm node hiện tại vào danh sách con của cha
        //        };

        //        await AddAncestors(parentDto, parentNode.ParentId, levelsRemaining - 1);

        //        // Cập nhật rootDto với node cha mới
        //        rootDto.ParentId = parentDto.Id;
        //    }
        //}

        //private async Task AddDescendants(TreeNodeDto rootDto, int levelsRemaining)
        //{
        //    if (levelsRemaining <= 0) return;

        //    // Tìm các node con của node hiện tại
        //    var children = await _dbContext.TreeNodes
        //        .Where(x => x.ParentId == rootDto.Id)
        //        .ToListAsync();

        //    foreach (var child in children)
        //    {
        //        var childDto = new TreeNodeDto
        //        {
        //            Id = child.Id,
        //            UserId = child.UserId,
        //            ParentId = rootDto.Id
        //        };

        //        rootDto.Children.Add(childDto);

        //        // Đệ quy để lấy các con của node con
        //        await AddDescendants(childDto, levelsRemaining - 1);
        //    }
        //}



        //private async Task AddNodeUseBranchPathAsync(int userId, int? inviterId = null)
        //{
        //    // Kiểm tra nếu không có vị trí trống trong `TreePositions`, thêm node gốc
        //    if (!_dbContext.TreePositions.Any())
        //    {
        //        await InitializeRootNodeAsync(userId);
        //        return;
        //    }

        //    // Lấy vị trí trống tiếp theo từ `TreePositions`
        //    TreePosition position = await GetNextAvailablePositionUserBranchPathAsync(inviterId);

        //    if (position == null)
        //    {
        //        throw new Exception("Không có vị trí trống khả dụng.");
        //    }

        //    // Lấy BranchPath của node cha để tạo BranchPath cho node mới
        //    var parentNode = await _dbContext.TreeNodes.FindAsync(position.TreeNodeId);
        //    if (parentNode == null)
        //    {
        //        throw new Exception("Node cha không tồn tại.");
        //    }

        //    // Tạo BranchPath tạm thời, sau đó sẽ cập nhật lại sau khi biết Id của newNode
        //    var temporaryBranchPath = parentNode.BranchPath + "temp/";

        //    // Tạo node mới với UserId và thông tin từ vị trí trống, sử dụng BranchPath tạm thời
        //    var newNode = new TreeNode
        //    {
        //        UserId = userId,
        //        Level = position.Level,
        //        ParentId = position.TreeNodeId,
        //        Position = position.Position,
        //        BranchPath = temporaryBranchPath // Gán giá trị tạm thời
        //    };

        //    // Thêm node mới và lưu để có Id hợp lệ
        //    _dbContext.TreeNodes.Add(newNode);
        //    await _dbContext.SaveChangesAsync();

        //    // Cập nhật lại BranchPath của node mới sau khi có Id
        //    newNode.BranchPath = parentNode.BranchPath + newNode.Id + "/";
        //    _dbContext.TreeNodes.Update(newNode);

        //    // Cập nhật node cha với newNode.Id
        //    if (position.Position == "left")
        //    {
        //        parentNode.LeftChildId = newNode.Id;
        //    }
        //    else if (position.Position == "right")
        //    {
        //        parentNode.RightChildId = newNode.Id;
        //    }

        //    _dbContext.TreeNodes.Update(parentNode);

        //    // Xóa vị trí trống vừa được lấp đầy trong `TreePositions`
        //    _dbContext.TreePositions.Remove(position);

        //    // Thêm các vị trí trống mới cho node con của newNode
        //    _dbContext.TreePositions.Add(new TreePosition
        //    {
        //        TreeNodeId = newNode.Id,
        //        Level = newNode.Level + 1,
        //        Position = "left",
        //        LeftOrRightPriority = 0
        //    });

        //    _dbContext.TreePositions.Add(new TreePosition
        //    {
        //        TreeNodeId = newNode.Id,
        //        Level = newNode.Level + 1,
        //        Position = "right",
        //        LeftOrRightPriority = 1
        //    });

        //    await _dbContext.SaveChangesAsync();
        //}


        //private async Task<TreePosition> GetNextAvailablePositionUserBranchPathAsync(int? inviterId = null)
        //{
        //    var query = _dbContext.TreePositions.AsQueryable();

        //    if (inviterId.HasValue)
        //    {
        //        // Lấy BranchPath của người mời để tìm các vị trí trống trong nhánh của người mời
        //        var inviterBranchPath = await _dbContext.TreeNodes
        //            .Where(n => n.Id == inviterId)
        //            .Select(n => n.BranchPath)
        //            .FirstOrDefaultAsync();

        //        if (string.IsNullOrEmpty(inviterBranchPath))
        //        {
        //            throw new Exception("Người mời không tồn tại trong cây hoặc không có BranchPath.");
        //        }

        //        // Lọc các vị trí trống trong TreePositions dựa trên BranchPath
        //        query = query.Where(tp => _dbContext.TreeNodes
        //            .Any(n => n.Id == tp.TreeNodeId && n.BranchPath.StartsWith(inviterBranchPath)));
        //    }

        //    // Sắp xếp theo Level (tầng), TreeNodeId (ưu tiên node hiện tại), và LeftOrRightPriority (ưu tiên trái trước phải)
        //    return await query
        //        .OrderBy(tp => tp.Level)
        //        .ThenBy(tp => tp.TreeNodeId)
        //        .ThenBy(tp => tp.LeftOrRightPriority)
        //        .FirstOrDefaultAsync();
        //}














        //private async Task<TreePosition> GetNextAvailablePositionAsync(int? inviterId = null)
        //{
        //    var query = _dbContext.TreePositions.AsQueryable();

        //    if (inviterId.HasValue)
        //    {
        //        // Lấy tất cả các node thuộc nhánh của người mời (inviterId)
        //        var inviterBranchTreeNodeIds = await GetAllTreeNodeIdsInBranchAsync(inviterId.Value);

        //        // Lọc các vị trí trống để chỉ lấy trong nhánh của người mời
        //        query = query.Where(tp => inviterBranchTreeNodeIds.Contains(tp.TreeNodeId));
        //    }

        //    // Sắp xếp theo Level (tầng), sau đó là theo node cha để ưu tiên từ trái sang phải trong cùng cấp
        //    return await query
        //        .OrderBy(tp => tp.Level)
        //        .ThenBy(tp => tp.TreeNodeId) // Sắp xếp theo node cha để ưu tiên node có vị trí trống bên trái trước
        //        .ThenBy(tp => tp.LeftOrRightPriority)
        //        .FirstOrDefaultAsync();
        //}

        //// Hàm đệ quy để lấy tất cả các node ID thuộc nhánh của một node cụ thể (inviterId)
        //private async Task<List<int>> GetAllTreeNodeIdsInBranchAsync(int rootTreeNodeId)
        //{
        //    var result = new List<int> { rootTreeNodeId };
        //    var nodesToCheck = new Queue<int>();
        //    nodesToCheck.Enqueue(rootTreeNodeId);

        //    while (nodesToCheck.Count > 0)
        //    {
        //        var currentTreeNodeId = nodesToCheck.Dequeue();

        //        var children = await _dbContext.TreeNodes
        //            .Where(n => n.ParentId == currentTreeNodeId)
        //            .Select(n => n.Id)
        //            .ToListAsync();

        //        foreach (var childId in children)
        //        {
        //            result.Add(childId);
        //            nodesToCheck.Enqueue(childId);
        //        }
        //    }

        //    return result;
        //}

        //private async Task AddNodeAsync(int userId, int? inviterId = null)
        //{
        //    // Kiểm tra nếu không có vị trí trống trong `TreePositions`, thêm node gốc
        //    if (!_dbContext.TreePositions.Any())
        //    {
        //        await InitializeRootNodeAsync(userId);
        //        return;
        //    }

        //    // Lấy vị trí trống tiếp theo từ `TreePositions`
        //    TreePosition position = await GetNextAvailablePositionAsync(inviterId);

        //    if (position == null)
        //    {
        //        throw new Exception("Không có vị trí trống khả dụng.");
        //    }

        //    // Tạo node mới với UserId và thông tin từ vị trí trống
        //    var newNode = new TreeNode
        //    {
        //        UserId = userId,
        //        Level = position.Level, // Sử dụng Level của vị trí trống
        //        ParentId = position.TreeNodeId,
        //        Position = position.Position
        //    };

        //    _dbContext.TreeNodes.Add(newNode);
        //    await _dbContext.SaveChangesAsync(); // Lưu newNode để có Id hợp lệ

        //    // Cập nhật node cha với newNode.Id
        //    var parentNode = await _dbContext.TreeNodes.FindAsync(position.TreeNodeId);
        //    if (position.Position == "left")
        //    {
        //        parentNode.LeftChildId = newNode.Id;
        //    }
        //    else if (position.Position == "right")
        //    {
        //        parentNode.RightChildId = newNode.Id;
        //    }

        //    _dbContext.TreeNodes.Update(parentNode);

        //    // Xóa vị trí trống vừa được lấp đầy trong `TreePositions`
        //    _dbContext.TreePositions.Remove(position);

        //    // Thêm các vị trí trống mới cho node con của newNode
        //    // Đảm bảo các vị trí được thêm vào đúng thứ tự từ trái sang phải trong cùng cấp độ
        //    _dbContext.TreePositions.Add(new TreePosition
        //    {
        //        TreeNodeId = newNode.Id,
        //        Level = newNode.Level + 1, // Tăng cấp độ cho node con
        //        Position = "left",
        //        LeftOrRightPriority = 0
        //    });

        //    _dbContext.TreePositions.Add(new TreePosition
        //    {
        //        TreeNodeId = newNode.Id,
        //        Level = newNode.Level + 1, // Tăng cấp độ cho node con
        //        Position = "right",
        //        LeftOrRightPriority = 1
        //    });

        //    await _dbContext.SaveChangesAsync();
        //}

        //// Hàm khởi tạo node gốc khi cây chưa có node nào
        //private async Task InitializeRootNodeAsync(int userId)
        //{
        //    var rootNode = new TreeNode
        //    {
        //        UserId = userId,
        //        Level = 1,
        //        Position = "root",
        //        BranchPath= "/1/"
        //    };

        //    _dbContext.TreeNodes.Add(rootNode);
        //    await _dbContext.SaveChangesAsync();

        //    // Thêm các vị trí trống cho con của node gốc vào TreePositions
        //    _dbContext.TreePositions.Add(new TreePosition
        //    {
        //        TreeNodeId = rootNode.Id,
        //        Level = 2, // Cấp độ của con của node gốc là 2
        //        Position = "left",
        //        LeftOrRightPriority = 0
        //    });

        //    _dbContext.TreePositions.Add(new TreePosition
        //    {
        //        TreeNodeId = rootNode.Id,
        //        Level = 2, // Cấp độ của con của node gốc là 2
        //        Position = "right",
        //        LeftOrRightPriority = 1
        //    });

        //    await _dbContext.SaveChangesAsync();
        //}






        //#region đúng nhưng không tối ưu

        ////private async Task<TreeNode> GetNextAvailablePositionAsync(int? inviterId = null)
        ////{
        ////    if (inviterId.HasValue)
        ////    {
        ////        // Trường hợp có người mời, chỉ tìm trong nhánh của người mời
        ////        var inviterNode = await _dbContext.TreeNodes.FindAsync(inviterId);
        ////        if (inviterNode == null) throw new Exception("Người mời không tồn tại trong cây.");

        ////        // Sử dụng hàng đợi để duyệt nhánh của người mời từ trên xuống
        ////        var queue = new Queue<TreeNode>();
        ////        queue.Enqueue(inviterNode);

        ////        while (queue.Count > 0)
        ////        {
        ////            var currentNode = queue.Dequeue();

        ////            // Kiểm tra vị trí trống ở con trái
        ////            if (currentNode.LeftChildId == null)
        ////            {
        ////                return currentNode;
        ////            }
        ////            else
        ////            {
        ////                var leftChild = await _dbContext.TreeNodes.FindAsync(currentNode.LeftChildId);
        ////                if (leftChild != null) queue.Enqueue(leftChild);
        ////            }

        ////            // Kiểm tra vị trí trống ở con phải
        ////            if (currentNode.RightChildId == null)
        ////            {
        ////                return currentNode;
        ////            }
        ////            else
        ////            {
        ////                var rightChild = await _dbContext.TreeNodes.FindAsync(currentNode.RightChildId);
        ////                if (rightChild != null) queue.Enqueue(rightChild);
        ////            }
        ////        }
        ////    }
        ////    else
        ////    {
        ////        // Trường hợp không có người mời, tìm vị trí trống từ gốc cây
        ////        var rootNode = await _dbContext.TreeNodes.FirstOrDefaultAsync(n => n.Position == "root");
        ////        if (rootNode == null) return null;

        ////        var queue = new Queue<TreeNode>();
        ////        queue.Enqueue(rootNode);

        ////        while (queue.Count > 0)
        ////        {
        ////            var currentNode = queue.Dequeue();

        ////            if (currentNode.LeftChildId == null)
        ////            {
        ////                return currentNode;
        ////            }
        ////            else
        ////            {
        ////                var leftChild = await _dbContext.TreeNodes.FindAsync(currentNode.LeftChildId);
        ////                if (leftChild != null) queue.Enqueue(leftChild);
        ////            }

        ////            if (currentNode.RightChildId == null)
        ////            {
        ////                return currentNode;
        ////            }
        ////            else
        ////            {
        ////                var rightChild = await _dbContext.TreeNodes.FindAsync(currentNode.RightChildId);
        ////                if (rightChild != null) queue.Enqueue(rightChild);
        ////            }
        ////        }
        ////    }

        ////    return null; // Trường hợp không tìm thấy vị trí trống
        ////}


        ////private async Task AddNodeAsync(int userId, int? inviterId = null)
        ////{
        ////    // Tìm vị trí trống dựa trên người mời (nếu có) hoặc toàn bộ cây
        ////    TreeNode positionNode = await GetNextAvailablePositionAsync(inviterId);

        ////    if (positionNode == null)
        ////    {
        ////        // Tạo node gốc đầu tiên cho cây
        ////        var rootNode = new TreeNode
        ////        {
        ////            UserId = userId,
        ////            Level = 1,
        ////            Position = "root" // Đánh dấu là node gốc
        ////        };

        ////        _dbContext.TreeNodes.Add(rootNode);
        ////        await _dbContext.SaveChangesAsync();

        ////        // Sau khi thêm node gốc, tạo vị trí trống cho các node con của nó
        ////        _dbContext.TreePositions.Add(new TreePosition
        ////        {
        ////            TreeNodeId = rootNode.Id,
        ////            Level = rootNode.Level + 1,
        ////            Position = "left",
        ////            LeftOrRightPriority = 0
        ////        });

        ////        _dbContext.TreePositions.Add(new TreePosition
        ////        {
        ////            TreeNodeId = rootNode.Id,
        ////            Level = rootNode.Level + 1,
        ////            Position = "right",
        ////            LeftOrRightPriority = 1
        ////        });

        ////        await _dbContext.SaveChangesAsync();

        ////        return;
        ////    }

        ////    // Tạo node mới với UserId và thông tin của node cha
        ////    var newNode = new TreeNode
        ////    {
        ////        UserId = userId,
        ////        Level = positionNode.Level + 1,
        ////        ParentId = positionNode.Id,
        ////        Position = positionNode.LeftChildId == null ? "left" : "right" // Thiết lập vị trí trước khi lưu
        ////    };

        ////    // Lưu newNode vào cơ sở dữ liệu trước khi cập nhật positionNode
        ////    _dbContext.TreeNodes.Add(newNode);
        ////    await _dbContext.SaveChangesAsync(); // Lưu newNode để có Id hợp lệ

        ////    // Cập nhật node cha (positionNode) với newNode.Id sau khi đã lưu newNode
        ////    if (positionNode.LeftChildId == null)
        ////    {
        ////        positionNode.LeftChildId = newNode.Id;
        ////    }
        ////    else if (positionNode.RightChildId == null)
        ////    {
        ////        positionNode.RightChildId = newNode.Id;
        ////    }

        ////    // Cập nhật node cha với thông tin mới
        ////    _dbContext.TreeNodes.Update(positionNode);
        ////    await _dbContext.SaveChangesAsync();
        ////}
        //#endregion




        ////đúng nhưng sai level

        //private async Task<TreePosition> GetNextAvailablePositionAsync2(int? inviterId = null)
        //{
        //    var query = _dbContext.TreePositions.AsQueryable();

        //    if (inviterId.HasValue)
        //    {
        //        // Lọc theo nhánh của người mời (inviter)
        //        query = query.Where(tp => _dbContext.TreeNodes
        //            .Any(un => un.Id == tp.TreeNodeId && un.ParentId == inviterId));
        //    }

        //    // Sắp xếp theo tầng và ưu tiên trái trước phải
        //    return await query
        //        .OrderBy(tp => tp.Level)
        //        .ThenBy(tp => tp.LeftOrRightPriority)
        //        .FirstOrDefaultAsync();

        //}


        //private async Task AddNodeAsync2(int userId, int? inviterId = null)
        //{
        //    // Kiểm tra xem có node nào không để xác định xem đây có phải node gốc không
        //    var position = await GetNextAvailablePositionAsync2(inviterId);

        //    // Nếu không có vị trí trống nào, thêm node gốc
        //    if (position == null)
        //    {
        //        // Tạo node gốc đầu tiên cho cây
        //        var rootNode = new TreeNode
        //        {
        //            UserId = userId,
        //            Level = 1,
        //            Position = "root" // Đánh dấu là node gốc
        //        };

        //        _dbContext.TreeNodes.Add(rootNode);
        //        await _dbContext.SaveChangesAsync();

        //        // Sau khi thêm node gốc, tạo vị trí trống cho các node con của nó
        //        _dbContext.TreePositions.Add(new TreePosition
        //        {
        //            TreeNodeId = rootNode.Id,
        //            Level = rootNode.Level + 1,
        //            Position = "left",
        //            LeftOrRightPriority = 0
        //        });

        //        _dbContext.TreePositions.Add(new TreePosition
        //        {
        //            TreeNodeId = rootNode.Id,
        //            Level = rootNode.Level + 1,
        //            Position = "right",
        //            LeftOrRightPriority = 1
        //        });

        //        await _dbContext.SaveChangesAsync();

        //        return;
        //    }

        //    //var parentNode = await _dbContext.TreeNodes.FindAsync(position.TreeNodeId);
        //    //int newNodeLevel = parentNode.Level + 1;

        //    // Trường hợp thêm node mới vào vị trí trống có sẵn
        //    var newNode = new TreeNode
        //    {
        //        UserId = userId,
        //        //Level = position.Level + 1,
        //        Level = position.Level + 1,
        //        //Level = newNodeLevel,
        //        Position = position.Position,
        //        ParentId = position.TreeNodeId
        //    };

        //    _dbContext.TreeNodes.Add(newNode);
        //    await _dbContext.SaveChangesAsync();

        //    // Cập nhật node cha với node mới
        //    var parentNode = await _dbContext.TreeNodes.FindAsync(position.TreeNodeId);
        //    if (position.Position == "left")
        //    {
        //        parentNode.LeftChildId = newNode.Id;
        //    }
        //    else
        //    {
        //        parentNode.RightChildId = newNode.Id;
        //    }

        //    _dbContext.TreeNodes.Update(parentNode);

        //    // Xóa vị trí trống vừa dùng
        //    _dbContext.TreePositions.Remove(position);

        //    // Thêm các vị trí trống mới của node mới vào TreePositions
        //    _dbContext.TreePositions.Add(new TreePosition
        //    {
        //        TreeNodeId = newNode.Id,
        //        Level = newNode.Level,
        //        Position = "left",
        //        LeftOrRightPriority = 0
        //    });

        //    _dbContext.TreePositions.Add(new TreePosition
        //    {
        //        TreeNodeId = newNode.Id,
        //        Level = newNode.Level,
        //        Position = "right",
        //        LeftOrRightPriority = 1
        //    });

        //    await _dbContext.SaveChangesAsync();
        //}

        #endregion




    }
}
