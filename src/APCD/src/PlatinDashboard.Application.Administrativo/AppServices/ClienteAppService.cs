using AutoMapper;
using PlatinDashboard.Application.Administrativo.Interfaces;
using PlatinDashboard.Application.Administrativo.ViewModels;
using PlatinDashboard.Domain.Administrativo.Entities;
using PlatinDashboard.Domain.Administrativo.Interfaces.Services;
using System.Collections.Generic;

namespace PlatinDashboard.Application.Administrativo.AppServices
{
    public class ClienteAppService : IClienteAppService
    {
        private readonly IClienteService _clienteService;

        public ClienteAppService(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public FarmaciaViewModel BuscarFarmaciaPorId(int farmaciaId)
        {
            return Mapper.Map<Farmacia, FarmaciaViewModel>(_clienteService.BuscarFarmaciaPorId(farmaciaId));
        }

        public IEnumerable<FarmaciaViewModel> BuscarFarmaciasAtivas()
        {
            return Mapper.Map<IEnumerable<Farmacia>, IEnumerable<FarmaciaViewModel>>(_clienteService.BuscarFarmaciasAtivas());
        }

        public IEnumerable<FarmaciaViewModel> BuscarFarmaciasPorRede(int codigoCliente)
        {
            return Mapper.Map<IEnumerable<Farmacia>, IEnumerable<FarmaciaViewModel>>(_clienteService.BuscarFarmaciasPorRede(codigoCliente));
        }

        public RedeViewModel BuscarRedePorCodigoCliente(int codigoCliente)
        {
            return Mapper.Map<Rede, RedeViewModel>(_clienteService.BuscarRedePorCodigoCliente(codigoCliente));
        }

        public IEnumerable<RedeViewModel> BuscarRedesAtivas()
        {
            return Mapper.Map<IEnumerable<Rede>, IEnumerable<RedeViewModel>>(_clienteService.BuscarRedesAtivas());
        }

        public UsuarioViewModel BuscarUsuarioPorCredencial(string login, string senha)
        {
            return Mapper.Map<Usuario, UsuarioViewModel>(_clienteService.BuscarUsuarioPorCredencial(login, senha));
        }

        public UsuarioViewModel BuscarUsuarioPorId(int usuarioId)
        {
            return Mapper.Map<Usuario, UsuarioViewModel>(_clienteService.BuscarUsuarioPorId(usuarioId));
        }

        public IEnumerable<UsuarioViewModel> BuscarUsuariosPorRede(int codigoCliente)
        {
            return Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(_clienteService.BuscarUsuariosPorRede(codigoCliente));
        }
    }
}
