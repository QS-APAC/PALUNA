using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PALUNA.Models.Dto
{
    public class ProductModel
    {
        public ProductModel()
        {

        }
        public ProductModel(Sanpham m)
        {
            Tensp = m.Tensp;
            Anhbia = m.Anhbia;
            Giatien = m.Giatien;
            LoaiSp = m.LoaiSp;
            Mahang = m.Mahang;
            Mahdh = m.Mahdh;
            Mota = m.Mota;
            Ram = m.Ram;
            Masp = m.Masp;
            Soluong = m.Soluong;
            Thesim = m.Thesim;
            Sanphammoi = m.Sanphammoi;
            m.Bonhotrong = m.Bonhotrong;
        }
        public int Masp { get; set; }
        public string Tensp { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,0 VND}")]
        public Nullable<decimal> Giatien { get; set; }
        public Nullable<int> Soluong { get; set; }
        public string Mota { get; set; }
        public Nullable<int> Thesim { get; set; }
        public Nullable<int> Bonhotrong { get; set; }
        public Nullable<bool> Sanphammoi { get; set; }
        public Nullable<int> Ram { get; set; }
        public string Anhbia { get; set; }
        public Nullable<int> Mahang { get; set; }
        public Nullable<int> Mahdh { get; set; }
        public Nullable<int> LoaiSp { get; set; }
    }
}