using Microsoft.Extensions.Configuration;
using Rocket.Core.Entities;
using Rocket.Core.Entities.Identity.OrderAgreggate;
using Rocket.Core.Repository;
using Rocket.Core.Services;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Rocket.Core.Entities.Product;

namespace Rocket.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IConfiguration _configure;
        private readonly IBasketRepository _basketrepo;
        private readonly IUnitOfWork _unitofwork;

        public PaymentServices(IConfiguration configure,IBasketRepository basketrepo , IUnitOfWork unitofwork)
        {
            _configure = configure;
            _basketrepo = basketrepo;
            _unitofwork = unitofwork;
        }
        // el angular byklm el function de w ybtdy y3ml el payment intent
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            // bklm el stripe
            StripeConfiguration.ApiKey = _configure["StripeSettings:Secretkey"];
            //1
            var basket= await _basketrepo.GetBasketAsync(basketId); 
            //2
            if (basket == null ) return null;
            //4
            var shippingPrice = 0m;

            //3
            if(basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitofwork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                //5
                shippingPrice = deliveryMethod.Cost;//6 hzwd prop decimal shippingPrice in customer basket class
                //7
                basket.shippingPrice = deliveryMethod.Cost;
            }
            //8
            foreach(var item in basket.Items)
            {
                //9 w fe allias name fo2 3nd el using ctrl+.
                var product = await _unitofwork.Repository<Product>().GetByIdAsync(item.Id);
               //bzbt el price bta3 el item ale mawgood gwa el basket
                if(item.Price!= product.Price)
                {
                    item.Price = product.Price;
                }
            }
            //10
            var services = new PaymentIntentService();
            PaymentIntent intent;
            if(string.IsNullOrEmpty(basket.PaymentIntend))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount =(long) basket.Items.Sum(i => i.Quantity * i.Price *100)+(long) shippingPrice*100,/*bdrb fe 100 34an btkon no3a قروش*/
                    Currency="usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };
                intent = await services.CreateAsync(options);
                basket.PaymentIntend = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else /// update payment intent
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * i.Price * 100) + (long)shippingPrice * 100,/*bdrb fe 100 34an btkon no3a قروش*/
                    
                };
                await services.UpdateAsync(basket.PaymentIntend, options);
            }

            await _basketrepo.updateBasketAsync(basket);
            return basket;

        }
    }
}
