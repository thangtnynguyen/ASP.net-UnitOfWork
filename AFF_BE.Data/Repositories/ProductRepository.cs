using AFF_BE.Core.Constants.System;
using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Data.Payment;
using AFF_BE.Core.Exceptions;
using AFF_BE.Core.Helpers;
using AFF_BE.Core.IRepositories;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Content.Banner;
using AFF_BE.Core.Models.Content.Brand;
using AFF_BE.Core.Models.Content.Product;
using AFF_BE.Core.Models.Content.ProductImage;
using AFF_BE.Core.Models.Content.ProductVariant;
using AFF_BE.Data.SeedWorks;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AFF_BE.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product, int>, IProductRepository
    {
        private const int VIEW_MAX_PER_CUSTOMER = 2;

        private readonly IMapper _mapper;

        public ProductRepository(AffContext dbContext,IMapper mapper, IHttpContextAccessor httpContext) : base(dbContext, httpContext)
        {
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> SearchProductsByKeyWord(SearchProductByKeyWord option)
        {
            var query = _dbContext.Products.Where(p => p.IsDeleted != true && p.Name.Contains(option.KeyWord));
            switch (option.SortBy)
            {
                case "desc":
                    query = query.OrderByDescending(p => p.ViewCounts);
                    break;
                case "asc":
                    query = query.OrderBy(p => p.ViewCounts);
                    break;
                default:
                    query = query.OrderBy(p => p.ViewCounts);
                    break;
            }
            List<Product> products = await query
                                            .Skip(option.PageSize * (option.PageIndex - 1))
                                                  .Take(option.PageSize).ToListAsync();
            List<ProductDto> result = _mapper.Map<List<ProductDto>>(products);
            return result;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            List<Product> products = await _dbContext.Products.Where(p => p.IsDeleted != true).ToListAsync();
            List<ProductDto> result = _mapper.Map<List<ProductDto>>(products);
            return result;
        }

        public async Task<PagingResult<ProductManagementDto>> GetProductsForStoreAsync(PagingRequest paging)
        {
            var query = _dbContext.Products
                .Include(p => p.ProductVariants)
                .Include(p => p.ProductImages)
                .Where(p => p.IsDeleted != true)
                .OrderByDescending(p => p.UpdatedAt);

            int totalRecords = query.Count();
            List<Product> products = await query.Skip((paging.PageIndex - 1) * paging.PageSize)
            .Take(paging.PageSize)
                .ToListAsync();
            ProductDto.TotalRecordsCount = totalRecords;
            List<ProductManagementDto> data = _mapper.Map<List<ProductManagementDto>>(products);

            var result = new PagingResult<ProductManagementDto>(data, paging.PageIndex, paging.PageSize, paging.SortBy, paging.OrderBy, totalRecords);
            return result;
        }
        public async Task<ProductManagementDto> GetOneProductManagementDto(int id)
        {
            Product? product = await _dbContext.Products.Include(p => p.ProductVariants.Where(pv => pv.IsDeleted != 1)).Include(p => p.ProductImages).Where(p => p.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                throw new NotFoundException($"product with {id} does not exist");
            }
            return _mapper.Map<ProductManagementDto>(product);
        }
        public async Task<PagingResult<ProductManagementDto>> FilterProductsForStoreAsync(SearchProductByKeyWord options)
        {
            var query = _dbContext.Products.Include(p => p.ProductImages).AsQueryable();


            if (options.StartPrice.HasValue)
            {
                query=query.Where(p=>p.SellingPrice>=options.StartPrice.Value);
            }
            if (options.EndPrice.HasValue) {
                query = query.Where(p => p.SellingPrice <= options.EndPrice.Value);

            }
            if (!string.IsNullOrEmpty(options.KeyWord))
            {
                query = query.Where(p=>p.Name.Contains(options.KeyWord)|| p.CategoryName.Contains(options.KeyWord));
            }

            if (options.CategoryId.HasValue)
            {
                query=query.Where(p=>p.CategoryId==options.CategoryId.Value);
            }

            var total = query.Count();


            if (string.IsNullOrEmpty(options.OrderBy) && string.IsNullOrEmpty(options.SortBy))
            {
                query = query.OrderByDescending(p => p.Id);
            }
            else if (string.IsNullOrEmpty(options.OrderBy))
            {
                if (options.SortBy == SortByConstant.Asc)
                {
                    query = query.OrderBy(p => p.Id);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Id);
                }
            }
            else if (string.IsNullOrEmpty(options.SortBy))
            {
                query = query.OrderByDescending(p => p.Id);
            }
            else
            {
                if (options.OrderBy == OrderByConstant.Id && options.SortBy == SortByConstant.Asc)
                {
                    query = query.OrderBy(p => p.Id);
                }
                else if (options.OrderBy == OrderByConstant.Id && options.SortBy == SortByConstant.Desc)
                {
                    query = query.OrderByDescending(p => p.Id);
                }

                if (options.OrderBy == OrderByConstant.SellingPrice && options.SortBy == SortByConstant.Asc)
                {
                    query = query.OrderBy(p => p.SellingPrice);
                }
                else if (options.OrderBy == OrderByConstant.SellingPrice && options.SortBy == SortByConstant.Desc)
                {
                    query = query.OrderByDescending(p => p.SellingPrice);
                }

                if (options.OrderBy == OrderByConstant.CreatedAt && options.SortBy == SortByConstant.Asc)
                {
                    query = query.OrderBy(p => p.CreatedAt);
                }
                else if (options.OrderBy == OrderByConstant.CreatedAt && options.SortBy == SortByConstant.Desc)
                {
                    query = query.OrderByDescending(p => p.CreatedAt);
                }
            }


            var data = await query.Skip((options.PageIndex - 1) * options.PageSize).Take(options.PageSize).ToListAsync();
            
            var productDtos= _mapper.Map<List<ProductManagementDto>>(data);

            var result = new PagingResult<ProductManagementDto>(productDtos, options.PageIndex, options.PageSize, total);

            return result;
        }
        public async Task<List<ProductManagementDto>> FilterProductsForStoreAsync2(SearchProductByKeyWord options)
        {
            var query = from p in _dbContext.Products
                        join v in _dbContext.ProductVariants on p.Id equals v.ProductId into pv
                        from v in pv.DefaultIfEmpty()
                        join img in _dbContext.ProductImages on p.Id equals img.ProductId into pimg
                        from img in pimg.DefaultIfEmpty()
                        where p.IsDeleted != true
                              && (string.IsNullOrEmpty(options.KeyWord) ? true :
                                  (p.Name + " " + p.Sku + " " + v.Sku + " " + p.Barcode + " " + v.Barcode).Contains(options.KeyWord))
                              && (options.CategoryId == null ? true : p.CategoryId == options.CategoryId)
                              && (options.StartPrice == null ? true : (p.SellingPrice <= 0 ? (v != null && v.Price >= options.StartPrice) : p.SellingPrice >= options.StartPrice))
                              && (options.EndPrice == null ? true : (p.SellingPrice <= 0 ? (v != null && v.Price <= options.EndPrice) : p.SellingPrice <= options.EndPrice))
                        orderby p.UpdatedAt descending
                        select new { p, v, img };

            var tolistquery = await (from p in query
                                     group p by p.p.Id into grouped
                                     select new
                                     {
                                         ProductId = grouped.Key,
                                         Grouped = grouped.ToList(),
                                     }).ToListAsync();

            var resultQuery = tolistquery.OrderByDescending(p => p.Grouped[0].p.UpdatedAt)
                                         .Skip((options.PageIndex - 1) * options.PageSize)
                                         .Take(options.PageSize);

            List<Product> products = new List<Product>();
            foreach (var item in resultQuery)
            {
                products.Add(item.Grouped[0].p);
            }

            var productDtos = _mapper.Map<List<ProductManagementDto>>(products);

            return productDtos;
        }


        public async Task<ProductDto> GetProductAsync(int id)
        {

            Product? product = await GetByIdAsync(id);
            if (product == null) { throw new ArgumentException("The product does not exist"); }
            ProductDto result = _mapper.Map<ProductDto>(product);
            return result;
        }
        private ProductImage CreateProductImageAsync(string linkimg)
        {
            ProductImage img = new ProductImage();
            img.CreatedAt = DateTime.Now;
            img.Link = linkimg;
            return img;
        }
        public async Task AddImages(int productId, string base64_image)
        {
            Product? product = await GetByIdAsync(productId);
            if (product == null) { throw new ArgumentException("The product does not exist"); }
            UploadHepler upload = new UploadHepler();
            byte[] bytes = Convert.FromBase64String(base64_image);
            string linkimg = await upload.UploadImageWithBytes(bytes);
            ProductImage img = CreateProductImageAsync(linkimg);
            img.ProductId = productId;
            _dbContext.ProductImages.Add(img);
            await SaveChangesAsync();
        }
        public async Task UpdateImages(int imageId, string base64_image)
        {
            ProductImage? productImage = await _dbContext.ProductImages.Where(productImage => productImage.Id == imageId).FirstOrDefaultAsync();
            if (productImage == null) { throw new ArgumentException("The productImage does not exist"); }
            UploadHepler upload = new UploadHepler();
            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Public\\Images\\");
            upload.DeleteFile(uploadPath + productImage.Link);
            byte[] bytes = Convert.FromBase64String(base64_image);
            string linkimg = await upload.UploadImageWithBytes(bytes);
            productImage.Link = linkimg;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteImages(int imageId)
        {
            ProductImage? productImage = await _dbContext.ProductImages.Where(productImage => productImage.Id == imageId).FirstOrDefaultAsync();
            if (productImage == null) { throw new ArgumentException("The productImage does not exist"); }
            UploadHepler upload = new UploadHepler();
            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Public\\Images\\");
            upload.DeleteFile(uploadPath + productImage.Link);
            _dbContext.ProductImages.Remove(productImage);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Product> CreateProductWithFile(CreateProductWithFileRequest productDto)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    Product newProduct = _mapper.Map<Product>(productDto);

                    // Check foreign key
                    ProductCategory? category = await _dbContext.ProductCategories.Where(category => category.Id == productDto.CategoryId).FirstOrDefaultAsync();

                    Brand? brand = null;
                    if (productDto.BrandId.HasValue)
                    {
                        brand = await _dbContext.Brands.Where(brand => brand.Id == productDto.BrandId).FirstOrDefaultAsync();
                    }

                    if (category == null)
                    {
                        throw new ForeignKeyConstraintException("Err for foreign key (brand, unit, category)");
                    }
                    // Upload
                    UploadHepler upload = new UploadHepler();
                    if (productDto.Base64_FileIamges != null)
                    {
                        int forloopcount = productDto.Base64_FileIamges.Length;
                        for (int i = 0; i < forloopcount; i++)
                        {
                            byte[] bytes = Convert.FromBase64String(productDto.Base64_FileIamges[i]);
                            string path = await upload.UploadImageWithBytes(bytes);
                            newProduct.ProductImages.Add(CreateProductImageAsync(path));
                        }
                    }
                    if (productDto.Base64_FileVideo != null)
                    {
                        byte[] bytes = Convert.FromBase64String(productDto.Base64_FileVideo);
                        string path = await upload.UploadVideoWithBytes(bytes);
                        newProduct.LinkVideo = path;
                    }
                    // Create new ProductVariant
                    int j = 0;
                    foreach (var item in newProduct.ProductVariants)
                    {
                        item.Code = StringHelper.GenerateCode(15);
                        if (!string.IsNullOrEmpty(productDto.productVariants[j].Base64_FileImage))
                        {
                            byte[] bytes = Convert.FromBase64String(productDto.productVariants[j].Base64_FileImage);
                            string linkImg = await upload.UploadImageWithBytes(bytes);
                            item.LinkImage = linkImg;
                        }
                        j++;
                    }

                    // Set product details
                    //newProduct.StoreId = token.StoreId;
                    //newProduct.StoreName = token.StoreName;
                    newProduct.BrandName = brand?.Name ?? string.Empty;
                    newProduct.CategoryName = category.Name;
                    //newProduct.SellingPrice = newProduct.ProductVariants.Count == 0 ? newProduct.SellingPrice : 0;
                    //newProduct.ImportPrice = newProduct.ProductVariants.Count == 0 ? newProduct.ImportPrice : 0;

                    await _dbContext.Products.AddAsync(newProduct);

                    // Commit
                    await SaveChangesAsync();
                    transaction.Commit();

                    return newProduct;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public async Task UpdateProductAsync(UpdateProductManagementRequest productDto)
        {

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    //PayloadToken token = JwtToken.VerifyJwtToken(httpContext, _configuration);
                    UploadHepler upload = new UploadHepler();
                    Product updateProduct = _mapper.Map<Product>(productDto);
                    Product? IsExits = await GetByIdAsync(productDto.Id);
                    if (IsExits == null) { throw new ArgumentException("The product does not exist"); }
                    if (productDto.CategoryId != null)
                    {
                        ProductCategory? category = await _dbContext.ProductCategories.Where(category => category.Id == productDto.CategoryId).FirstOrDefaultAsync();
                        updateProduct.CategoryName = category.Name;
                    }
                    if (productDto.BrandId != null)
                    {
                        Brand? brand = await _dbContext.Brands.Where(brand => brand.Id == productDto.BrandId).FirstOrDefaultAsync();
                        updateProduct.BrandName = brand.Name;
                    }
                    if (productDto.LinkVideo == null || productDto.LinkVideo == "")
                    {
                        string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Public\\Videos\\");
                        upload.DeleteFile(uploadPath + updateProduct.LinkVideo);
                        updateProduct.LinkVideo = "";
                    }
                    if (productDto.Base64_FileVideo != null)
                    {
                        byte[] bytes = Convert.FromBase64String(productDto.Base64_FileVideo);
                        string path = await upload.UploadVideoWithBytes(bytes);
                        string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Public\\Videos\\");
                        upload.DeleteFile(uploadPath + updateProduct.LinkVideo);
                        updateProduct.LinkVideo = path;
                    }
                    //if (updateProduct.ProductVariants != null)
                    //{
                    //    if (updateProduct.ProductVariants.Count != 0)
                    //    {
                    //        updateProduct.TotalQuantity = 0;
                    //    }
                    //}
                    for (int i = 0; i < productDto.ProductVariants.Count(); i++)
                    {
                        UpdateProductVariantManagementRequest item = productDto.ProductVariants[i];
                        if (item.IsDeleted == 1)
                        {
                            //updateProduct.TotalQuantity -= item.Quantity ?? 0;
                        }
                        else
                        {
                            //updateProduct.TotalQuantity += item.Quantity ?? 0;
                        }
                        if (item.Id > 0)
                        {
                            ProductVariant productVariant = _mapper.Map<ProductVariant>(item);
                            if (item.Base64_FileImage != null)
                            {
                                byte[] bytes = Convert.FromBase64String(item.Base64_FileImage);
                                string linkImg = await upload.UploadImageWithBytes(bytes);
                                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Public\\Images\\");
                                upload.DeleteFile(uploadPath + item.LinkImage);
                                productVariant.LinkImage = linkImg;
                            }
                            ProductVariant? exist = _dbContext.Set<ProductVariant>().Find(item.Id);
                            _dbContext.Entry(exist).CurrentValues.SetValues(productVariant);
                            await _dbContext.SaveChangesAsync();
                        }
                        else
                        {
                            ProductVariant productVariant = _mapper.Map<ProductVariant>(item);
                            productVariant.Code = StringHelper.GenerateRandomCode(8);
                            if (item.Base64_FileImage != null)
                            {
                                byte[] bytes = Convert.FromBase64String(item.Base64_FileImage);
                                string linkImg = await upload.UploadImageWithBytes(bytes);
                                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Public\\Images\\");
                                upload.DeleteFile(uploadPath + item.LinkImage);
                                productVariant.LinkImage = linkImg;
                                productVariant.Code = StringHelper.GenerateRandomCode(8);
                            }
                            await _dbContext.ProductVariants.AddAsync(productVariant);
                        }
                    }

                    // Commit
                    updateProduct.ProductVariants = null;
                    //updateProduct.StoreId = token.StoreId;
                    //updateProduct.StoreName = token.StoreName;
                    await UpdateAsync(updateProduct);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public async Task ChangeProductStatus(int Id)
        {
            Product? product = await GetByIdAsync(Id);
            if (product == null)
            {
                throw new NoDataException("The product does not exist");
            }
            product.Status = product.Status == 1 ? 0 : 1;
            await UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            Product? product = await GetByIdAsync(id);
            if (product == null) { throw new NoDataException("Not found"); }
            product.IsDeleted = true;
            await UpdateAsync(product);
        }

        public Task<List<ProductVariantDto>> GetProductVariants(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckBarcode(string barCode, int? id = null)
        {
            IQueryable<Product> query = _dbContext.Products
            .Where(b => b.Barcode == barCode && b.IsDeleted != true);

            if (id.HasValue)
            {
                query = query.Where(b => b.Id != id.Value);
            }

            var existing = await query.FirstOrDefaultAsync();

            return existing != null;
        }
        public async Task<bool> CheckSKU(string sku, int? id = null)
        {
            IQueryable<Product> query = _dbContext.Products
                .Where(b => b.Sku == sku && b.IsDeleted != true);

            if (id.HasValue)
            {
                query = query.Where(b => b.Id != id.Value);
            }

            var existing = await query.FirstOrDefaultAsync();

            return existing != null;
        }



    }
}
