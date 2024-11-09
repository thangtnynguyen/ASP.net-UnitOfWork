using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.IRepositories;
using AFF_BE.Core.ISeedWorks;
using AFF_BE.Data.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace AFF_BE.Data.SeedWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AffContext _context;

        public UnitOfWork(AffContext context, IMapper mapper, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            Banners = new BannerRepository(context, mapper, httpContextAccessor);
            Brands = new BrandRepository(context, mapper, httpContextAccessor);
            ProductCategories = new ProductCategoryRepostory(context, mapper, httpContextAccessor);
            PaymentAccounts = new PaymentAccountRepository(context, mapper, httpContextAccessor);
            Products = new ProductRepository(context, mapper, httpContextAccessor);
            Orders = new OrderRepository(context, mapper, httpContextAccessor);
            OrderDetails = new OrderDetailRepository(context, mapper, httpContextAccessor);
            Address= new AddressRepository(context, httpContextAccessor);
            Commissions = new CommissionRepository(context, mapper, httpContextAccessor);
        
            Tree= new TreeRepository(context, mapper, httpContextAccessor);
            News = new NewsRepository(context, mapper, httpContextAccessor);
            Commissions = new CommissionRepository(context, mapper, httpContextAccessor);
        }

        public IBannerRepository Banners { get; private set; }
        public INewsRepository News { get; private set; }
        public IPaymentAccountRepository PaymentAccounts { get; private set; }

        public IBrandRepository Brands { get; private set; }

        public IProductCategoryRepository ProductCategories { get; private set; }

        public IProductRepository Products { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IOrderDetailRepository OrderDetails { get; private set; }
        public IAddressRepository Address { get; }
        public ICommissionRepository Commissions { get; private set; }

        public ITreeRepository Tree { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
