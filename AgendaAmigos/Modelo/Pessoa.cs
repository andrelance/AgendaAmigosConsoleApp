using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Classe LIB Padronizada para formação do objeto pessoa
    /// </summary>
    public class Pessoa
    {
        public Guid IdPessoa { get; set; } // O Id da pessoa é gerado automaticamente pelo sistema (Program)
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; } // data no formato dd/mm/yyyy (Program)
    }
}
