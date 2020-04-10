using Model.Dao;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Neg
{
    public class RegionaisNeg
    {
        private RegionaisDao objRemessaDao;
        public RegionaisNeg()
        {
            objRemessaDao = new RegionaisDao();
        }

        public List<Regionais> findAll()
        {
            return objRemessaDao.findAll();
        }
        public List<Regionais> BuscaPorRegional(string codigo)
        {
            return objRemessaDao.BuscaPorCodigo(codigo);
        }
        public List<Regionais> BuscaPorNome(string nome)
        {
            return objRemessaDao.BuscaPorNome(nome);
        }

    }
}
