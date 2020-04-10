using PlatinDashboard.Domain.Farmacia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlatinDashboard.Application.Farmacia.ViewModels.Loja
{
    public class VendaLojaViewModel
    {
        public string Data { get; set; }
        public int LojaId { get; set; }
        public string Loja { get; set; }
        public decimal Bruto { get; set; }
        public decimal Desconto { get; set; }
        public decimal Devolucao { get; set; }
        public decimal Liquida { get; set; }
        public decimal Lucro { get; set; }
        public string PercentualMargem { get; set; }
        public string PercentualLucro { get; set; }
        public string TicketMedio { get; set; }
        public string QtMediaClientes { get; set; }
        public decimal ClientesAtendidos { get; set; }
        public string TotalLojas { get; set; }
        public decimal Custo { get; set; }

        public VendaLojaViewModel()
        {

        }

        public VendaLojaViewModel(GraficoWebViewModel graficoWebViewModel)
        {
            Data = graficoWebViewModel.Dat.Value.ToString("dd/MM/yyyy");
            TotalLojas = graficoWebViewModel.TotaldeLojas.ToString();
            Lucro = graficoWebViewModel?.Lucro.Value ?? 0;
            Bruto = graficoWebViewModel?.VendaBruta.Value ?? 0;
            Liquida = graficoWebViewModel?.Liquida.Value ?? 0;
            Desconto = graficoWebViewModel?.Desconto.Value ?? 0;
            PercentualMargem = string.Format("{0:N}%", graficoWebViewModel.PercentualMargem);
            PercentualLucro = string.Format("{0:N}%", (Lucro / Liquida) * 100) ?? "0";
            QtMediaClientes = graficoWebViewModel.QtMediaClientes != null ? string.Format("{0:N}", graficoWebViewModel.QtMediaClientes) : "0";
            ClientesAtendidos = graficoWebViewModel?.ClientesAtendidos.Value ?? 0;
            Custo = graficoWebViewModel?.Custo.Value ?? 0;
            Devolucao = graficoWebViewModel?.Devolucao.Value ?? 0;
            TicketMedio = string.Format("{0:N}", (Liquida / ClientesAtendidos)) ?? "0";
        }

        public VendaLojaViewModel(GraficoWeb graficoWeb)
        {
            Data = graficoWeb.Dat.Value.ToString("dd/MM/yyyy");
            TotalLojas = graficoWeb.TotaldeLojas.ToString();
            Lucro = graficoWeb?.Lucro.Value ?? 0;
            Bruto = graficoWeb?.VendaBruta.Value ?? 0;
            Liquida = graficoWeb?.Liquida.Value ?? 0;
            Desconto = graficoWeb?.Desconto.Value ?? 0;
            PercentualMargem = string.Format("{0:N}%", graficoWeb.PercentualMargem);
            PercentualLucro = string.Format("{0:N}%", (Lucro / Liquida) * 100) ?? "0";
            QtMediaClientes = graficoWeb.QtMediaClientes != null ? string.Format("{0:N}", graficoWeb.QtMediaClientes) : "0";
            ClientesAtendidos = graficoWeb?.ClientesAtendidos.Value ?? 0;
            Custo = graficoWeb?.Custo.Value ?? 0;
            Devolucao = graficoWeb?.Devolucao.Value ?? 0;
            TicketMedio = string.Format("{0:N}", (Liquida / ClientesAtendidos)) ?? "0";
        }

        public VendaLojaViewModel(VenUadViewModel venUadViewModel, IEnumerable<UadCabViewModel> lojas)
        {
            Data = venUadViewModel.Dat.ToString("dd/MM/yyyy");
            LojaId = Convert.ToInt32(venUadViewModel.Uad);
            Loja = lojas.FirstOrDefault(l => l.Uad == venUadViewModel.Uad).Des;
            Bruto = venUadViewModel?.Vlb ?? 0;
            //Bruto = venUadViewModel != null && venUadViewModel.Vlb != null ? venUadViewModel.Vlb.Value : 0;
            Desconto = venUadViewModel?.Vld ?? 0;
            Devolucao = venUadViewModel?.Vde ?? 0;
            Liquida = (venUadViewModel.Vlb - venUadViewModel.Vld) ?? 0;
            Lucro = ((venUadViewModel.Vlb - venUadViewModel.Vld) - venUadViewModel.Vlc) ?? 0;
            Custo = venUadViewModel?.Vlc ?? 0;
            PercentualMargem = venUadViewModel.Vlc > 0 ? string.Format("{0:N}", (venUadViewModel.Vlc / (venUadViewModel.Vlb - venUadViewModel.Vld) * 100).Value) + "%" : "0";
            PercentualLucro = string.Format("{0:N}%", (Lucro / Liquida) * 100) ?? "0";
            TicketMedio = venUadViewModel.Tme != null ? string.Format("{0:N}", venUadViewModel.Tme) : "0";
            QtMediaClientes = venUadViewModel.Qtp > 0 ? string.Format("{0:N}", (venUadViewModel.Qtp / venUadViewModel.Reg)) : "0";
            ClientesAtendidos = venUadViewModel.Reg != null ? venUadViewModel.Reg.Value : 0;
        }

        public VendaLojaViewModel(VendaLojaPorMesViewModel vendaLojaPorMesViewModel, IEnumerable<UadCabViewModel> lojas)
        {
            LojaId = Convert.ToInt32(vendaLojaPorMesViewModel.Uad);
            Loja = lojas.FirstOrDefault(l => l.Uad == vendaLojaPorMesViewModel.Uad).Des;
            Bruto = vendaLojaPorMesViewModel?.Bruto.Value ?? 0;
            Desconto = vendaLojaPorMesViewModel?.Desconto.Value ?? 0;
            Devolucao = vendaLojaPorMesViewModel?.Devolucao.Value ?? 0;
            Liquida = (vendaLojaPorMesViewModel.Bruto - vendaLojaPorMesViewModel.Desconto) ?? 0;
            Lucro = ((vendaLojaPorMesViewModel.Bruto - vendaLojaPorMesViewModel.Desconto) - vendaLojaPorMesViewModel.Custo) ?? 0;
            Custo = vendaLojaPorMesViewModel?.Custo.Value ?? 0;
            PercentualMargem = vendaLojaPorMesViewModel.Custo > 0 ? string.Format("{0:N}", (vendaLojaPorMesViewModel.Custo / (vendaLojaPorMesViewModel.Bruto - vendaLojaPorMesViewModel.Desconto) * 100).Value) + "%" : "0";
            PercentualLucro = string.Format("{0:N}%", (Lucro / Liquida) * 100) ?? "0";
            TicketMedio = vendaLojaPorMesViewModel.TicketMedio != null ? string.Format("{0:N}", vendaLojaPorMesViewModel.TicketMedio) : "0";
            QtMediaClientes = vendaLojaPorMesViewModel.UnidadesVendidas > 0 ? string.Format("{0:N}", (vendaLojaPorMesViewModel.UnidadesVendidas / vendaLojaPorMesViewModel.ClientesAtendidos)) : "0";
            ClientesAtendidos = vendaLojaPorMesViewModel.ClientesAtendidos != null ? vendaLojaPorMesViewModel.ClientesAtendidos.Value : 0;
        }
    }
}
