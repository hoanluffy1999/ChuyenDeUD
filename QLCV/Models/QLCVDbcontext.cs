using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCV.Models
{
    public  class QLCVDbcontext
    {

        private static ChuyenDeDBcontext instance;

        private QLCVDbcontext() { }

        public static ChuyenDeDBcontext getInstance()
        {
            if (instance == null)
            {
                instance = new ChuyenDeDBcontext();
            }
            return instance;
        }
    }
}