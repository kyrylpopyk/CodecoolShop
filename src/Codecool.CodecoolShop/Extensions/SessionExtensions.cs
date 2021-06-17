using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Core.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Codecool.CodecoolShop.Extensions
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }

        public static Order GetOrder(this ISession session)
        {
            var order = session.Get<Order>("ShoppingCart");
            return order ?? new Order();
        }

        public static void SaveOrder(this ISession session, Order order)
        {
            session.Set<Order>("ShoppingCart", order);

            var productsCount = order.Items.Sum(item => item.Quantity);

            session.SetInt32("CartItemsCount", productsCount);
            session.SetString("TotalPrice", order.Items.Sum(i => i.Price * i.Quantity).ToString("C2"));
        }
    }
}
