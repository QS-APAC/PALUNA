using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PALUNA.Models.Dto;

namespace PALUNA.Models.ViewModel
{
    public class DanhMucViewModel
    {
        public int Mahang { get; set; }
        public string Tenhang { get; set; }

        public IEnumerable<ProductViewModel> Sanphams { get; set; }
    }
    public class ProductViewModel : ProductModel
    {
        public ProductViewModel(Sanpham m) : base(m)
        {
            Tenhang = m.Hangsanxuat?.Tenhang;
        }

        public ProductViewModel() : base()
        {

        }
        public string Tenhang { get; set; }
    }
}