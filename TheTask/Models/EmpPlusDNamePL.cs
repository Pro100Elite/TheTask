﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheTask.Models
{
    public class EmpPlusDNamePL
    {
        public decimal EmpNo { get; set; }
        public string EmpName { get; set; }
        public string Job { get; set; }
        public decimal? Mgr { get; set; }
        public DateTime? HireDate { get; set; }
        public decimal? Sal { get; set; }
        public decimal? Comm { get; set; }
        public decimal? DeptNo { get; set; }
        public string DeptName { get; set; }
    }
}