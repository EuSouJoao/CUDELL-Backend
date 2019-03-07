using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Db;
using WEBAPI.Dto;
using WEBAPI.Models;
using WEBAPI.Repositories;

namespace WEBAPI.Controllers
{

    [Produces("application/json")]
    [Route("api/FaturasPendentes")]
    public class FaturasPendentesController : Controller
    {

        private IFaturasRepository faturasRepository;

        private readonly DemoContext _context;

        //public FaturasPendentesController()
        //{
        //    this.faturasRepository = new FaturasRepository(new DemoContext());
        //}

        public FaturasPendentesController(DemoContext context)
        {
            _context = context;
            this.faturasRepository = new FaturasRepository(context);
        }

        //public FaturasPendentesController(IFaturasRepository faturasRepository)
        //{
        //    this.faturasRepository = faturasRepository;
        //}

        // GET: api/FaturasPendentes
        [HttpGet]
        [Authorize]
        public IEnumerable<FaturaDTO> Get()
        {
            List<FaturaDTO> ListFaturasPendentes = new List<FaturaDTO>();
            foreach (Fatura f in faturasRepository.GetPendingFaturas()) {
                ListFaturasPendentes.Add(new FaturaDTO(f));
            }
            return ListFaturasPendentes;
        }

        [HttpGet("{page}")]
        [Authorize]
        public IEnumerable<FaturaDTO> Get([FromRoute] int page)
        {
            List<FaturaDTO> ListFaturasPendentes = new List<FaturaDTO>();
            foreach (Fatura f in faturasRepository.GetPendingFaturas(page))
            {
                ListFaturasPendentes.Add(new FaturaDTO(f));
            }

            return ListFaturasPendentes;
        }


        // GET: api/FaturasPendentes/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/FaturasPendentes
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/FaturasPendentes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
