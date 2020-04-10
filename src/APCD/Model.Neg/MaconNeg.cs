using Model.Dao;
using Model.Entity;
using System.Collections.Generic;
using System;

namespace Model.Neg
{
    public class MaconNeg
    {
        private MaconDao objMaconDao;
        //public string nivel = Session["GUIDE"].ToString();

        public MaconNeg()
        {
            objMaconDao = new MaconDao();
        }

        public void create(Macon objMacon)
        {
            
            //Macon objMacon1 = new Macon();
            //objMacon1.Id_Macon = objMacon.Id_Macon;
            //var verificacao = !objMaconDao.find(objMacon1);
            //if (!verificacao)
            //{
            //    objMacon.Estados = 9;
            //    return;
            //}


            ////se nao tem erro
            //objMacon.Estados = 99;
            objMaconDao.create(objMacon);
            return;
        }
        public void update(Macon objMacon)
        {
           
            bool verificacao = true;
            ////begin validar codigo retorna estado=1
            //int Id_Macon = objMacon.Id_Macon;
            //string Cim = objMacon.Cim.ToString();
            //string Id_Loja = objMacon.Id_Loja.ToString();
            //string Id_Graduacao = objMacon.Id_Graduacao.ToString();
            //string Id_Cargo = objMacon.Id_Cargo.ToString();
            //string Id_Condecoracao = objMacon.Id_Condecoracao.ToString();
            //string Nome = objMacon.Nome;
            //string Guide = objMacon.Guide;
            //string Nome_Tratamento = objMacon.Nome_Tratamento;
            //string Senha = objMacon.Senha;
            //string Situacao = objMacon.Situacao;
            //string CPF = objMacon.CPF;
            //string Login = objMacon.Login;
            //string Dt_Iniciado = objMacon.Dt_Iniciado;
            //string Email = objMacon.Email;
            //string Email_Outros = objMacon.Email_Outros;
            //string Dt_Nascimento = objMacon.Dt_Nascimento;
            //string Naturalidade = objMacon.Naturalidade;
            //string Uf = objMacon.Uf;
            //string Nacionalidade = objMacon.Nacionalidade;
            //string Religiao = objMacon.Religiao;
            //string Escolaridade = objMacon.Escolaridade;
            //string Sexo = objMacon.Sexo;
            //string Tipo_Sanguíneo = objMacon.Tipo_Sanguíneo;
            //string Estado_Civil = objMacon.Estado_Civil;
            //string Data_Casamento = objMacon.Data_Casamento;
            //string Tipo_Di = objMacon.Tipo_Di;
            //string Num_Di = objMacon.Num_Di;
          
            //string Dt_Emissao_Di = objMacon.Dt_Emissao_Di;
            //string Uf_Di = objMacon.Uf_Di;
            //string Tit_Eleitor = objMacon.Tit_Eleitor;
            //string Zona_Eleitoral = objMacon.Zona_Eleitoral;
            //string Secao_Eleitoral = objMacon.Secao_Eleitoral;
            //string Nome_Pai = objMacon.Nome_Pai;
            //string Nome_Mae = objMacon.Nome_Mae;
            //string End_Res = objMacon.End_Res;
            //string Bairro_Res = objMacon.Bairro_Res;
            //string Cidade_Res = objMacon.Cidade_Res;
            //string Uf_Res = objMacon.Uf_Res;
            //string Cep_Res = objMacon.Cep_Res;
            //string cx_postal = objMacon.cx_postal;
            //string celular = objMacon.celular;
            //string tel_res = objMacon.tel_res;
            //string fax = objMacon.fax;
            //string obs = objMacon.obs;
            //string correspondencia_end = objMacon.correspondencia_end;
            ////string foto = objMacon.foto.ToString();


            // Inicia validacao

            /// ???????????

            //se nao tem erro
            objMacon.Estados = 99;
            objMaconDao.update(objMacon);
            return;
        }
        public void delete(Macon objMacon)
        {
            bool verificacao = true;
            //verificando se existe
            Macon objMaconAux = new Macon();
            objMaconAux.Id_Macon = objMacon.Id_Macon;
            verificacao = objMaconDao.find(objMaconAux);
            if (!verificacao)
            {
                objMacon.Estados = 33;
                return;
            }


            objMacon.Estados = 99;
            objMaconDao.delete(objMacon);
            return;
        }
        public bool find(Macon objMacon)
        {
            return objMaconDao.find(objMacon);
        }
        public List<Macon> FindPorId(Macon obj1)
        {
            return objMaconDao.FindPorId(obj1);
        }
        public List<Macon> FindPorGuide(Macon obj1)
        {
            return objMaconDao.FindPorGuide(obj1);
        }
        public List<Macon> PesquisarGuide(string usuario)
        {
            return objMaconDao.PesquisarGuide(usuario);
        }
        public List<Macon> findAll()
        {
            return objMaconDao.findAll();
        }
        public List<Macon> findAllMacon(Macon objMacon)
        {
            return objMaconDao.findAllMacon(objMacon);
        }

        public List<Situacao> ListaSituacao()
        {
            return objMaconDao.ListaSituacao();
        }
        public List<Tipo_Sanguíneo> ListaTipoSangue()
        {
            return objMaconDao.ListaTipoSangue();
        }
        public List<Sexo> ListaSexo()
        {
            return objMaconDao.ListaSexo();
        }
        public List<Estado_Civil> ListaEstado_Civil()
        {
            return objMaconDao.ListaEstado_Civil();
        }
        public List<Escolaridade> ListaEscolaridade()
        {
            return objMaconDao.ListaEscolaridade();
        }
        public List<Religiao> ListaReligiao()
        {
            return objMaconDao.ListaReligiao();
        }
    }
}
