using Model.Dao;
using Model.Entity;
using System.Collections.Generic;

namespace Model.Neg
{
    public class BalaustresNeg
    {
        private BalaustresDao objBalaustresDao;

        public BalaustresNeg()
        {
            objBalaustresDao = new BalaustresDao();
        }

        public void create(Balaustres objBalaustres)
        {
            bool verificacao = true;
            int Id_Macon = objBalaustres.Id_Macon;
            string Id_Loja = objBalaustres.Id_Loja.ToString();
            string Nome = objBalaustres.Nome;
            string DataLoja = objBalaustres.DataLoja.ToString();

            Balaustres objBalaustres1 = new Balaustres();
            objBalaustres1.Id_Macon = objBalaustres.Id_Macon;
            verificacao = !objBalaustresDao.find(objBalaustres1);
            if (!verificacao)
            {
                objBalaustres.Estados = 9;
                return;
            }


            //se nao tem erro
            objBalaustres.Estados = 99;
            objBalaustresDao.create(objBalaustres);
            return;
        }
        public void update(Balaustres objBalaustres)
        {
            bool verificacao = true;
            //begin validar codigo retorna estado=1
            int Id_Macon = objBalaustres.Id_Macon;
            string Id_Loja = objBalaustres.Id_Loja.ToString();
            string Nome = objBalaustres.Nome;
            string DataLoja = objBalaustres.DataLoja.ToString();


            // Inicia validacao

            /// ???????????

            //se nao tem erro
            objBalaustres.Estados = 99;
            objBalaustresDao.update(objBalaustres);
            return;
        }
        public void delete(Balaustres objBalaustres)
        {
            bool verificacao = true;
            //verificando se existe
            Balaustres objBalaustresAux = new Balaustres();
            objBalaustresAux.Id_Macon = objBalaustres.Id_Macon;
            verificacao = objBalaustresDao.find(objBalaustresAux);
            if (!verificacao)
            {
                objBalaustres.Estados = 33;
                return;
            }


            objBalaustres.Estados = 99;
            objBalaustresDao.delete(objBalaustres);
            return;
        }
        public bool find(Balaustres objBalaustres)
        {
            return objBalaustresDao.find(objBalaustres);
        }
        public List<Balaustres> findAll()
        {
            return objBalaustresDao.findAll();
        }
        public List<Balaustres> findAllBalaustres(Balaustres objBalaustres)
        {
            return objBalaustresDao.findAllBalaustres(objBalaustres);
        }
    }
}
