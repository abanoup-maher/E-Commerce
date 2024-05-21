using Rocket.Core.Entities;
using Rocket.Core.Entities.Identity.OrderAgreggate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Core.Repository
{
    public interface IUnitOfWork:IDisposable
    {
        #region m4 s7

        //IGenericRepository<Product> productRepo();
        //IGenericRepository<Order> orderRepo();
        //IGenericRepository<OrderItem> orderItemRepo();
        //IGenericRepository<DeliveryMethod> deliveryMethodRepo();

        //IGenericRepository<ProductBrand> brandRepo();

        //IGenericRepository<ProductType> typeRepo(); 
        #endregion

        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        Task<int> Complete();
    }
}
