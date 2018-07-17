using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCar.Models.Cart
{
    //儲存單一商品
    [Serializable]//可序列化
    public class CartItem
    {
        //商品編號
        public int Id { get; set; }

        //商品名稱
        public string Name { get; set; }

        //商品價格
        public decimal Price { get; set; }

        //購買時數量
        public int Quantity { get; set; }

        //商品小計
        public decimal Amount
        {
            get { return this.Price * this.Quantity; }
        }


        public string DefaultImageURL { get; set; }
    }
}