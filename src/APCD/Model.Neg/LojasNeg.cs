using Model.Dao;
using Model.Entity;
using System.Collections.Generic;

namespace Model.Neg
{
    public class LojasNeg
    {
        private LojasDao objLojasDao;

        public LojasNeg()
        {
            objLojasDao = new LojasDao();
        }
        public List<Lojas> findAll()
        {
            return objLojasDao.findAll();
        }
    }
}
