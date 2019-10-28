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
            string horas = TempoVoo == 1 ? "hora" : "horas";
            return $" {this.Id} - {this.Origem} até {this.Destino} ({this.Companhia}) \n \t {this.TempoVoo} {horas} de viagem aproximadamente";
        }

        public string detalhesVoo(bool adm)
        {
            string horas = TempoVoo == 1 ? "hora" : "horas";
            return adm ? $"{this.Origem} até {this.Destino} ({this.Companhia}) \n \t {this.TempoVoo} {horas} de viagem aproximadamente, no dia {this.Decolagem.ToString("dd/MM/yyyy")} " : detalhesVoo();
        }

        public bool validoParaDecolar()
        {
            DateTime now = DateTime.Now;
            return Decolagem.Day == now.Day &&
                    Decolagem.Month == now.Month &&
                    Decolagem.Year == now.Year
                ;
        }

    }
}
