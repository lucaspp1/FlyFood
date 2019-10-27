using System;
using System.Collections.Generic;
using System.Text;

namespace FlyFood.Model
{
    public class Voo
    {
        public DateTime Decolagem { get; set; }
        public float TempoVoo { get; set; }
        public string Aviao{ get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }

        public int Id { get; set; }

    }
}
