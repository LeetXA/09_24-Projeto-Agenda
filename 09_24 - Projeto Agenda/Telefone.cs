using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_24___Projeto_Agenda
{
    public class Telefone
    {
        public string Tipo { get; set; }
        public string Numero { get; set; }
        public bool Principal { get; set; }

        public Telefone(string tipo, string numero, bool principal)
        {
            if (string.IsNullOrWhiteSpace(tipo))
                throw new ArgumentException("Informe o tipo.");

            if (string.IsNullOrWhiteSpace(numero))
                throw new ArgumentException("Informe o número.");

            Tipo = tipo;
            Numero = numero;
            Principal = principal;
        }

        public override string ToString()
        {
            string identificacao = Principal ? " (Principal)" : "";
            return $"{Tipo}: {Numero}{identificacao}";
        }
    }
}
