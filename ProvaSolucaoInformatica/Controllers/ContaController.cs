using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProvaSolucaoInformatica.Models;
using ProvaSolucaoInformatica.Services;

namespace ProvaSolucaoInformatica.Controllers
{
    public class ContaController : Controller
    {
        public ContaController(GerenciadorUsuarios gerenciadorUsuarios)
        {
            GerenciadorUsuarios = gerenciadorUsuarios;
        }

        private GerenciadorUsuarios GerenciadorUsuarios { get; }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoggarViewModel loggarForm)
        {
            if (!ModelState.IsValid)
                return View(loggarForm);

            try
            {
                await GerenciadorUsuarios.Loggar(HttpContext, loggarForm.Login, loggarForm.Senha);
                return RedirectToAction("Index", "Cliente");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("summary", e.Message);
                return View(loggarForm);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await GerenciadorUsuarios.Deslogar(HttpContext);
            return RedirectToAction("Index", "Index");
        }
    }
}