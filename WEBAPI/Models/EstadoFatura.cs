using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WEBAPI.Models
{
    public partial class EstadoFatura
    {
        public EstadoFatura()
        {
            Fatura = new HashSet<Fatura>();
        }

        public short EstadoFaturaId { get; set; }
        public string DescritivoEstadoFatura { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Fatura> Fatura { get; set; }
    }
}
