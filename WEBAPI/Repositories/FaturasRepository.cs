using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Db;
using WEBAPI.Models;

namespace WEBAPI.Repositories
{
    public class FaturasRepository : IFaturasRepository
    {
        private static short ID_PENDENTE = 1;

        private readonly DemoContext _context;

        public FaturasRepository(DemoContext context)
        {
            this._context = context;
        }
        public IEnumerable<Fatura> GetFaturas(string user)
        {
            return _context.Fatura
                .Include(f => f.EstadoFatura)
                .Include(f => f.Fornecedor)
                .Where(f => f.ResponsavelFatura == user)
                .ToList();
        }

        public IEnumerable<Fatura> GetPendingFaturas()
        {
            return _context.Fatura
                   .Include(f => f.EstadoFatura)
                   .Include(f => f.Fornecedor)
                   .Where(f => f.EstadoFaturaId == ID_PENDENTE);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Fatura> GetFaturas(string user, int page)
        {
            return _context.Fatura
                .Include(f => f.EstadoFatura)
                .Include(f => f.Fornecedor)
                .Where(f => f.ResponsavelFatura == user)
                .Skip((page-1) * 10)
                .Take(10)
                .ToList();
        }

        public IEnumerable<Fatura> GetPendingFaturas(int page)
        {
            return _context.Fatura
                   .Include(f => f.EstadoFatura)
                   .Include(f => f.Fornecedor)
                   .Where(f => f.EstadoFaturaId == ID_PENDENTE)
                   .Skip((page - 1) * 10)
                   .Take(10)
                   .ToList();
        }
    }
}
