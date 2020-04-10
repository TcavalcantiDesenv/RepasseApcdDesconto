using Model.Dao;
using Model.Entity;
using System.Collections.Generic;

namespace Model.Neg
{
    public class GraduacaoNeg
    {
        private GraduacaoDao objGraduacaoDao;

        public GraduacaoNeg()
        {
            objGraduacaoDao = new GraduacaoDao();
        }

        public void create(Graduacao objGraduacao)
        {
            bool verificacao = true;
            int Id_Graduacao = objGraduacao.Id_Graduacao;
            string Descricao = objGraduacao.Descricao;
            string Numero = objGraduacao.Numero.ToString();
            string Sigla = objGraduacao.Sigla;
            string Meses_Intersticio = objGraduacao.Meses_Intersticio;
            string Dt_Inicial = objGraduacao.Dt_Inicial.ToString();
            string Dt_Final = objGraduacao.Dt_Final.ToString();

            Graduacao objGraduacao1 = new Graduacao();
            objGraduacao1.Id_Graduacao = objGraduacao.Id_Graduacao;
            verificacao = !objGraduacaoDao.find(objGraduacao1);
            if (!verificacao)
            {
                objGraduacao.Estados = 9;
                return;
            }


            //se nao tem erro
            objGraduacao.Estados = 99;
            objGraduacaoDao.create(objGraduacao);
            return;
        }
        public void update(Graduacao objGraduacao)
        {
            bool verificacao = true;
            //begin validar codigo retorna estado=1
            int Id_Graduacao = objGraduacao.Id_Graduacao;
            string Descricao = objGraduacao.Descricao;
            string Numero = objGraduacao.Numero.ToString();
            string Sigla = objGraduacao.Sigla;
            string Meses_Intersticio = objGraduacao.Meses_Intersticio;
            string Dt_Inicial = objGraduacao.Dt_Inicial.ToString();
            string Dt_Final = objGraduacao.Dt_Final.ToString();


            // Inicia validacao

            /// ???????????

            //se nao tem erro
            objGraduacao.Estados = 99;
            objGraduacaoDao.update(objGraduacao);
            return;
        }
        public void delete(Graduacao objGraduacao)
        {
            bool verificacao = true;
            //verificando se existe
            Graduacao objGraduacaoAux = new Graduacao();
            objGraduacaoAux.Id_Graduacao = objGraduacao.Id_Graduacao;
            verificacao = objGraduacaoDao.find(objGraduacaoAux);
            if (!verificacao)
            {
                objGraduacao.Estados = 33;
                return;
            }


            objGraduacao.Estados = 99;
            objGraduacaoDao.delete(objGraduacao);
            return;
        }
        public bool find(Graduacao objGraduacao)
        {
            return objGraduacaoDao.find(objGraduacao);
        }
        public List<Graduacao> findAll()
        {
            return objGraduacaoDao.findAll();
        }
        public List<Parentesco> Listar()
        {
            return objGraduacaoDao.Listar();
        }
        public List<Graduacao> findAllGraduacao(Graduacao objGraduacao)
        {
            return objGraduacaoDao.findAllGraduacao(objGraduacao);
        }
    }
}
