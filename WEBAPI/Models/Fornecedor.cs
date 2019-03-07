using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WEBAPI.Models
{
    public partial class Fornecedor
    {
        public Fornecedor()
        {
            Fatura = new HashSet<Fatura>();
        }

        public short FornecedorId { get; set; }
        public string DescritivoFornecedor { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public Fornecedor FornecedorNavigation { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public Fornecedor InverseFornecedorNavigation { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Fatura> Fatura { get; set; }
    }
}
