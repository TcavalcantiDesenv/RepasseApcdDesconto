using PlatinDashboard.Application.Administrativo.ViewModels;
using System.Collections.Generic;

namespace PlatinDashboard.Application.Administrativo.Interfaces
{
    public interface IClienteAppService
    {
        IEnumerable<RedeViewModel> BuscarRedesAtivas();
        RedeViewModel BuscarRedePorCodigoCliente(int codigoCliente);
        FarmaciaViewModel BuscarFarmaciaPorId(int farmaciaId);
        IEnumerable<FarmaciaViewModel> BuscarFarmaciasAtivas();
        IEnumerable<FarmaciaViewModel> BuscarFarmaciasPorRede(int codigoCliente);
        IEnumerable<UsuarioViewModel> BuscarUsuariosPorRede(int codigoCliente);
        UsuarioViewModel BuscarUsuarioPorId(int usuarioId);
        UsuarioViewModel BuscarUsuarioPorCredencial(string login, string senha);
    }
}
