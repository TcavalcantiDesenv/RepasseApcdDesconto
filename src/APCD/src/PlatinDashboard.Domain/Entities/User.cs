using System;
using System.Collections.Generic;

namespace PlatinDashboard.Domain.Entities
{
    public class User
    {
        private string _email;
        private string _userName;

        public User()
        {
            UserId = Guid.NewGuid().ToString();
            EmailConfirmed = true;
            Active = true;
            LockoutEnabled = true;
            AccessFailedCount = 0;
            LockoutEndDateUtc = null;
            SecurityStamp = Guid.NewGuid().ToString();
            UserClaims = new List<UserClaim>();
            AdministrativeCode = null;
        }

        public string UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName
        {
            get
            {
                return string.IsNullOrEmpty(_userName) ? _userName : _userName.ToLower();
            }
            set { _userName = value; }
        }
        public string Email
        {
            get
            {
                return string.IsNullOrEmpty(_email) ? _email : _email.ToLower();
            }
            set { _email = value; }
        }
        public string NovoValor { get; set; } 
        public string DataInicial { get; set; }
        public string DataFinal { get; set; }
        public string Descricao { get; set; }
        public bool Active { get; set; }
        public string UserType { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }

        //public string NovoValor { get; set; }
        //public string DataInicial { get; set; }
        //public string DataFinal { get; set; }


        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public int AccessFailedCount { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserStore> UsersStores { get; set; }

        //Propriedades de controle da base do sistema administrativo
        public int? AdministrativeCode { get; set; }
        public bool ImportedFromAdministrative { get; set; }
    }
}