using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace PlatinDashboard.Application.ViewModels.CompanyViewModels
{
    public class CompanyViewModel
    { 
        [Key]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "O nome da Regional é obrigatório")]
        [MinLength(3, ErrorMessage = "O nome da loja deve conter no mínimo 3 caracteres")]
        [MaxLength(255, ErrorMessage = "O nome da loja deve conter no máximo 255 caracteres")]
        [DisplayName("Nome da Regional")]
        public string Name { get; set; }

        [ScaffoldColumn(false)]
        public string CompanyType { get; set; }

        [Required(ErrorMessage = "O Tipo da Regional é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo Tipo da Regional deve conter no mínimo 3 caracteres")]
        [MaxLength(100, ErrorMessage = "O campo Tipo da Regional deve conter no máximo 100 caracteres")]
        [DisplayName("Tipo da Regional")]
        public string DatabaseServer { get; set; }

        [Required(ErrorMessage = "O campo primeiro vigilante é obrigatório")]
        [DisplayName("Informacao 1")]
        public string Database { get; set; }

        [Required(ErrorMessage = "O campo segundo vigilante é obrigatório")]
        [DisplayName("Informacao 2")]
        public string DatabasePort { get; set; }

        [Required(ErrorMessage = "O campo tipo do banco é obrigatório")]
        [DisplayName("Tipo")]
        public string DatabaseProvider { get; set; }

        [Required(ErrorMessage = "O campo cidade é obrigatório")]
        [MinLength(2, ErrorMessage = "O campo cidade deve conter no mínimo 3 caracteres")]
        [MaxLength(100, ErrorMessage = "O campo cidade deve conter no máximo 100 caracteres")]
        [DisplayName("Cidade")]
        public string DatabaseUser { get; set; }

        [Required(ErrorMessage = "O campo Estado é obrigatório")]
        [MinLength(6, ErrorMessage = "O campo Estado deve conter no mínimo 6 caracteres")]
        [MaxLength(40, ErrorMessage = "O campo Estado deve conter no máximo 40 caracteres")]
        [DisplayName("Estado")]
        public string DatabasePassword { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreatedAt { get; set; }

        [ScaffoldColumn(false)]
        public DateTime UpdatedAt { get; set; }

        public int CustomerCode { get; set; }

        [ScaffoldColumn(false)]
        public bool ImportedFromAdministrative { get; set; }

        public DbConnection GetDbConnection()
        {
            var connectionString = $"server={DatabaseServer};port={DatabasePort};user id={DatabaseUser};password={DatabasePassword};database={Database}";
            var databaseProvider = DatabaseProvider == "MySQL" ? "MySql.Data.MySqlClient" : "Npgsql";
            var conn = DbProviderFactories.GetFactory(databaseProvider).CreateConnection();
            conn.ConnectionString = connectionString;
            return conn;
        }

        public string GetConnectionString()
        {
            var connectionString = $"server={DatabaseServer};port={DatabasePort};user id={DatabaseUser};password={DatabasePassword};database={Database}";
            return connectionString;
        }

        public bool CheckConnectionConfiguration()
        {
            //Método para verificar se a conexão da empresa foi configurada
            return !DatabaseServer.Equals("Não Informado") || 
                   !DatabasePort.Equals("0000") || 
                   !DatabasePassword.Equals("Não Informado") ||
                   !DatabaseUser.Equals("Não Informado") ||
                   !DatabasePassword.Equals("Não Informado");
        }
    }    
}
