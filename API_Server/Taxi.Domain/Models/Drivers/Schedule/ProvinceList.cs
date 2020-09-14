using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Taxi.Domain.Models.Drivers.Schedule
{
    public static class ProvinceList
    {
        public readonly static Hashtable Province = new Hashtable()
        {
            {"Thanh Hoá", 1 },
            {"Nghệ An", 2 },
            {"Hà Tĩnh", 3 },
            {"Quảng Bình",4 },
            {"Quảng Trị",5 },
            {"Thừa Thiên Huế", 6 },
            {"Đà Nẵng", 7 },
            {"Quảng Nam", 8 },
            {"Quãng Ngãi", 9 },
            {"Bình Định", 10 },
            {"Phú Yên", 11 }
        };
    }
}
