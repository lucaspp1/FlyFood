using System;
using System.Collections.Generic;
using System.Text;

namespace FlyFood.Model
{
    public class Voo
    {
        public DateTime Decolagem { get; set; }
        public float TempoVoo { get; set; }
        public string Companhia { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }

        public int Id { get; set; }

        public string detalhesVoo()
        {
            return $" {this.Id} - {this.Origem} até {this.Destino} ({this.Companhia}) \n \t {this.TempoVoo} horas de viagem ";
        }

        public string detalhesVoo(bool adm)
        {
            return adm ? $"{this.Origem} até {this.Destino} ({this.Companhia}) \n \t {this.TempoVoo} horas de viagem no dia {this.Decolagem.ToString("dd/MM/yyyy")} " : detalhesVoo();
        }

        public bool validoParaDecolar()
        {
            DateTime now = DateTime.Now;
            return Decolagem.Day == now.Day &&
                    Decolagem.Month == now.Month &&
                    Decolagem.Year == now.Year;


                ;
        }

    }
}
