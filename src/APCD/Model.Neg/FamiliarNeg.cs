using Model.Dao;
using Model.Entity;
using System.Collections.Generic;

namespace Model.Neg
{
    public class FamiliarNeg
    {
        private FamiliarDao objFamiliarDao;

        public FamiliarNeg()
        {
            objFamiliarDao = new FamiliarDao();
        }

        public void create(Familiar objFamiliar)
        {
            bool verificacao = true;
            objFamiliarDao.create(objFamiliar);
            return;
        }
        public void update(Familiar objFamiliar)
        {
            int Id_Familiar = objFamiliar.Id_Familiar;
            string Cim = objFamiliar.Cim.ToString();
            string Nome = objFamiliar.Nome;
            string Grau = objFamiliar.Grau;
            objFamiliarDao.update(objFamiliar);
            return;
        }
        public void delete(Familiar objFamiliar)
        {
            Familiar objFamiliarAux = new Familiar();
            objFamiliarAux.Id_Familiar = objFamiliar.Id_Familiar;
            objFamiliarDao.delete(objFamiliar);
            return;
        }
        public bool find(Familiar objFamiliar)
        {
            return objFamiliarDao.find(objFamiliar);
        }
        public List<Familiar> findAll()
        {
            return objFamiliarDao.findAll();
        }
        public List<Familiar> findAllFamiliar(Familiar objFamiliar)
        {
            return objFamiliarDao.findAllFamiliar(objFamiliar);
        }
    }
}
