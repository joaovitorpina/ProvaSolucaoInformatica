using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProvaSolucaoInformatica.Databases.Default;
using ProvaSolucaoInformatica.Databases.Default.Entities;

namespace ProvaSolucaoInformatica.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        public ClienteController(DefaultDbContext defaultDbContext)
        {
            DefaultDbContext = defaultDbContext;
        }

        private DefaultDbContext DefaultDbContext { get; }

        public ViewResult Index(string searchString)
        {
            IEnumerable<Cliente> clientes = !string.IsNullOrWhiteSpace(searchString)
                ? DefaultDbContext.Clientes.Where(c => c.Nome.Contains(searchString))
                : DefaultDbContext.Clientes;

            return View(clientes.ToList());
        }

        public IActionResult Criar()
        {
            return View("Editor", new Cliente());
        }

        public IActionResult Editar(Cliente cliente)
        {
            return View("Editor", cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Cliente cliente)
        {
            if (!ModelState.IsValid)
                return View("Editor", cliente);

            try
            {
                cliente.Telefone = FormatarTelefone(cliente.Telefone);

                DefaultDbContext.Clientes.Update(cliente);
                await DefaultDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("summary", e.Message);
                return View("Editor", cliente);
            }
        }

        public async Task<IActionResult> Deletar(Cliente cliente)
        {
            DefaultDbContext.Clientes.Remove(cliente);
            await DefaultDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private string FormatarTelefone(string telefone)
        {
            var telefoneFormatado =
                telefone.Replace("(", null).Replace(")", null).Replace("-", null).Replace(" ", null);

            if (!telefoneFormatado.All(char.IsDigit))
                throw new Exception("Telefone não pode conter letras!");

            var ddd = $"{telefoneFormatado[0]}{telefoneFormatado[1]}";
            telefoneFormatado = telefoneFormatado.Remove(0, 2);

            if (telefoneFormatado.Length >= 10 || telefoneFormatado.Length <= 7)
                throw new Exception("Telefone formatado de forma incorreta!");

            var prefixoTelefone = telefoneFormatado.Substring(0, telefoneFormatado.Length - 4);

            return $"({ddd}) {prefixoTelefone}-{telefoneFormatado.Substring(prefixoTelefone.Length)}";
        }
    }
}