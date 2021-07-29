using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PALUNA.Utilties
{
    public enum ProductType
    {
        [Description("Điện thoại")]
        Phone,
        [Description("Máy tính")]
        Laptop,
        [Description("Phụ kiện")]
        Accessory
    }

    public enum ProductModel
    {
        [Description("Xiaomi")]
        Xiaomi,
        [Description("SamSung")]
        SamSung,
        [Description("Iphone")]
        Iphone
    }
}