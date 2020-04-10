using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Infra.Data.Farmacia.EntityConfig.MySql;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PlatinDashboard.Infra.Data.Farmacia.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbConnection dbConnection) : base(dbConnection, false)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<ClsCab> ClsCabs { get; set; }
        public DbSet<ClsVenAll> ClsVenAlls { get; set; }
        public DbSet<GraficoWeb> GraficosWeb { get; set; }
        public DbSet<VenUad> VensUad { get; set; }
        public DbSet<VenUadMensal> VensUadMensal { get; set; }
        public DbSet<VenUadCls> VensUadCls { get; set; }
        public DbSet<VenUadClsDash> VensUadClsDash { get; set; }
        public DbSet<UadCab> UadCabs { get; set; }
        public DbSet<FunCab> FunCabs { get; set; }
        public DbSet<ViewBalconista> ViewBalconistas { get; set; }
        public DbSet<VendaBalconistaPorHora> VendasBalconistaPorHora { get; set; }
        public DbSet<VendaLojaPorHora> VendasLojaPorHora { get; set; }
        public DbSet<VendaLojaPorMes> VendasLojaPorMes { get; set; }
        public DbSet<ClsVenAllGraficoTotal> ClsVenAllGraficoTotals { get; set; }
        public DbSet<ClsVenAllGraficoPorCls> ClsVenAllGraficosPorCls { get; set; }
        public DbSet<VenAll> VenAlls { get; set; }
        public DbSet<VItensPorCliente> VItensPorClientes { get; set; }
        public DbSet<VendaBalconistaPorClassificacao> VendasBalconistaPorClassificacao { get; set; }
        public DbSet<BalVenAllGraficoTotal> BalVenAllGraficoTotals { get; set; }
        public DbSet<BalVenAllGraficoPorCls> BalVenAllGraficoPorClss { get; set; }
        public DbSet<VendaClassificacaoLojaSp> VendaClassificacaoLojaSp { get; set; }
        public DbSet<VendaClassificacaoLojaTotalSp> VendaClassificacaoLojaTotalSp { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new ClsCabConfiguration());
            modelBuilder.Configurations.Add(new ClsVenAllConfiguration());
            modelBuilder.Configurations.Add(new GraficoWebConfiguration());
            modelBuilder.Configurations.Add(new VenUadConfiguration());
            modelBuilder.Configurations.Add(new VenUadMensalConfiguration());
            modelBuilder.Configurations.Add(new VenUadClsConfiguration());
            modelBuilder.Configurations.Add(new VenUadClsDashConfiguration());
            modelBuilder.Configurations.Add(new UadCabConfiguration());
            modelBuilder.Configurations.Add(new FunCabConfiguration());
            modelBuilder.Configurations.Add(new ViewBalconistaConfiguration());
            modelBuilder.Configurations.Add(new VendaBalconistaPorHoraConfiguration());
            modelBuilder.Configurations.Add(new VendaLojaPorHoraConfiguration());
            modelBuilder.Configurations.Add(new VendaLojaPorMesConfiguration()); 
            modelBuilder.Configurations.Add(new ClsVenAllGraficoTotalConfiguration());
            modelBuilder.Configurations.Add(new ClsVenAllGraficoPorClsConfiguration());
            modelBuilder.Configurations.Add(new VenAllConfiguration());
            modelBuilder.Configurations.Add(new VItensPorClienteConfiguration());
            modelBuilder.Configurations.Add(new VendaBalconistaPorClassificacaoConfiguration());
            modelBuilder.Configurations.Add(new BalVenAllGraficoTotalConfiguration());
            modelBuilder.Configurations.Add(new BalVenAllGraficoPorClsConfiguration());
            modelBuilder.Configurations.Add(new VendaClassificacaoLojaSpConfiguration());
            modelBuilder.Configurations.Add(new VendaClassificacaoLojaTotalSpConfiguration());
        }
    }
}
