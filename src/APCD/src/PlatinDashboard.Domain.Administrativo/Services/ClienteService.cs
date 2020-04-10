using PlatinDashboard.Domain.Administrativo.Entities;
using PlatinDashboard.Domain.Administrativo.Interfaces.Repositories;
using PlatinDashboard.Domain.Administrativo.Interfaces.Services;
using System.Collections.Generic;

namespace PlatinDashboard.Domain.Administrativo.Services
{
    public class ClienteService : ServiceBase<Cliente>, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository) : base(clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public Farmacia BuscarFarmaciaPorId(int farmaciaId)
        {
            return _clienteRepository.BuscarFarmaciaPorId(farmaciaId);
        }

        public IEnumerable<Farmacia> BuscarFarmaciasAtivas()
        {
            return _clienteRepository.BuscarFarmaciasAtivas();
        }

        public IEnumerable<Farmacia> BuscarFarmaciasPorRede(int codigoCliente)
        {
            return _clienteRepository.BuscarFarmaciasPorRede(codigoCliente);
        }

        public Rede BuscarRedePorCodigoCliente(int codigoCliente)
        {
            return _clienteRepository.BuscarRedePorCodigoCliente(codigoCliente);
        }

        public IEnumerable<Rede> BuscarRedesAtivas()
        {
            return _clienteRepository.BuscarRedesAtivas();
        }

        public Usuario BuscarUsuarioPorCredencial(string login, string senha)
        {
            return _clienteRepository.BuscarUsuarioPorCredencial(login, senha);
        }

        public Usuario BuscarUsuarioPorId(int usuarioId)
        {
            return _clienteRepository.BuscarUsuarioPorId(usuarioId);
        }

        public IEnumerable<Usuario> BuscarUsuariosPorRede(int codigoCliente)
        {
            return _clienteRepository.BuscarUsuariosPorRede(codigoCliente);
        }
    }
}
