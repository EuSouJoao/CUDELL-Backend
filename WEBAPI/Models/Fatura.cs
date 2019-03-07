using System;
using System.Collections.Generic;

namespace WEBAPI.Models
{
    public partial class Fatura
    {
        public long FaturaId { get; set; }
        public DateTime DataFatura { get; set; }
        public DateTime? DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public short? FornecedorId { get; set; }
        public string InsertUser { get; set; }
        public DateTime InsertDate { get; set; }
        public string AlterUser { get; set; }
        public DateTime AlterDate { get; set; }
        public string ResponsavelFatura { get; set; }
        public short EstadoFaturaId { get; set; }

        public EstadoFatura EstadoFatura { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}
