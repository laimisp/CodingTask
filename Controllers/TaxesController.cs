using CodingTask.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace CodingTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxesController : ControllerBase
    {
        private readonly DefaultDbContext db;

        private readonly ILogger<TaxesController> _logger;

        public TaxesController(ILogger<TaxesController> logger, DefaultDbContext context)
        {
            _logger = logger;
            db = context;
        }

        [HttpGet("GetByDateAndMunicipality")]
        public IActionResult GetByDateAndMunicipality(int id, DateTime date)
        {
            try
            {
                var tax = db.Tax.FirstOrDefault(x => x.Municipality_Id == id && x.PeriodStart.Date == date.Date && x.PeriodEnd.Date == date.Date);
                if (tax == null)
                {
                    var temptaxList = db.Tax.Where(x => x.Municipality_Id == id && x.PeriodStart.Date <= date.Date && x.PeriodEnd.Date >= date.Date).ToList();
                    if (temptaxList.Any(x => (x.PeriodEnd.Date - x.PeriodStart.Date).Days < 32))
                    {
                        tax = temptaxList.FirstOrDefault(x => (x.PeriodEnd.Date - x.PeriodStart.Date).Days < 32);
                    }
                    else
                    {
                        tax = temptaxList.FirstOrDefault(x => (x.PeriodEnd.Date - x.PeriodStart.Date).Days > 32);
                    }
                }
                TaxVew taxVew = new TaxVew();
                if (tax != null)
                {
                    taxVew.Id = tax.Id;
                    taxVew.MunicipalityName = db.Municipality.FirstOrDefault(x => x.Id == tax.Municipality_Id).Name;
                    taxVew.Date = date.ToShortDateString();
                    taxVew.Result = tax.Result;
                }
                return Ok(new { Success = true, Data = taxVew });
            }
            catch (Exception e)
            {
                return Ok(new { Success = false, Error = e.Message });
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(Tax tax)
        {
            try
            {
                db.Tax.Update(tax);
                db.SaveChanges();
                return Ok(new { Success = true });
            }
            catch (Exception e)
            {
                return Ok(new { Success = false, Error = e.Message });
            }
        }

        [HttpPost("Create")]
        public IActionResult Create(Tax tax)
        {
            try
            {
                db.Tax.Add(tax);
                db.SaveChanges();
                return Ok(new { Success = true });
            }
            catch (Exception e)
            {
                return Ok(new { Success = false, Error = e.Message });
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                db.Tax.Remove(db.Tax.FirstOrDefault(x => x.Id == id));
                db.SaveChanges();
                return Ok(new { Success = true });
            }
            catch (Exception e)
            {
                return Ok(new { Success = false, Error = e.Message });
            }
        }
    }
}
