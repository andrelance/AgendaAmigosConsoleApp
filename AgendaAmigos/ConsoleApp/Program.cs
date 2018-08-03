using Controller;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Program
    {
        // Obs:  Não foi possível implementar o primeiro requisito do segundo parágrafo
        //ou seja, assim que o programa iniciar, mostrar os aniversariantes do dia,
        //visto que é necessário carregar a agenda primeiro. Além disso, o Menu Inicial perderia sua função de looping,
        //no caso de haver muitos aniversariantes no dia.
        //Dessa forma, foi oferecida a opção de mostrar os aniversariantes do dia numa página dedicada.
        static void Main(string[] args)
        {
            Agenda agenda = new Agenda();
            Arquivo repositorio = new Arquivo();
            int opcao = 0;            
            const string diretorio = @"D:\agenda.txt";

            // Obtem o arquivo para verificar se há aniversariantes no dia.
            agenda = repositorio.ObterAgendaDeArquivo(diretorio);
            // Exibe aniversariantes do dia.
            AniversariantesDoDia(agenda);


            do // Enquanto se desejar o menu vai ficar rodando
            {                
                Console.WriteLine("\n");
                Console.WriteLine("\t\t"+DateTime.Now);
                Console.WriteLine("\n\n");

                Console.WriteLine("\n\t\tGeranciador de Aniversários.\n");
                Console.WriteLine("\t\tSelecione uma das opções abaixo:\n");
                Console.WriteLine("\t\t1 - Adicionar nova pessoa.\n" +
                                  "\t\t2 - Buscar pessoa pelo nome ou sobrenome.\n" +
                                  "\t\t3 - Editar dados da Pessoa.\n" +
                                  "\t\t4 - Apagar pessoa da agenda.\n" +
                                  "\t\t5 - Imprimir agenda.\n" +
                                  "\t\t6 - Carregar agenda de arquivo.\n" +
                                  "\t\t7 - Gravar agenda em arquivo.\n" +
                                  "\t\t8 - Abrir arquivo.\n" +
                                  "\t\t9 - Aniversariantes do Dia.\n" +
                                  "\t\t0 - Sair.\n");

                Console.WriteLine("\n\n\n\t\tQuantidade de pessoas na agenda: "+ agenda.ObterQuantidadePessoas());

                opcao = int.Parse(Console.ReadLine()); // Captura a opção escolhida pelo usuário

                // Opções do menu e seus comandos a serem executados, de a cordo com a opção escolhida pelo usuário
                switch (opcao)
                {
                    case 1: // Adiciona nova pessoa na agenda
                        Console.Clear();
                        AdicionarPessoa(agenda);
                        break;
                    case 2: // Busca pessoa pelo nome ou sobrenome
                        Console.Clear();
                        BuscarPessoasPeloNome(agenda);
                        break;
                    case 3: // Edita os dados de uma determinada pessoa
                        Console.Clear();
                        EditarPessoa(agenda);
                        break;
                    case 4: // Apaga pessoa da agenda
                        Console.Clear();
                        DeletarPessoa(agenda);
                        break;
                    case 5: // Imprime toda a  agenda
                        Console.Clear();
                        ImprimirAgenda(agenda);
                        break;
                    case 6: // Carrega agenda de arquivo já salvo
                        Console.Clear();
                        agenda = repositorio.ObterAgendaDeArquivo(diretorio);
                        Console.WriteLine("Agenda carregada com sucesso!");
                        break;
                    case 7: // Grava agenda em arquivo
                        Console.Clear();
                        repositorio.GravarAgendaEmArquivo(agenda, diretorio);
                        Console.WriteLine("Agenda gravada com sucesso!");
                        break;
                    case 8: // Abre arquivo da agenda apenas para leitura (se editar o arquivo ele será inutilizado!!!)
                        Console.Clear();
                        System.Diagnostics.Process.Start(diretorio);
                        Console.WriteLine("Arquivo texto aberto!");
                        Console.WriteLine("IMPORTANTE: É apenas para você olha ele, lembre de fechar antes de gravar novamente!");
                        break;
                    case 9: // Exibe os aniversariantes do dia
                        Console.Clear();
                        AniversariantesDoDia(agenda);
                        break;
                }
            } while (opcao != 0); // Digitando o número ZERO o programa é encerrado
        }

        // Adiciona nova pessoa na agenda
        static void AdicionarPessoa(Agenda agenda) 
        {

            Console.WriteLine("Digite o nome da pessoa que deseja adicionar (apénas o primeiro nome): ");
            string nome = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Digite o sobrenome da pessoa: "); 
            string sobrenome = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Digite a data de nascimento no formato dd/mm/aaaa: ");
            DateTime DataNascimento = DateTime.ParseExact(Console.ReadLine(),
                "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Console.WriteLine();
            Console.WriteLine("\nOs dados abaixo estão corretos? \n");
            Console.WriteLine("Nome completo:  " + nome + " " + sobrenome);
            Console.WriteLine("Data de nascimento:  " + DataNascimento.ToString("dd/MM/yyyy"));
            //Confirma ou não os dados pesquisados
            Console.WriteLine("\n1 - Sim.\n2 - Não.\n");
            int insersao = int.Parse(Console.ReadLine()); 

            if (insersao == 1) // Caso os dados confiram com a pessoa a ser inserida na agenda
            {                
                //Preparar a pessoa para inserir na agenda
                Pessoa pessoa = new Pessoa();

                pessoa.IdPessoa = Guid.NewGuid();
                pessoa.Nome = nome;
                pessoa.Sobrenome = sobrenome;
                pessoa.DataNascimento = DataNascimento;

                //Inserir pessoa na agenda
                agenda.Adicionar(pessoa);

                Console.WriteLine("Dados adicionados com sucesso na agenda!");
            }
            else if (insersao == 2)  // Os dados não confiram com a pessoa a ser inserida
            {
                Console.WriteLine("Tente digitar novamente.");
            }
            Console.Clear();
        }

        // Edita os dados de uma determinada pessoa previamente identificada (2 momentos)
        public static void EditarPessoa(Agenda agenda)
        {
            Console.WriteLine("\nDigite o nome da pessoa que deseja editar: ");
            string id = Console.ReadLine();
            Console.WriteLine("\n\n");

            // Utiliza função da agenda para pesquisar pessoa
            Pessoa pessoa = agenda.BuscarPessoaPeloNome(id);
            
            if (pessoa == null)
            {
                Console.WriteLine("Pessoa não encontrada!");
                return;
            }
            else
            {                
                Console.WriteLine("\nOs dados abaixo estão corretos? \n");
                Console.WriteLine("Nome completo:  " + pessoa.Nome + " " + pessoa.Sobrenome);
                Console.WriteLine("Data de nascimento:  " + pessoa.DataNascimento.ToString("dd/MM/yyyy"));
                //Confirma ou não os dados pesquisados
                Console.WriteLine("\n1 - Sim.\n2 - Não.\n");                
                int insersao = int.Parse(Console.ReadLine()); 

                if (insersao == 1) // Caso os dados confiram com a pessoa a ser inserida na agenda
                {
                    // Primeiro momento:
                    // Chama a função da agenda para remover a pessoa previamente identificada                                      
                    agenda.Remover(pessoa.IdPessoa);

                    Console.WriteLine("Digite o nome da pessoa que deseja adicionar (apénas o primeiro nome): ");
                    string nome = Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine("Digite o sobrenome da pessoa: "); // Não consegui fazer split com sobrenome - só aceita 1 palavra
                    string sobrenome = Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine("Digite a data de nascimento no formato dd/mm/aaaa: ");
                    DateTime DataNascimento = DateTime.ParseExact(Console.ReadLine(),
                        "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    Console.WriteLine();
                    Console.WriteLine("\nOs dados abaixo estão corretos? \n");
                    Console.WriteLine("Nome completo:  " + nome + " " + sobrenome);
                    Console.WriteLine("Data de nascimento:  " + DataNascimento.ToString("dd/MM/yyyy"));
                    Console.WriteLine("\n1 - Sim.\n2 - Não.\n");
                    int confere = int.Parse(Console.ReadLine()); //Confirma ou não os dados pesquisados

                    if (confere == 1)
                    { 
                        // Segundo momento:
                        //Preparar a pessoa para inserir na agenda
                        Pessoa pessoaModificada = new Pessoa();                        

                        pessoaModificada.IdPessoa = Guid.NewGuid();
                        pessoaModificada.Nome = nome;
                        pessoaModificada.Sobrenome = sobrenome;
                        pessoaModificada.DataNascimento = DataNascimento;

                        //Inserir pessoa na agenda
                        agenda.Adicionar(pessoaModificada);

                        Console.WriteLine("Dados adicionados com sucesso na agenda!");
                    }
                    else if (confere == 2)  // Os dados não conferem
                    {
                        Console.WriteLine("Tente digitar novamente.");
                    }
                    Console.Clear();                    
                }
                else if (insersao == 2)  // Novamente os dados não conferem
                {
                    Console.WriteLine("Tente novamente.");
                }
                Console.Clear();
            }
         }

        // Apaga pessoa, previamente identificada, da agenda
        public static void DeletarPessoa(Agenda agenda)
        {
            Console.WriteLine("\nDigite o nome da pessoa que deseja excluir da agenda: ");
            string id = Console.ReadLine();
            Console.WriteLine("\n\n");

            Pessoa pessoa = agenda.BuscarPessoaPeloNome(id);

            if (pessoa == null)
            {
                Console.WriteLine("Pessoa não encontrada!");
                return;
            }
            else
            {
                Console.WriteLine("\nOs dados abaixo estão corretos? \n");
                Console.WriteLine("Nome completo:  " + pessoa.Nome + " " + pessoa.Sobrenome);
                Console.WriteLine("Data de nascimento:  " + pessoa.DataNascimento.ToString("dd/MM/yyyy"));
                //Confirma ou não os dados pesquisados
                Console.WriteLine("\n1 - Sim.\n2 - Não.\n");
                int insersao = int.Parse(Console.ReadLine()); 

                if (insersao == 1) // Caso os dados confiram com a pessoa a ser eliminada da agenda
                {                    
                    agenda.Remover(pessoa.IdPessoa);

                    Console.WriteLine("Pessoa removida com sucesso na agenda!");
                }
                else if (insersao == 2)  // Os dados não conferem com a pessoa em questão
                {
                    Console.WriteLine("Tente novamente.");
                }
                Console.Clear();
            }
        }

        // Busca pessoa pelo nome ou sobrenome
        public static void BuscarPessoasPeloNome(Agenda agenda)  
        {
            int idade;

            Console.WriteLine("\nDigite o nome, ou sobrenome, da pessoa que deseja pesquisar: "); 
            string nome = Console.ReadLine();
            Console.WriteLine("\n\n");

            List<Pessoa> pessoa = agenda.BuscarPessoasPeloNome(nome);

            int pessoasEncontradas = 0;
            for (int i=0;i<pessoa.Count;i++)
            {
                pessoasEncontradas++;
            }

            if (pessoa == null)
            {
                Console.WriteLine("Pessoa não encontrada!");
                return;
            }
            else
            {

                Console.WriteLine(pessoasEncontradas+" Pessoas encontradas: \n\n");

                for (int i = 0; i < pessoa.Count; i++)
                {                                       

                    // Imprime a idade e a quantidade de dias para o próximo aniversário 
                    DateTime dataNascimento = pessoa[i].DataNascimento;
                    DateTime hoje = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day);
                    DateTime proximoAniversario = new DateTime(DateTime.Now.Date.Year, dataNascimento.Month, dataNascimento.Day); //.Date elimina a hora
                    TimeSpan diasParaAniversario = proximoAniversario - hoje;

                    if (dataNascimento.Date.Day == DateTime.Now.Date.Day && dataNascimento.Date.Month == DateTime.Now.Date.Month) //Aniversariante do dia
                    {
                        Console.WriteLine();
                        idade = DateTime.Today.Year - dataNascimento.Year;
                        Console.WriteLine("\t\t***********************************************************\n");
                        Console.WriteLine("\t\t" + (i + 1) + " ª Pessoa:\n");
                        Console.WriteLine("\t\tAniversariante do dia!!!\n");
                        Console.WriteLine("\t\tNome:  " + pessoa[i].Nome + " " + pessoa[i].Sobrenome);
                        Console.WriteLine("\t\tData de nascimento:  " + pessoa[i].DataNascimento.ToShortDateString());
                        Console.WriteLine("\t\t" + pessoa[i].Nome + " " + pessoa[i].Sobrenome + " completou hoje " + idade + " anos.\n");
                        Console.WriteLine("\t\tId: " + pessoa[i].IdPessoa + "\n");
                        Console.WriteLine("\t\t************************************************************");
                        Console.WriteLine("\n\n");
                    }
                    else if (diasParaAniversario.Days < 0) //Caso a data já tenha ocorrido (correção de quantidade negativa)
                    {
                        Console.WriteLine();
                        Console.WriteLine((i + 1) + "ª Pessoa: ");
                        Console.WriteLine("Id: " + pessoa[i].IdPessoa);
                        Console.WriteLine("Nome:  \t  " + pessoa[i].Nome + " " + pessoa[i].Sobrenome);
                        Console.WriteLine("Data de Nascimento:  " + pessoa[i].DataNascimento.ToShortDateString());
                        DateTime proximo = new DateTime(DateTime.Now.Date.Year+1, dataNascimento.Month, dataNascimento.Day); // Adiciona 1 ano para o aniversario que já ocorreu
                        TimeSpan dias = proximo - hoje;
                        Console.WriteLine("Faltam " + dias.Days + " dias para o próximo aniversário. Caso 1");
                        idade = DateTime.Today.Year - dataNascimento.Year;
                        Console.WriteLine(pessoa[i].Nome + " " + pessoa[i].Sobrenome + " tem " + idade + " anos.");
                    }
                    else //Todos os demais casos
                    {
                        Console.WriteLine();
                        Console.WriteLine((i + 1) + "ª Pessoa: ");
                        Console.WriteLine("Id: " + pessoa[i].IdPessoa);
                        Console.WriteLine("Nome:  \t  " + pessoa[i].Nome + " " + pessoa[i].Sobrenome);
                        Console.WriteLine("Data de Nascimento:  " + pessoa[i].DataNascimento.ToShortDateString());
                        Console.WriteLine("Faltam " + diasParaAniversario.Days + " dias para o próximo aniversário. Caso 2");
                        idade = DateTime.Today.Year - dataNascimento.Year - 1; // Ainda não fez aniversário, não completou a idade no presente ano
                        Console.WriteLine(pessoa[i].Nome + " " + pessoa[i].Sobrenome + " tem " + idade + " anos.");
                    }
                }              

                Console.ReadLine();
            }
            Console.Clear();
        }

        // Imprime toda a  agenda
        public static void ImprimirAgenda(Agenda agenda) 
        {
            List<Pessoa> pessoa = agenda.ObterTodasPessoas();
            int idade;
            Console.WriteLine("Lista de pessoas na agenda:\n");

            for (int i = 0; i < pessoa.Count; i++)
            {
                // Imprime a idade e a quantidade de dias para o próximo aniversário 
                DateTime dataNascimento = pessoa[i].DataNascimento;
                DateTime hoje = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day);
                DateTime proximoAniversario = new DateTime(DateTime.Now.Date.Year, dataNascimento.Month, dataNascimento.Day); //.Date elimina a hora
                TimeSpan diasParaAniversario = proximoAniversario - hoje;

                if (dataNascimento.Date.Day == DateTime.Now.Date.Day && dataNascimento.Date.Month == DateTime.Now.Date.Month) //Aniversariante do dia
                {
                    Console.WriteLine();
                    idade = DateTime.Today.Year - dataNascimento.Year;
                    Console.WriteLine("\t\t***********************************************************\n");
                    Console.WriteLine("\t\t" + (i + 1) + " ª Pessoa:\n");
                    Console.WriteLine("\t\tAniversariante do dia!!!\n");
                    Console.WriteLine("\t\tNome:  " + pessoa[i].Nome + " " + pessoa[i].Sobrenome);
                    Console.WriteLine("\t\tData de nascimento:  " + pessoa[i].DataNascimento.ToString("dd/MM/yyyy"));
                    Console.WriteLine("\t\t" + pessoa[i].Nome + " " + pessoa[i].Sobrenome + " completou hoje " + idade + " anos.\n");
                    Console.WriteLine("\t\tId: " + pessoa[i].IdPessoa+"\n");                    
                    Console.WriteLine("\t\t************************************************************");
                    Console.WriteLine("\n\n");
                }
                else if (diasParaAniversario.Days < 0) //Caso a data já tenha ocorrido (correção de quantidade negativa)
                {
                    Console.WriteLine();
                    Console.WriteLine((i + 1) + "ª Pessoa: ");                    
                    Console.WriteLine("Nome:  \t  " + pessoa[i].Nome + " " + pessoa[i].Sobrenome);
                    Console.WriteLine("Data de nascimento:  " + pessoa[i].DataNascimento.ToString("dd/MM/yyyy"));
                    // Adiciona 1 ano para o aniversario que já ocorreu este ano
                    DateTime proximo = new DateTime(DateTime.Now.Date.Year + 1, dataNascimento.Month, dataNascimento.Day); 
                    TimeSpan dias = proximo - hoje;
                    Console.WriteLine("Faltam " + dias.Days + " dias para o próximo aniversário. Caso 1");
                    idade = DateTime.Today.Year - dataNascimento.Year;
                    Console.WriteLine(pessoa[i].Nome + " " + pessoa[i].Sobrenome + " tem " + idade + " anos.");
                    Console.WriteLine("Id: " + pessoa[i].IdPessoa);                    
                }
                else //Todos os demais casos
                {
                    Console.WriteLine();
                    Console.WriteLine((i + 1) + "ª Pessoa: ");                    
                    Console.WriteLine("Nome:  \t  " + pessoa[i].Nome + " " + pessoa[i].Sobrenome);
                    Console.WriteLine("Data de nascimento:  " + pessoa[i].DataNascimento.ToString("dd/MM/yyyy"));
                    Console.WriteLine("Faltam " + diasParaAniversario.Days + " dias para o próximo aniversário. Caso 2");
                    idade = DateTime.Today.Year - dataNascimento.Year - 1; // Ainda não fez aniversário, não completou a idade no presente ano
                    Console.WriteLine(pessoa[i].Nome + " " + pessoa[i].Sobrenome + " tem " + idade + " anos.");
                    Console.WriteLine("Id: " + pessoa[i].IdPessoa);
                }
            }
            Console.ReadLine();
            Console.Clear();
        }

        // Exibe os aniversariantes do dia
        private static void AniversariantesDoDia(Agenda agenda)
        {
            Console.WriteLine("\n");
            Console.WriteLine("\t\t" + DateTime.Now);
            Console.WriteLine("\n");

            int idade;
            List<Pessoa> pessoa = agenda.BuscarPessoasPorDataNascimento(DateTime.Now.Date);           

            if(pessoa.Count == 0)
            {
                Console.WriteLine("\n\n\n\t\tNenhuma pessoa fazendo aniversário hoje.\n\n");
            }
            else
            {
                Console.WriteLine("\t\tAniversariantes do dia: \n");

                for (int i = 0; i < pessoa.Count; i++)
                {
                    DateTime dataNascimento = pessoa[i].DataNascimento;
                    DateTime hoje = new DateTime(DateTime.Now.Date.DayOfYear); //.Date elimina a hora de DateTime                
                    DateTime proximoAniversario = new DateTime(DateTime.Now.Date.Year, dataNascimento.Month, dataNascimento.Day);
                    TimeSpan diasParaAniversario = proximoAniversario - hoje;

                    if (dataNascimento.Date.Day == DateTime.Now.Date.Day && dataNascimento.Date.Month == DateTime.Now.Date.Month) //Aniversariante do dia
                    {
                        Console.WriteLine();
                        idade = DateTime.Today.Year - dataNascimento.Year;
                        Console.WriteLine("\t\t***********************************************************\n");
                        Console.WriteLine("\t\t" + (i + 1) + " ª Pessoa:\n");                        
                        Console.WriteLine("\t\tNome:  " + pessoa[i].Nome + " " + pessoa[i].Sobrenome + "\n");
                        Console.WriteLine("\t\tNasceu em:  " + pessoa[i].DataNascimento.ToString("dd/MM/yyyy"));
                        Console.WriteLine("\t\tId: " + pessoa[i].IdPessoa);
                        Console.WriteLine("\t\t" + pessoa[i].Nome + " " + pessoa[i].Sobrenome + " completou hoje " + idade + " anos.\n");
                        Console.WriteLine("\t\t************************************************************");
                    }
                }
            }                            
            Console.WriteLine("\n\n");
            
            Console.WriteLine("\t\tPressione ENTER para continuar...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
