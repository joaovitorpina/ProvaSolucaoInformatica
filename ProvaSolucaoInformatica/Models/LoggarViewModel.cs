using System.ComponentModel.DataAnnotations;

namespace ProvaSolucaoInformatica.Models
{
    public class LoggarViewModel
    {
        [Required(ErrorMessage = "Preencha o campo de login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Preencha o campo de senha")]
        public string Senha { get; set; }
    }
}