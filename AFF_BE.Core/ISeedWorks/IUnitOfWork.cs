
using AFF_BE.Core.IRepositories;

namespace AFF_BE.Core.ISeedWorks
{
    public interface IUnitOfWork
    {
        public IBannerRepository Banners { get; }
        public INewsRepository News { get; }
        public IPaymentAccountRepository PaymentAccounts { get; }

        public IBrandRepository Brands { get; }

        public IProductCategoryRepository ProductCategories { get; }

        public IProductRepository Products { get; }
        public IOrderRepository Orders { get; } 
        public IOrderDetailRepository OrderDetails { get; }

        public IAddressRepository Address { get; }
        public ICommissionRepository Commissions { get; }

        public ITreeRepository Tree { get; }

        Task<int> CompleteAsync();
    }
}
