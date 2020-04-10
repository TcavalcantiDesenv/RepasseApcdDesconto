using Model.Dao;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Neg
{
    public class EnderecosNeg
    {
        private EnderecosDao objEnderecosDao;
        public EnderecosNeg()
        {
            objEnderecosDao = new EnderecosDao();
        }
        public List<Enderecos> BuscaPorCodigo(string codigo)
        {
            return objEnderecosDao.BuscaPorCodigo(codigo);
        }
    }
}
