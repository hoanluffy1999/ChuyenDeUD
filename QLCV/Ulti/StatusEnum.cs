using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace QLCV.Ulti
{
    public enum StatusEnum
    {
        [Description("Đang thực hiện")]
        ToDo = 1,
        [Description("Gửi duyệt")]
        InProgress = 2,
        [Description("Hoàn thành")]
        Done = 3
    }
    public enum RateEnum
    {

        [Description("Tốt")]
        Good = 1,
        [Description("Khá")]
        Rather = 2,
        [Description("Trung bình")]
        Medium = 3,
        [Description("Tệ")]
        Bad = 4,
    }
    public enum RoleEnum
    {
        [Description("Admin")]
        SuperAdmin = 1,
        [Description("Quản lý ")]
        Admin = 2,
        [Description("Nhân viên")]
        User = 3
    }
}