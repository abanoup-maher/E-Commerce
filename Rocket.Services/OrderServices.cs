using Microsoft.AspNetCore.Mvc;
using Rocket.Core.Entities;
using Rocket.Core.Entities.Identity.OrderAgreggate;
using Rocket.Core.Repository;
using Rocket.Core.Services;
using Rocket.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Services
{
    public class OrderServices : IOrderService
    {
        private readonly IBasketRepository _basketrepo;
        private readonly IUnitOfWork _unitofwork;
        private readonly IPaymentServices _paymentservice;
        //private readonly IGenericRepository<Order> _orderrepo;
        //private readonly IGenericRepository<DeliveryMethod> _deliverymethodrepo;
        //private readonly IGenericRepository<Product> _productrepo;

        public OrderServices
            (IBasketRepository basketrepo ,
             //IGenericRepository<Order> orderrepo,
             //IGenericRepository<DeliveryMethod> deliverymethodrepo,
             //IGenericRepository<Product> productrepo
             IUnitOfWork unitofwork,
             IPaymentServices paymentservice
            )
        {
            _basketrepo = basketrepo;
            _unitofwork = unitofwork;
            _paymentservice = paymentservice;

            //_orderrepo = orderrepo;
            //_deliverymethodrepo = deliverymethodrepo;
            //_productrepo = productrepo;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deleveryMethodId, Address ShippingAddress)
        {
            //get basket from basketrepo
            var basket = await _basketrepo.GetBasketAsync(basketId);
            //get selected item at basket from productrepo
            var orderitems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _unitofwork.Repository<Product>().GetByIdAsync(item.Id);
                var productitemorder = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
                var orderitem = new OrderItem(productitemorder, product.Price, item.Quantity);
                orderitems.Add(orderitem);
            }
            //calculate subtotal
            var subtotal = orderitems.Sum(item=> item.Price*item.quantity);
            //get delivery method from delivery repo
            var deliverymethod = await _unitofwork.Repository<DeliveryMethod>().GetByIdAsync(deleveryMethodId);
            //create order
            var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntend);
            var existingOrder = await _unitofwork.Repository<Order>().GetByIdwithspecAsync(spec);
            if (existingOrder != null)
            {
                _unitofwork.Repository<Order>().Delete(existingOrder);
                await _paymentservice.CreateOrUpdatePaymentIntent(basketId);
            }

            var order = new Order(buyerEmail, ShippingAddress, deliverymethod, orderitems, subtotal , basket.PaymentIntend);
            await _unitofwork.Repository<Order>().CreateAsync(order);
            // save to database [to do]

           await _unitofwork.Complete(); 

            return order;
        }




      
           



        public async Task<Order> GetOrderByIdAsync(int orderid, string buyeremail)
        {
            var spec = new OrderWithItemAndDeliveryMethodSpecification(orderid ,buyeremail);
            var order = await _unitofwork.Repository<Order>().GetByIdwithspecAsync(spec);
            return order;
        }

          


        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyeremail)
        {
            var spec = new OrderWithItemAndDeliveryMethodSpecification(buyeremail);
            var orders = await _unitofwork.Repository<Order>().GetAllwithspecAsync (spec);
            return (IReadOnlyList<Order>) orders;
        }
        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            var deliverymethod = await _unitofwork.Repository<DeliveryMethod>().GetAllAsync();
            return deliverymethod;
        }
    }
}
