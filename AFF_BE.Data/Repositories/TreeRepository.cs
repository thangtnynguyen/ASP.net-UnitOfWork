using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Data.Tree;
using AFF_BE.Core.IRepositories;
using AFF_BE.Core.Models.Identity.User;
using AFF_BE.Core.Models.Tree;
using AFF_BE.Data.SeedWorks;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Data.Repositories
{
    public class TreeRepository : RepositoryBase<TreeNode, int>, ITreeRepository
    {


        private readonly IMapper _mapper;
        public TreeRepository(AffContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _mapper = mapper;
        }


        public async Task<bool> AddUserToTreeUseBranchPath(CreateNoteToTreeRequest request)
        {
            var noteUser = _dbContext.TreeNodes.Where(x => x.UserId == request.UserId).FirstOrDefault();
            if (noteUser != null)
            {
                throw new Exception("Người dùng đã tồn tại trong cây.");

            }
            if (!_dbContext.TreePositions.Any())
            {
                await InitializeRootNodeAsync(request.UserId);
                return true;
            }

            TreePosition position = await GetNextAvailablePositionUserBranchPathAsync(request.InviterId);

            if (position == null)
            {
                throw new Exception("Không có vị trí trống khả dụng.");
            }

            var parentNode = await _dbContext.TreeNodes.FindAsync(position.TreeNodeId);
            if (parentNode == null)
            {
                throw new Exception("Node cha không tồn tại.");
            }

            var temporaryBranchPath = parentNode.BranchPath + "temp/";

            var newNode = new TreeNode
            {
                UserId = request.UserId,
                Level = position.Level,
                ParentId = position.TreeNodeId,
                Position = position.Position,
                BranchPath = temporaryBranchPath
            };

            _dbContext.TreeNodes.Add(newNode);
            await _dbContext.SaveChangesAsync();

            newNode.BranchPath = parentNode.BranchPath + newNode.Id + "/";
            _dbContext.TreeNodes.Update(newNode);

            if (position.Position == "left")
            {
                parentNode.LeftChildId = newNode.Id;
            }
            else if (position.Position == "right")
            {
                parentNode.RightChildId = newNode.Id;
            }

            _dbContext.TreeNodes.Update(parentNode);

            _dbContext.TreePositions.Remove(position);

            _dbContext.TreePositions.Add(new TreePosition
            {
                TreeNodeId = newNode.Id,
                Level = newNode.Level + 1,
                Position = "left",
                LeftOrRightPriority = 0
            });

            _dbContext.TreePositions.Add(new TreePosition
            {
                TreeNodeId = newNode.Id,
                Level = newNode.Level + 1,
                Position = "right",
                LeftOrRightPriority = 1
            });

            await _dbContext.SaveChangesAsync();

            return true;

        }


        private async Task<TreePosition> GetNextAvailablePositionUserBranchPathAsync(int? inviterId = null)
        {

            var query = _dbContext.TreePositions.AsQueryable();

            if (inviterId.HasValue)
            {
                var noteUser = _dbContext.TreeNodes.Where(x => x.UserId == inviterId.Value).FirstOrDefault();
                if (noteUser == null)
                {
                    throw new Exception("Người mời không tồn tại trong cây.");

                }
                var inviterBranchPath = await _dbContext.TreeNodes
                    .Where(n => n.Id == noteUser.Id)
                    .Select(n => n.BranchPath)
                    .FirstOrDefaultAsync();

                if (string.IsNullOrEmpty(inviterBranchPath))
                {
                    throw new Exception("Người mời không tồn tại trong cây hoặc không có BranchPath.");
                }

                query = query.Where(tp => _dbContext.TreeNodes.Any(n =>
                          n.Id == tp.TreeNodeId
                          && n.BranchPath.StartsWith(inviterBranchPath)
                          && (n.LeftChildId == null || n.RightChildId == null) ));
            }

            return await query
                .OrderBy(tp => tp.Level)
                .ThenBy(tp => tp.TreeNodeId)
                .ThenBy(tp => tp.LeftOrRightPriority)
                .FirstOrDefaultAsync();
        }







        private async Task<TreePosition> GetNextAvailablePositionAsync(int? inviterId = null)
        {
            var query = _dbContext.TreePositions.AsQueryable();

            if (inviterId.HasValue)
            {
                var noteUser = _dbContext.TreeNodes.Where(x => x.UserId == inviterId.Value).FirstOrDefault();
                if (noteUser == null)
                {
                    throw new Exception("Người mời không tồn tại trong cây.");

                }
                var inviterBranchTreeNodeIds = await GetAllTreeNodeIdsInBranchAsync(noteUser.Id);

                query = query.Where(tp => inviterBranchTreeNodeIds.Contains(tp.TreeNodeId));
            }

            return await query
                .OrderBy(tp => tp.Level)
                .ThenBy(tp => tp.TreeNodeId)
                .ThenBy(tp => tp.LeftOrRightPriority)
                .FirstOrDefaultAsync();
        }

        private async Task<List<int>> GetAllTreeNodeIdsInBranchAsync(int rootTreeNodeId)
        {
            var result = new List<int> { rootTreeNodeId };
            var nodesToCheck = new Queue<int>();
            nodesToCheck.Enqueue(rootTreeNodeId);

            while (nodesToCheck.Count > 0)
            {
                var currentTreeNodeId = nodesToCheck.Dequeue();

                var children = await _dbContext.TreeNodes
                    .Where(n => n.ParentId == currentTreeNodeId)
                    .Select(n => n.Id)
                    .ToListAsync();

                foreach (var childId in children)
                {
                    result.Add(childId);
                    nodesToCheck.Enqueue(childId);
                }
            }

            return result;
        }

        public async Task<bool> AddUserToTree(CreateNoteToTreeRequest request)
        {
            if (!_dbContext.TreePositions.Any())
            {
                await InitializeRootNodeAsync(request.UserId);
                return true;
            }

            TreePosition position = await GetNextAvailablePositionAsync(request.InviterId);

            if (position == null)
            {
                throw new Exception("Không có vị trí trống khả dụng.");
            }

            var newNode = new TreeNode
            {
                UserId = request.UserId,
                Level = position.Level,
                ParentId = position.TreeNodeId,
                Position = position.Position
            };

            _dbContext.TreeNodes.Add(newNode);
            await _dbContext.SaveChangesAsync();

            var parentNode = await _dbContext.TreeNodes.FindAsync(position.TreeNodeId);
            if (position.Position == "left")
            {
                parentNode.LeftChildId = newNode.Id;
            }
            else if (position.Position == "right")
            {
                parentNode.RightChildId = newNode.Id;
            }

            _dbContext.TreeNodes.Update(parentNode);

            _dbContext.TreePositions.Remove(position);

            _dbContext.TreePositions.Add(new TreePosition
            {
                TreeNodeId = newNode.Id,
                Level = newNode.Level + 1,
                Position = "left",
                LeftOrRightPriority = 0
            });

            _dbContext.TreePositions.Add(new TreePosition
            {
                TreeNodeId = newNode.Id,
                Level = newNode.Level + 1,
                Position = "right",
                LeftOrRightPriority = 1
            });

            await _dbContext.SaveChangesAsync();
            return true;

        }

        private async Task InitializeRootNodeAsync(int userId)
        {
            var rootNode = new TreeNode
            {
                UserId = userId,
                Level = 0,
                Position = "root",
                BranchPath = "/1/"
            };

            _dbContext.TreeNodes.Add(rootNode);
            await _dbContext.SaveChangesAsync();

            _dbContext.TreePositions.Add(new TreePosition
            {
                TreeNodeId = rootNode.Id,
                Level = 1,
                Position = "left",
                LeftOrRightPriority = 0
            });

            _dbContext.TreePositions.Add(new TreePosition
            {
                TreeNodeId = rootNode.Id,
                Level = 1,
                Position = "right",
                LeftOrRightPriority = 1
            });

            await _dbContext.SaveChangesAsync();
        }



        public async Task<TreeNodeDto> GetTree(GetPositionTreeRequest request)
        {
            var noteUser = await _dbContext.TreeNodes
                .Where(x => x.UserId == request.UserId)
                .FirstOrDefaultAsync();

            if (noteUser == null)
            {
                throw new Exception("Người dùng chưa tồn tại trong cây.");
            }
            if (!request.DownLevel.HasValue)
            {
                request.DownLevel = 5;
            }
            request.DownLevel = Math.Min((int)request.DownLevel, 5);

            var currentNode = await _dbContext.TreeNodes.Include(tr => tr.User)
                .Where(tr => tr.Id == noteUser.Id)
                .FirstOrDefaultAsync();

            if (currentNode == null) return null;

            var currentDto = _mapper.Map<TreeNodeDto>(currentNode);

            if (currentNode.ParentId.HasValue)
            {
                var parentNode = await _dbContext.TreeNodes.Include(tr => tr.User).Where(tr => tr.Id == currentNode.ParentId).FirstOrDefaultAsync();
                if (parentNode != null)
                {

                    var parentNodeDto = _mapper.Map<TreeNodeDto>(parentNode);

                    currentDto.Parent = parentNodeDto;
                }
            }
            await AddDescendantsUseBranchPath(currentDto, currentNode.BranchPath, (int)request.DownLevel);

            return currentDto;
        }



        private async Task AddDescendantsUseBranchPath(TreeNodeDto currentDto, string branchPath, int downLevels)
        {
            var maxLevel = currentDto.Level + downLevels;

            var descendants = await _dbContext.TreeNodes.Include(tr => tr.User)
                .Where(x => x.BranchPath.StartsWith(branchPath) && x.Level <= maxLevel && x.Id != currentDto.Id)
                .OrderBy(x => x.Level)
                .ToListAsync();

            var nodeDictionary = new Dictionary<int, TreeNodeDto>();
            nodeDictionary[currentDto.Id] = currentDto;

            foreach (var descendant in descendants)
            {

                var descendantDto = _mapper.Map<TreeNodeDto>(descendant);

                if (nodeDictionary.ContainsKey(descendant.ParentId.Value))
                {
                    var parentDto = nodeDictionary[descendant.ParentId.Value];
                    parentDto.Children.Add(descendantDto);
                }

                nodeDictionary[descendant.Id] = descendantDto;
            }
        }







        #region parent
        private async Task AddAncestorsUseBranchPath(TreeNodeDto rootDto, string branchPath)
        {
            var ancestors = await _dbContext.TreeNodes
                .Where(x => branchPath.StartsWith(x.BranchPath) && x.Id != rootDto.Id)
                .OrderByDescending(x => x.Level)
                .ToListAsync();

            TreeNodeDto? previous = null;

            foreach (var ancestor in ancestors)
            {
                var ancestorDto = new TreeNodeDto
                {
                    Id = ancestor.Id,
                    UserId = ancestor.UserId,
                    ParentId = ancestor.ParentId,
                    User = _mapper.Map<UserTreeDto>(ancestor.User),
                    Children = previous != null ? new List<TreeNodeDto> { previous } : new List<TreeNodeDto>()
                };

                previous = ancestorDto;
            }

            if (previous != null)
            {
                rootDto.Parent = previous;
            }
        }

        #endregion 



        #region Recusive
        //public async Task<TreeNodeDto> GetTree(GetPositionTreeRequest request)
        //{
        //    var noteUser= _dbContext.TreeNodes.Where(x => x.UserId == request.UserId).FirstOrDefault();
        //    if (noteUser == null) {
        //        throw new Exception("Người dùng chưa tồn tại trong cây.");

        //    }
        //    request.DownLevel = Math.Min((int)request.DownLevel, 5);

        //    var currentNode = await _dbContext.TreeNodes.Include(tr=>tr.User).Where(tr=>tr.Id== noteUser.Id).FirstOrDefaultAsync();
        //    if (currentNode == null) return null;

        //    var rootDto = new TreeNodeDto
        //    {
        //        Id = currentNode.Id,
        //        UserId = currentNode.UserId,
        //        ParentId = currentNode.ParentId,
        //        User = _mapper.Map<UserTreeDto>(currentNode.User)
        //    };

        //    if (currentNode.ParentId.HasValue)
        //    {
        //        var parentNode = await _dbContext.TreeNodes.Include(tr => tr.User).Where(tr => tr.Id == currentNode.ParentId).FirstOrDefaultAsync();
        //        if (parentNode != null)
        //        {
        //            rootDto.Parent = new TreeNodeDto
        //            {
        //                Id = parentNode.Id,
        //                UserId = parentNode.UserId,
        //                ParentId = parentNode.ParentId,
        //                User = _mapper.Map<UserTreeDto>(parentNode.User)

        //            };
        //        }
        //    }

        //    await AddDescendants(rootDto, (int)request.DownLevel);

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
        //            Children = new List<TreeNodeDto> { rootDto }
        //        };

        //        await AddAncestors(parentDto, parentNode.ParentId, levelsRemaining - 1);

        //        rootDto.ParentId = parentDto.Id;
        //    }
        //}

        //private async Task AddDescendants(TreeNodeDto rootDto, int levelsRemaining)
        //{
        //    if (levelsRemaining <= 0) return;

        //    var children = await _dbContext.TreeNodes.Include(tr=>tr.User)
        //        .Where(x => x.ParentId == rootDto.Id)
        //        .ToListAsync();

        //    foreach (var child in children)
        //    {
        //        var childDto = new TreeNodeDto
        //        {
        //            Id = child.Id,
        //            UserId = child.UserId,
        //            ParentId = rootDto.Id,
        //            User = _mapper.Map<UserTreeDto>(child.User)

        //        };

        //        rootDto.Children.Add(childDto);

        //        await AddDescendants(childDto, levelsRemaining - 1);
        //    }
        //}
        #endregion
    }
}
