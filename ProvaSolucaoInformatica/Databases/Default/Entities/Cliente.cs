using System.ComponentModel.DataAnnotations;

namespace ProvaSolucaoInformatica.Databases.Default.Entities
{
    public class Cliente
    {
        [Key] public int Codigo { get; set; }

        [Required(ErrorMessage = "Preencha o campo de Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo de Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Preencha o campo de Telefone")]
        public string Telefone { get; set; }
    }
}