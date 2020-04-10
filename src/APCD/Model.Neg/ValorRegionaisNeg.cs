using Model.Dao;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Neg
{
    public class ValorRegionaisNeg
    {
        private ValorRegionaisDao objRemessaDao;

        public ValorRegionaisNeg()
        {
            objRemessaDao = new ValorRegionaisDao();
        }

        public void create(ValorRegionais objValor)
        {
           objRemessaDao.create(objValor);
        }

        public List<ValorRegionais> findAll()
        {
            return objRemessaDao. findAll();
        }
        public List<ValorRegionais> BuscaPorData(string codregional, string dataini, string datafim)
        {
            return objRemessaDao.BuscaPorData(codregional,dataini, datafim);
        }
        public List<ValorRegionais> BuscaPorRegional(string codregional)
        {
            return objRemessaDao.BuscaPorRegional(codregional);
        }

    }
}
