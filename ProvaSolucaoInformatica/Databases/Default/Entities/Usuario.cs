using System.ComponentModel.DataAnnotations;

namespace ProvaSolucaoInformatica.Databases.Default.Entities
{
    public class Usuario
    {
        [Key] public int Codigo { get; set; }

        public string Nome { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }
    }
}