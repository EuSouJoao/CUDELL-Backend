using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Db;
using WEBAPI.Dto;
using WEBAPI.Models;
using WEBAPI.Repositories;

namespace WEBAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Faturas")]
    public class FaturasController : Controller
    {
        private IFaturasRepository faturasRepository;

        private readonly DemoContext _context;

        public FaturasController(DemoContext context)
        {
            _context = context;
            this.faturasRepository = new FaturasRepository(context);
        }

        // GET: api/Faturas
        [HttpGet]
        [Authorize]
        public IEnumerable<FaturaDTO> GetFatura()
        {
            List<FaturaDTO> ListFaturas = new List<FaturaDTO>();
            foreach (Fatura f in faturasRepository.GetFaturas(User.Identity.Name.Substring(User.Identity.Name.IndexOf(@"\") + 1)))
                {
                    ListFaturas.Add(new FaturaDTO(f));
                }

            return ListFaturas;
        }

        //GET: api/Faturas/(page)
        [HttpGet("{page}")]
        [Authorize]
        public IEnumerable<FaturaDTO> GetFatura([FromRoute] int page)
        {
            List<FaturaDTO> ListFaturas = new List<FaturaDTO>();
            foreach (Fatura f in faturasRepository.GetFaturas(User.Identity.Name.Substring(User.Identity.Name.IndexOf(@"\") + 1) , page))
            {
                ListFaturas.Add(new FaturaDTO(f));
            }

            return ListFaturas;
        }

        // GET: api/Faturas/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetFatura([FromRoute] long id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var fatura = await _context.Fatura.SingleOrDefaultAsync(m => m.FaturaId == id);

        //    if (fatura == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(fatura);
        //}

        // PUT: api/Faturas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFatura([FromRoute] long id, [FromBody] Fatura fatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fatura.FaturaId)
            {
                return BadRequest();
            }

            _context.Entry(fatura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FaturaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Faturas
        [HttpPost]
        public async Task<IActionResult> PostFatura([FromBody] Fatura fatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Fatura.Add(fatura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFatura", new { id = fatura.FaturaId }, fatura);
        }

        // DELETE: api/Faturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFatura([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fatura = await _context.Fatura.SingleOrDefaultAsync(m => m.FaturaId == id);
            if (fatura == null)
            {
                return NotFound();
            }

            _context.Fatura.Remove(fatura);
            await _context.SaveChangesAsync();

            return Ok(fatura);
        }

        private bool FaturaExists(long id)
        {
            return _context.Fatura.Any(e => e.FaturaId == id);
        }
    }
}