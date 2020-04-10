using System.Collections.Generic;
using System.Linq;
using PlatinDashboard.Domain.Administrativo.Entities;
using PlatinDashboard.Domain.Administrativo.Interfaces.Repositories;

namespace PlatinDashboard.Infra.Data.Administrativo.Repositories
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
        public Farmacia BuscarFarmaciaPorId(int farmaciaId)
        {
            var cliente = Db.Clientes.FirstOrDefault(c => c.Id == farmaciaId);
            return cliente != null ? new Farmacia(cliente) : null;
        }

        public IEnumerable<Farmacia> BuscarFarmaciasAtivas()
        {
            var clientes = Db.Clientes.Where(c => c.Situacao == "ativo");
            var farmacias = new List<Farmacia>();
            foreach (var cliente in clientes)
            {
                farmacias.Add(new Farmacia(cliente));
            }
            return farmacias;
        }

        public IEnumerable<Farmacia> BuscarFarmaciasPorRede(int codigoCliente)
        {
            var clientes = Db.Clientes.Where(c => c.CodigoCliente == codigoCliente);
            var farmacias = new List<Farmacia>();
            foreach (var cliente in clientes)
            {
                farmacias.Add(new Farmacia(cliente));
            }
            return farmacias;
        }

        public Rede BuscarRedePorCodigoCliente(int codigoCliente)
        {
            var cliente = Db.Clientes.FirstOrDefault(c => c.CodigoCliente == codigoCliente && c.Tipo == "matriz");
            return cliente != null ? new Rede(cliente) : null;
        }

        public IEnumerable<Rede> BuscarRedesAtivas()
        {
            var clientes = Db.Clientes.Where(c => c.Tipo == "matriz" && c.Situacao == "ativo");
            var redes = new List<Rede>();
            foreach (var cliente in clientes)
            {
                redes.Add(new Rede(cliente));
            }
            return redes.OrderBy(r => r.NomeFantasia);
        }

        public Usuario BuscarUsuarioPorCredencial(string login, string senha)
        {
            var cliente = Db.Clientes.FirstOrDefault(c => c.Login == login && c.Senha == senha);
            return cliente != null ? new Usuario(cliente) : null;
        }

        public Usuario BuscarUsuarioPorId(int usuarioId)
        {
            var cliente = Db.Clientes.FirstOrDefault(c => c.Id == usuarioId);
            return cliente != null ? new Usuario(cliente) : null;
        }

        public IEnumerable<Usuario> BuscarUsuariosPorRede(int codigoCliente)
        {
            var clientes = Db.Clientes.Where(c => c.CodigoCliente == codigoCliente);
            var usuarios = new List<Usuario>();
            foreach (var cliente in clientes)
            {
                usuarios.Add(new Usuario(cliente));
            }
            return usuarios;
        }
    }
}
