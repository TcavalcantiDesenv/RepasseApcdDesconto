using Model.Dao;
using Model.Entity;
using System.Collections.Generic;

namespace Model.Neg
{
    public class AcessosNeg
    {
        private AcessosDao objAcessosDao;
       
        public AcessosNeg()
        {
            objAcessosDao = new AcessosDao();
        }
        public bool find(Acessos objAcessos)
        {
            return objAcessosDao.find(objAcessos);
        }
        public List<Acessos> findAll()
        {
            return objAcessosDao.findAll();
        }

        public int buscarUltimoID()
        {
            return objAcessosDao.buscarUltimoID();
        }
        public List<Acessos> BuscarTodosPorUsuario(Acessos obj)
        {
            return objAcessosDao.buscarAcessosPorUsuario(obj);
        }
        public void delete(Acessos objAcessos)
        {
            bool verificacao = true;
            //verificando se existe
            Acessos objobjAcessosAux = new Acessos();
            objobjAcessosAux.IdAcesso = objAcessos.IdAcesso;
            verificacao = objAcessosDao.find(objobjAcessosAux);
            if (!verificacao)
            {
                objAcessos.Estados = 33;
                return;
            }

            objAcessos.Estados = 99;
            objAcessosDao.delete(objAcessos);
            return;
        }
        public void AtualizaSaida(string id)
        {
            //tudo ok edite
            objAcessosDao.AtualizaSaida(id);
            return;
        }

        public void create(Acessos obj)
        {
            bool verificacao = true;

            string Nome = obj.Nome.ToString();
            string Usuario = obj.Usuario;
            string DataEntrada = obj.DataEntrada.ToString();
            string DataSaida = obj.DataSaida.ToString();
            string Empresa = obj.Empresa;
            string IP = obj.IP;

            //begin verificar duplicidade cpf retorna estado=8
            Acessos objAcessos1 = new Acessos();
            objAcessos1.IdAcesso = obj.IdAcesso;
            //se nao tem erro
            obj.Estados = 99;
        
            objAcessosDao.create(obj);
            return;
        }

    }
}
