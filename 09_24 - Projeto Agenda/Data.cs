using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_24___Projeto_Agenda
{
    public class Data
    {
        private int dia;
        private int mes;
        private int ano;

        public int Dia
        {
            get { return dia; }
        }

        public int Mes
        {
            get { return mes; }
        }

        public int Ano
        {
            get { return ano; }
        }

        public Data(int dia, int mes, int ano)
        {
            SetData(dia, mes, ano);
        }

        public void SetData(int dia, int mes, int ano)
        {
            if (!DateTime.TryParse(
                $"{ano:D4}-{mes:D2}-{dia:D2}",
                out DateTime data) ||
                data.Day != dia ||
                data.Month != mes ||
                data.Year != ano)
            {
                throw new ArgumentException("Data inválida.");
            }

            if (data.Date > DateTime.Today)
                throw new ArgumentException(
                    "A data não pode ser futura.");

            this.dia = dia;
            this.mes = mes;
            this.ano = ano;
        }

        public override string ToString()
        {
            return $"{dia:D2}/{mes:D2}/{ano:D4}";
        }
    }
}
