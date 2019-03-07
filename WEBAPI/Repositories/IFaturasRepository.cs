using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Models;

namespace WEBAPI.Repositories
{
    interface IFaturasRepository : IDisposable
    {
        IEnumerable<Fatura> GetPendingFaturas();
        IEnumerable<Fatura> GetPendingFaturas(int page);
        IEnumerable<Fatura> GetFaturas(string user);
        IEnumerable<Fatura> GetFaturas(string user, int page);
    }
}
