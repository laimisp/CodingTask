using CodingTask.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MunicipalitiesController : ControllerBase
    {
        private readonly DefaultDbContext db;

        private readonly ILogger<MunicipalitiesController> _logger;

        public MunicipalitiesController(ILogger<MunicipalitiesController> logger, DefaultDbContext context)
        {
            _logger = logger;
            db = context;
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(new { Success = true, Data = db.Municipality.FirstOrDefault(x => x.Id == id) });
            }
            catch (Exception e)
            {
                return Ok(new { Success = false, Error = e.Message });
            }
        }

        [HttpGet("GetListActive")]
        public IActionResult GetListActive()
        {
            try
            {
                return Ok(new { Success = true, Data = db.Municipality.Where(x => x.Deleted == null).ToList() });
            }
            catch (Exception e)
            {
                return Ok(new { Success = false, Error = e.Message });
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(Municipality municipality)
        {
            try
            {
                db.Municipality.Update(municipality);
                db.SaveChanges();
                return Ok(new { Success = true });
            }
            catch (Exception e)
            {
                return Ok(new { Success = false, Error = e.Message });
            }
        }

        [HttpPost("Create")]
        public IActionResult Create(Municipality municipality)
        {
            try
            {
                db.Municipality.Add(municipality);
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
                db.Municipality.FirstOrDefault(x => x.Id == id).Deleted = DateTime.Now;
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
