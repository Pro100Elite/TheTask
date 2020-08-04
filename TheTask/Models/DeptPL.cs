using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheTask.Models
{
    public class DeptPL
    {
        public decimal DeptNo { get; set; }
        [Required(ErrorMessage = "Input Name")]
        [Remote("CheckDeptName", "Dept")]
        public string DeptName { get; set; }
        [Required(ErrorMessage = "Input Loc")]
        public string Loc { get; set; }
    }
}