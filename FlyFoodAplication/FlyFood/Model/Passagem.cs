using System;
using System.Collections.Generic;
using System.Text;

namespace FlyFood.Model
{
    public class Passagem
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public Passagem MyProperty { get; set; }
        public string Poltrona { get; set; }
    }
}
