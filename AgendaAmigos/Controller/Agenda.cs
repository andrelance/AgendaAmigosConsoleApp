using Model;
using Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    /// <summary>
    /// Classe LIB Padronizada para armazenar pessoas na agenda
    /// </summary>
    public class Agenda : IInsercao, IAtualizacao, IRemocao, IBusca
    {
        private List<Pessoa> agenda;

        // Construtor para Agenda
        public Agenda()
        {
            agenda = new List<Pessoa>();
        }

        // Função que permite obter a quantidade de pessoas na agenda
        public int ObterQuantidadePessoas()
        {
            return agenda.Count;
        }

        // Função que adiciona uma pessoa na agenda
        public void Adicionar(Pessoa pessoa)
        {
            agenda.Add(pessoa);
        }

        // Função que atualiza os dados de uma pessoa na agenda.
        // Foi construída para num primeiro momento apagar a pessoa a ater os dados modificados e
        // num segundo momento inserir uma nova pessoa, que é a anterior com os dados já modificados.
        public void Atualizar(Pessoa pessoaModificada)
        {
            for (int i = 0; i < agenda.Count; i++)
            {
                if (agenda[i].IdPessoa == pessoaModificada.IdPessoa)
                {
                    agenda.RemoveAt(i);
                }
            }
            agenda.Add(pessoaModificada);
        }

        // Função que deleta(remove) a pessoa, previamente identificada, da agenda.
        public void Remover(Guid id)
        {
            for (int i = 0; i < agenda.Count; i++)
            {
                if (agenda[i].IdPessoa == id)
                {
                    agenda.RemoveAt(i);
                }
            }
        }

        // Função para facilitar outras funções em obter todas as pessoas da agenda
        public List<Pessoa> ObterTodasPessoas()
        {
            return agenda;
        }

        // Função que busca, pelo nome ou sobrenome, uma única pessoa na agenda
        public Pessoa BuscarPessoaPeloNome(string nome)
        {
            for (int i = 0; i < agenda.Count; i++)
            {
                if (agenda[i].Nome.ToUpper().Contains(nome.ToUpper()) || agenda[i].Sobrenome.ToUpper().Contains(nome.ToUpper()))
                {
                    return agenda[i];
                }
            }
            return null;
        }

        // Função que busca, pelo nome ou sobrenome, mais de uma pessoa na agenda
        public List<Pessoa> BuscarPessoasPeloNome(string nome)
        {
            List<Pessoa> pessoasComMesmoNome = new List<Pessoa>();

            for (int i = 0; i < agenda.Count; i++)
            {
                if (agenda[i].Nome.ToUpper().Contains(nome.ToUpper()) || agenda[i].Sobrenome.ToUpper().Contains(nome.ToUpper()))
                    pessoasComMesmoNome.Add(agenda[i]);
            }
            return pessoasComMesmoNome;

        }

        // Função que busca o aniversariante do dia
        public Pessoa BuscarPessoaPorDataNascimento(DateTime nascimento)
        {
            for (int i = 0; i < agenda.Count; i++)
            {
                if (agenda[i].DataNascimento.Date.Day == nascimento.Date.Day && agenda[i].DataNascimento.Date.Month == nascimento.Date.Month)
                    return agenda[i];
            }
            return null;
        }

        // Função que busca o~s aniversariantes do dia
        public List<Pessoa> BuscarPessoasPorDataNascimento(DateTime nascimento)
        {
            List<Pessoa> aniversarianteDoDia = new List<Pessoa>();

            for (int i = 0; i < agenda.Count; i++)
            {
                if (agenda[i].DataNascimento.Date.Day == DateTime.Now.Date.Day && agenda[i].DataNascimento.Date.Month == DateTime.Now.Date.Month)
                    aniversarianteDoDia.Add(agenda[i]);
                //return aniversarianteDoDia;
            }
            return aniversarianteDoDia;
        }

    }
}
