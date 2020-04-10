using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlatinDashboard.Infra.CrossCutting.Identity.Model
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            ClientesWeb = new Collection<ClienteWeb>();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        //Propriedades do Usuário do Domínio
        public string Name { get; set; } 
        public string LastName { get; set; }
        public bool Active { get; set; }
        public string UserType { get; set; }
        public int CompanyId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? AdministrativeCode { get; set; }
        public bool ImportedFromAdministrative { get; set; }

        public string NovoValor { get; set; }
        public string DataInicial { get; set; }
        public string DataFinal { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<ClienteWeb> ClientesWeb { get; set; }

        [NotMapped]
        public string CurrentClientId { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, ClaimsIdentity ext = null)
        {
            // Observe que o authenticationType precisa ser o mesmo que foi definido em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(CurrentClientId))
            {
                claims.Add(new Claim("AspNet.Identity.ClientId", CurrentClientId));
            }

            //  Adicione novos Claims aqui //

            // Adicionando Claims externos capturados no login
            if (ext != null)
            {
                await SetExternalProperties(userIdentity, ext);
            }

            // Gerenciamento de Claims para informaçoes do usuario
            //claims.Add(new Claim("AdmRoles", "True"));

            userIdentity.AddClaims(claims);
            userIdentity.AddClaim(new Claim("FirstName", Name));
            userIdentity.AddClaim(new Claim("LastName", LastName ?? string.Empty));
            userIdentity.AddClaim(new Claim("FullName", $"{Name} {LastName ?? string.Empty}"));
            userIdentity.AddClaim(new Claim("UserType", UserType));
            return userIdentity;
        }

        private async Task SetExternalProperties(ClaimsIdentity identity, ClaimsIdentity ext)
        {
            if (ext != null)
            {
                var ignoreClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";
                // Adicionando Claims Externos no Identity
                foreach (var c in ext.Claims)
                {
                    if (!c.Type.StartsWith(ignoreClaim))
                        if (!identity.HasClaim(c.Type, c.Value))
                            identity.AddClaim(c);
                }
            }
        }
    }
}
