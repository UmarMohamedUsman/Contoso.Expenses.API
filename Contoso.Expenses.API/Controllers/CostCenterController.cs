﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contoso.Expenses.API.Database;
using Contoso.Expenses.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Contoso.Expenses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostCenterController : ControllerBase
    {
        private readonly IOptions<ConfigValues> _config;
        private readonly DatabaseContext _dbctx;

        //public CostCenterController()
        //{
        //}

        public CostCenterController(IOptions<ConfigValues> config)
        {
            _dbctx = new DatabaseContext(config);
            _config = config;
        }

        // GET: api/CostCenter
        [HttpGet]
        public IEnumerable<CostCenter> Get()
        {
            return _dbctx.costCenters.ToList();
        }

        // GET: api/CostCenter/umar@hotmail.com
        [HttpGet("{email}", Name = "Get")]
        public CostCenter Get(string email)
        {
            var costCenter = _dbctx.costCenters.FirstOrDefault(x => x.SubmitterEmail == email);

            if (costCenter != null)
            {
                Console.WriteLine("Cost Center with email {0} found." + email);
                return costCenter;
            }
            else
            {
                Console.WriteLine("Cost Center with email {0} not found." + email);
                return null;
            }
        }

        // POST: api/CostCenter
        [HttpPost]
        public IActionResult Post([FromBody] CostCenter model)
        {
            try
            {
                _dbctx.costCenters.Add(model);
                _dbctx.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // DELETE: api/CostCenter/umar@hotmail.com
        [HttpDelete("{email}")]
        public IActionResult Delete(string email)
        {
            var costCenter = Get(email);

            if (costCenter != null)
            {
                _dbctx.costCenters.Remove(Get(email));
                _dbctx.SaveChanges();
                Console.WriteLine("Cost Center with email {0} deleted successfully." + email);
                return StatusCode(StatusCodes.Status200OK, costCenter);
            }
            else
            {
                Console.WriteLine("Cost Center with email {0} not found. No action taken." + email);
                return StatusCode(StatusCodes.Status500InternalServerError, string.Format("Cost Center with email {0} not found. No action taken.", email));
            }
            
        }
    }
}
