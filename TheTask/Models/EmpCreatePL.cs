using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;

namespace TheTask.Models
{
    public class EmpCreatePL
    {
        public decimal EmpNo { get; set; }
        [Required(ErrorMessage = "Input ENAME")]
        public string EmpName { get; set; }
        public string Job { get; set; }
        public decimal? Mgr { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Select Date")]
        public DateTime? HireDate { get; set; }
        [Required(ErrorMessage = "Input SAL")]
        [Range(700, 9999, ErrorMessage = "Min Sal = 700, Max Sal = 9999")]
        public decimal? Sal { get; set; }
        public decimal? Comm { get; set; }
        public decimal DeptNo { get; set; }

        public IEnumerable<SelectListItem> ListDept { get; set; }
        public IEnumerable<SelectListItem> ListMgr { get; set; }
        public IEnumerable<SelectListItem> ListJob { get; set; }
    }
}