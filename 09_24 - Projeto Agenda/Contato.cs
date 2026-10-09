using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_24___Projeto_Agenda
{
    public class Contato
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public Data DtNasc { get; set; }

        private List<Telefone> telefones = new List<Telefone>();

        public Contato(string email, string nome, Data dtNasc)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Informe o e-mail.");

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Informe o nome.");

            Email = email.Trim();
            Nome = nome.Trim();
            DtNasc = dtNasc ??
                throw new ArgumentNullException(nameof(dtNasc));
        }

        public int GetIdade()
        {
            DateTime hoje = DateTime.Today;

            int idade = hoje.Year - DtNasc.Ano;

            if (hoje.Month < DtNasc.Mes ||
                (hoje.Month == DtNasc.Mes &&
                 hoje.Day < DtNasc.Dia))
            {
                idade--;
            }

            return idade;
        }

        public void AdicionarTelefone(Telefone t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));

            // Se o novo telefone for principal,
            // os anteriores deixam de ser principais.
            if (t.Principal)
            {
                foreach (Telefone telefone in telefones)
                {
                    telefone.Principal = false;
                }
            }

            telefones.Add(t);
        }

        public string GetTelefonePrincipal()
        {
            Telefone principal = telefones.FirstOrDefault(
                t => t.Principal);

            return principal != null
                ? principal.Numero
                : "Nenhum telefone principal cadastrado";
        }

        public override string ToString()
        {
            StringBuilder texto = new StringBuilder();

            texto.AppendLine($"Nome: {Nome}");
            texto.AppendLine($"E-mail: {Email}");
            texto.AppendLine($"Nascimento: {DtNasc}");
            texto.AppendLine($"Idade: {GetIdade()} anos");
            texto.AppendLine(
                $"Telefone principal: {GetTelefonePrincipal()}");
            texto.AppendLine("Telefones:");

            if (telefones.Count == 0)
            {
                texto.AppendLine("Nenhum telefone cadastrado.");
            }
            else
            {
                foreach (Telefone telefone in telefones)
                {
                    texto.AppendLine($"- {telefone}");
                }
            }

            return texto.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is not Contato outro)
                return false;

            return string.Equals(
                Email,
                outro.Email,
                StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return StringComparer.OrdinalIgnoreCase.GetHashCode(Email);
        }
    }
}
