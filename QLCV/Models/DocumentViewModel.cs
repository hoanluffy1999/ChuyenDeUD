using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCV.Models
{
    public class DocumentViewModel
    {
        public string ProjectName { get; set; }
        public string EmployeeName { get; set; }
        public byte StatusJob { get; set; }
        public int EmployeeId { get; set; }
    }
    public class ResourceDocument
    {
        public string ProjectName { get; set; }
        public string EmployeeName { get; set; }
        public double Done { get; set; }
        public double ToDo { get; set; }
        public double InProgress { get; set; }
        public double Total { get; set; }
    }
}