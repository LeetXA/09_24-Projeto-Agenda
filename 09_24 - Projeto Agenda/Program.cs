using System.Linq;

namespace _09_24___Projeto_Agenda
{
    internal class Program
    {
        static Contatos agenda = new Contatos();

        static void Main(string[] args)
        {
            int opcao;

            do
            {
                Console.WriteLine("\n===== AGENDA DE CONTATOS =====");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("1 - Adicionar contato");
                Console.WriteLine("2 - Pesquisar contato");
                Console.WriteLine("3 - Alterar contato");
                Console.WriteLine("4 - Remover contato");
                Console.WriteLine("5 - Listar contatos");
                Console.Write("Escolha uma opção: ");

                opcao = LerInteiro();

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Agenda encerrada.");
                        break;
                    case 1:
                        AdicionarContato();
                        break;
                    case 2:
                        PesquisarContato();
                        break;
                    case 3:
                        AlterarContato();
                        break;
                    case 4:
                        RemoverContato();
                        break;
                    case 5:
                        ListarContatos();
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

            } while (opcao != 0);
        }

        static int LerInteiro()
        {
            int valor;

            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Digite um número válido: ");
            }

            return valor;
        }

        static string LerTexto(string mensagem)
        {
            Console.Write(mensagem);
            return Console.ReadLine() ?? "";
        }

        static Data LerData()
        {
            while (true)
            {
                try
                {
                    int dia = LerInteiroComMensagem("Dia: ");
                    int mes = LerInteiroComMensagem("Mês: ");
                    int ano = LerInteiroComMensagem("Ano: ");

                    return new Data(dia, mes, ano);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                    Console.WriteLine("Digite a data novamente.");
                }
            }
        }

        static int LerInteiroComMensagem(string mensagem)
        {
            Console.Write(mensagem);
            return LerInteiro();
        }

        static Contato LerDadosContato(string email)
        {
            string nome = LerTexto("Nome: ");

            Console.WriteLine("Data de nascimento:");
            Data nascimento = LerData();

            Contato contato = new Contato(email, nome, nascimento);

            string resposta;

            do
            {
                string tipo = LerTexto("Tipo do telefone: ");

                string numero;

                while (true)
                {
                    numero = LerTexto("Número do telefone (somente números): ");

                    if (!string.IsNullOrWhiteSpace(numero) &&
                        numero.All(char.IsDigit))
                    {
                        break;
                    }

                    Console.WriteLine(
                        "Número inválido! Digite apenas números, sem letras ou símbolos.");
                }

                string principal = LerTexto(
                    "É o telefone principal? (S/N): ");

                bool ehPrincipal =
                    principal.Trim().Equals(
                        "S", StringComparison.OrdinalIgnoreCase);

                contato.AdicionarTelefone(
                    new Telefone(tipo, numero, ehPrincipal));

                resposta = LerTexto("Adicionar outro telefone? (S/N): ");

            } while (resposta.Trim().Equals(
                "S", StringComparison.OrdinalIgnoreCase));

            return contato;
        }

        static void AdicionarContato()
        {
            string email = LerTexto("E-mail: ");

            try
            {
                Contato contato = LerDadosContato(email);

                if (agenda.Adicionar(contato))
                    Console.WriteLine("Contato adicionado com sucesso!");
                else
                    Console.WriteLine("Já existe um contato com esse e-mail.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        static void PesquisarContato()
        {
            string email = LerTexto("E-mail do contato: ");

            try
            {
                Contato busca = new Contato(
                    email, "Pesquisa", new Data(1, 1, 2000));

                Contato encontrado = agenda.Pesquisar(busca);

                if (encontrado != null)
                    Console.WriteLine("\n" + encontrado);
                else
                    Console.WriteLine("Contato não encontrado.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        static void AlterarContato()
        {
            string email = LerTexto(
                "E-mail do contato que deseja alterar: ");

            try
            {
                Contato busca = new Contato(
                    email, "Pesquisa", new Data(1, 1, 2000));

                if (agenda.Pesquisar(busca) == null)
                {
                    Console.WriteLine("Contato não encontrado.");
                    return;
                }

                Console.WriteLine("Digite os novos dados do contato.");
                Contato atualizado = LerDadosContato(email);

                if (agenda.Alterar(atualizado))
                    Console.WriteLine("Contato alterado com sucesso!");
                else
                    Console.WriteLine("Não foi possível alterar o contato.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        static void RemoverContato()
        {
            string email = LerTexto("E-mail do contato que deseja remover: ");

            try
            {
                Contato busca = new Contato(
                    email, "Pesquisa", new Data(1, 1, 2000));

                if (agenda.Remover(busca))
                    Console.WriteLine("Contato removido com sucesso!");
                else
                    Console.WriteLine("Contato não encontrado.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        static void ListarContatos()
        {
            var contatos = agenda.Listar();

            if (contatos.Count == 0)
            {
                Console.WriteLine("A agenda está vazia.");
                return;
            }

            Console.WriteLine("\n===== CONTATOS CADASTRADOS =====");

            foreach (Contato contato in contatos)
            {
                Console.WriteLine(contato);
                Console.WriteLine("-------------------------------");
            }

            Console.WriteLine($"Total de contatos: {contatos.Count}");
        }
    }
}