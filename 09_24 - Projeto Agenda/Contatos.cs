using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_24___Projeto_Agenda
{
    public class Contatos
    {
        private readonly List<Contato> agenda =
            new List<Contato>();

        public bool Adicionar(Contato c)
        {
            if (c == null || Pesquisar(c) != null)
                return false;

            agenda.Add(c);
            return true;
        }

        public Contato Pesquisar(Contato c)
        {
            if (c == null)
                return null;

            return agenda.FirstOrDefault(
                contato => contato.Equals(c));
        }

        public bool Alterar(Contato c)
        {
            if (c == null)
                return false;

            Contato existente = Pesquisar(c);

            if (existente == null)
                return false;

            int indice = agenda.IndexOf(existente);
            agenda[indice] = c;

            return true;
        }

        public bool Remover(Contato c)
        {
            Contato existente = Pesquisar(c);

            if (existente == null)
                return false;

            agenda.Remove(existente);
            return true;
        }

        public IReadOnlyList<Contato> Listar()
        {
            return agenda.AsReadOnly();
        }
    }
}
