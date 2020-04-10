using Loja;
using Model.Dao;
using System.Collections.Generic;

namespace Model.Neg
{
    class VisitanteNeg
    {
        private VisitanteDao objVisitanteDao;

        public VisitanteNeg()
        {
            objVisitanteDao = new VisitanteDao();
        }

        public void create(Visitante objVisitante)
        {
            bool verificacao = true;
            int Id_Visitante = objVisitante.Id_Visitante;
            string Id_Loja = objVisitante.Id_Loja.ToString();
            string Cim = objVisitante.Cim.ToString();
            string Obediencia = objVisitante.Obediencia;
            string Dt_Visita = objVisitante.Dt_Visita.ToString();

            Visitante objVisitante1 = new Visitante();
            objVisitante1.Id_Visitante = objVisitante.Id_Visitante;
            verificacao = !objVisitanteDao.find(objVisitante1);
            if (!verificacao)
            {
                objVisitante.Estados = 9;
                return;
            }


            //se nao tem erro
            objVisitante.Estados = 99;
            objVisitanteDao.create(objVisitante);
            return;
        }
        public void update(Visitante objVisitante)
        {
            bool verificacao = true;
            //begin validar codigo retorna estado=1
            int Id_Visitante = objVisitante.Id_Visitante;
            string Id_Loja = objVisitante.Id_Loja.ToString();
            string Cim = objVisitante.Cim.ToString();
            string Obediencia = objVisitante.Obediencia;
            string Dt_Visita = objVisitante.Dt_Visita.ToString();


            // Inicia validacao

            /// ???????????

            //se nao tem erro
            objVisitante.Estados = 99;
            objVisitanteDao.update(objVisitante);
            return;
        }
        public void delete(Visitante objVisitante)
        {
            bool verificacao = true;
            //verificando se existe
            Visitante objVisitanteAux = new Visitante();
            objVisitanteAux.Id_Visitante = objVisitante.Id_Visitante;
            verificacao = objVisitanteDao.find(objVisitanteAux);
            if (!verificacao)
            {
                objVisitante.Estados = 33;
                return;
            }


            objVisitante.Estados = 99;
            objVisitanteDao.delete(objVisitante);
            return;
        }
        public bool find(Visitante objVisitante)
        {
            return objVisitanteDao.find(objVisitante);
        }
        public List<Visitante> findAll()
        {
            return objVisitanteDao.findAll();
        }
        public List<Visitante> findAllVisitante(Visitante objVisitante)
        {
            return objVisitanteDao.findAllVisitante(objVisitante);
        }
    }
}
