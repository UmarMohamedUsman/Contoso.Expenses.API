﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Expenses.API.Models
{
    public class CostCenter
    {
        [Key]
        public string SubmitterEmail { get; set; }
        public string ApproverEmail { get; set; }
        public string CostCenterName { get; set; }
    }
}
