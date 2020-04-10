using PlatinDashboard.Domain.Administrativo.Entities;
using System.Collections.Generic;

namespace PlatinDashboard.Domain.Administrativo.Interfaces.Services
{
    public interface IClienteService : IServiceBase<Cliente>
    {
        IEnumerable<Rede> BuscarRedesAtivas();
        Rede BuscarRedePorCodigoCliente(int codigoCliente);
        Farmacia BuscarFarmaciaPorId(int farmaciaId);
        IEnumerable<Farmacia> BuscarFarmaciasAtivas();
        IEnumerable<Farmacia> BuscarFarmaciasPorRede(int codigoCliente);
        IEnumerable<Usuario> BuscarUsuariosPorRede(int codigoCliente);
        Usuario BuscarUsuarioPorId(int usuarioId);
        Usuario BuscarUsuarioPorCredencial(string login, string senha);
    }
}
