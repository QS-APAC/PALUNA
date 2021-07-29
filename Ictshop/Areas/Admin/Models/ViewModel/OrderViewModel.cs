using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Newtonsoft.Json;
using PALUNA.Models;

namespace PALUNA.Areas.Admin.Models.ViewModel
{
    public class OrderViewModel
    {
        public OrderViewModel(Donhang dh)
        {
            OrderCode = dh.Madon;
            OrderDate = dh.Ngaydat ?? DateTime.MinValue;
            Status = dh.Tinhtrang ?? 0;
            Customer = new CustomerViewModel(dh.Nguoidung);
            OrderDetails = dh.Chitietdonhangs.Select(m => new OrderDetailViewModel(m));
            CustomerDelivery = string.IsNullOrWhiteSpace(dh.DeliveryInfo)
                ? null
                : new CustomerViewModel(JsonConvert.DeserializeObject<Nguoidung>(dh.DeliveryInfo));
            TotalPayment = 0;
            OrderDetails.Aggregate(TotalPayment, (total, addedValue) => { return total + addedValue.Total; });
        }
        public decimal TotalPayment { get; set; }
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public int Status { get; set; }
        public int CustomerCode { get; set; }
        public IEnumerable<OrderDetailViewModel> OrderDetails { get; set; }
        public CustomerViewModel Customer { get; set; }
        public CustomerViewModel CustomerDelivery { get; set; }
    }

    public class OrderDetailViewModel
    {
        public OrderDetailViewModel(Chitietdonhang dh)
        {
            Code = dh.Madon;
            ProductName = dh.Sanpham?.Tensp;
            Quantity = dh.Soluong ?? 0;
            Price = dh.Dongia ?? 0;
        }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Price * Quantity;

    }
    [DataContract]
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {

        }
        public CustomerViewModel(Nguoidung nd)
        {
            Name = nd.Hoten;
            Email = nd.Email;
            Address = nd.Diachi;
            Phone = nd.Dienthoai;
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}