using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// Classe LIB Padronizada para gerenciamento dos dados da agenda
    /// </summary>
    public class Arquivo
    {
        // Função que faz salvar a agenda em um arquivo
        public void GravarAgendaEmArquivo(Agenda agenda, string diretorio)
        {
            var arquivo = new System.IO.StreamWriter(diretorio);

            List<Pessoa> pessoasAgenda = agenda.ObterTodasPessoas();

            for (int i = 0; i < pessoasAgenda.Count; i++)
            {
                arquivo.WriteLine(pessoasAgenda[i].IdPessoa);
                arquivo.WriteLine(pessoasAgenda[i].Nome);
                arquivo.WriteLine(pessoasAgenda[i].Sobrenome);
                arquivo.WriteLine(pessoasAgenda[i].DataNascimento);
            }
            arquivo.Close();
        }

        // Função que carrega a agenda a partir de um arquivo já salvo
        public Agenda ObterAgendaDeArquivo(string diretorio)
        {
            Agenda agenda = new Agenda();

            var arquivo = new System.IO.StreamReader(diretorio);

            while (!arquivo.EndOfStream)
            {
                Pessoa pessoa = new Pessoa();

                pessoa.IdPessoa = Guid.Parse(arquivo.ReadLine());
                pessoa.Nome = arquivo.ReadLine();
                pessoa.Sobrenome = arquivo.ReadLine();
                pessoa.DataNascimento = DateTime.Parse(arquivo.ReadLine());

                agenda.Adicionar(pessoa);
            }
            arquivo.Close();

            return agenda;
        }
    }
}
