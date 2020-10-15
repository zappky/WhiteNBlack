using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class genericListDataPacket<T1, T2>
    {
        public T1 list1;
        public T2 list2;
        public genericListDataPacket() { }
    }
    public class AdminManageDataPacket : genericListDataPacket<IEnumerable<SmokeFreeApplication.Models.Article>, IEnumerable<SmokeFreeApplication.Models.Doctor>>
    {
    }
}