using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Models;

namespace WEBAPI.Dto
{
    public class FaturaDTO
    {
        public FaturaDTO(Fatura f){
            FaturaId = f.FaturaId;
            DataFatura = f.DataFatura; //.ToShortDateString();
            if (f.DataVencimento.HasValue)
            {
                /*DateTime DataVencimento1*/ DataVencimento = (DateTime) f.DataVencimento;
                // DataVencimento = DataVencimento1.ToString();
            }
            Valor = f.Valor;
            Estado = f.EstadoFatura.DescritivoEstadoFatura;
            if (f.Fornecedor != null) { this.Fornecedor = f.Fornecedor.DescritivoFornecedor; }
        }

        public long FaturaId { get; set; }

        public DateTime DataFatura { get; set; }
        
        public DateTime DataVencimento { get; set; }

        public decimal Valor { get; set; }

        public string Estado { get; set; }

        public string Fornecedor { get; set; }
    }
}
