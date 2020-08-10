using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProvaSolucaoInformatica.Databases.Default;
using ProvaSolucaoInformatica.Databases.Default.Entities;

namespace ProvaSolucaoInformatica.Services
{
    public class GerenciadorUsuarios
    {
        public GerenciadorUsuarios(DefaultDbContext defaultDbContext)
        {
            DefaultDbContext = defaultDbContext;
        }

        private DefaultDbContext DefaultDbContext { get; }

        public async Task Loggar(HttpContext httpContext, string login, string senha)
        {
            var usuario =
                await DefaultDbContext.Usuarios.FirstOrDefaultAsync(u => u.Login == login && u.Senha == senha);

            if (usuario == default) throw new Exception("Usuario não encontrado!");

            var principal = new ClaimsPrincipal(new ClaimsIdentity(ObterClaimsUsuario(usuario),
                CookieAuthenticationDefaults.AuthenticationScheme));

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        public async Task Deslogar(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }

        private IEnumerable<Claim> ObterClaimsUsuario(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Codigo.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome)
            };

            return claims;
        }
    }
}