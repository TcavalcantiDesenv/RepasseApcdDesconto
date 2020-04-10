using System;
using System.Collections.Generic;

namespace PlatinDashboard.Domain.Entities
{
    public class Company
    {
        public Company()
        {
            CustomerCode = null;
            Charts = new List<Chart>();
        }

        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string CompanyType { get; set; }
        public string DatabaseServer { get; set; }
        public string Database { get; set; }
        public string DatabasePort { get; set; }
        public string DatabaseProvider { get; set; }
        public string DatabaseUser { get; set; }
        public string DatabasePassword { get; set; }        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Chart> Charts { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
        public virtual ICollection<Store> Stores { get; set; }

        //Propriedades de controle da base do sistema administrativo
        public int? CustomerCode { get; set; }
        public bool ImportedFromAdministrative { get; set; }
    }    
}