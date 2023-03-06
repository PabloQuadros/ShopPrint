using ShopPrint.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrint.Domain.Entities
{
    public class Checkout
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual IUser user { get; set; }
        public int OrderId { get; set; }
        public virtual Order order { get; set; }
        public PaymentOption paymentOption { get; set; }
        public DateTime dateTime { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string road { get; set; }
        public string number { get; set; }
        public string complement { get; set; }
        public string zipcode { get; set; }
        public string cardNumber { get; set; }
        public string cardName { get; set; }
        public string cardType { get; set; }
        public string cvv { get; set; }

    }
}
