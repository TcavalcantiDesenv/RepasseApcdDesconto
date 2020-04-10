using Model.Dao;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Neg
{
   public class RemessaNeg 
    {
        private RemessaDao objRemessaDao;
        public RemessaNeg()
        {
            objRemessaDao = new RemessaDao();
        }

        public List<RemessaWeb> findAll()
        {
            return objRemessaDao.findAll();
        }
        public List<RemessaWeb> BuscaPorRegional(string regional, bool pago, string ativo, string mesano, DateTime DataIni, DateTime DataFim, bool Ativo, string novoValor)
        {
            return objRemessaDao.BuscaPorRegional(regional, pago, ativo, mesano, DataIni, DataFim, Ativo, novoValor);
        }
        public List<RemessaWeb> BuscaPorCodRegional(string regional, string mesano, DateTime DataIni, DateTime DataFim, bool Ativo, string novoValor)
        {
            return objRemessaDao.BuscaPorCodRegional(regional, mesano, DataIni, DataFim, Ativo, novoValor);
        }
        public List<RemessaWeb> BuscaPagos(string ativo)
        {
            return objRemessaDao.BuscaPagos(ativo);
        }
        public List<RemessaWeb> BuscaPorId(string Id)
        {
            return objRemessaDao.BuscaPorId(Id);
        }
        public void update(RemessaWeb objRemessa)
        {
            objRemessaDao.update(objRemessa);
            return;
        }
        public void baixaLote(string Pago, string Ativo, string CodRegional, string MesAno, string ListaLCM)
        {
            objRemessaDao.baixaLote(Pago, Ativo, CodRegional, MesAno, ListaLCM);
            return;
        }
        public void create(RemessaWeb objMacon)
        {
            objRemessaDao.create(objMacon);
            return;
        }
    }
}
