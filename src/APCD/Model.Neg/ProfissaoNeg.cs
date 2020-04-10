using Model.Dao;
using Model.Entity;
using System.Collections.Generic;

namespace Model.Neg
{
    class ProfissaoNeg
    {
        private ProfissaoDao objProfissaoDao;

        public ProfissaoNeg()
        {
            objProfissaoDao = new ProfissaoDao();
        }

        public void create(Profissao objProfissao)
        {
            bool verificacao = true;
            int Id_Profissao = objProfissao.Id_Profissao;
            string Cim = objProfissao.Cim.ToString();
            string Aposentado = objProfissao.Aposentado;
            string Profissao1 = objProfissao.Profissao1;
            string Empresa_Orgao = objProfissao.Empresa_Orgao;
            string Cargo = objProfissao.Cargo;
            string Endereco = objProfissao.Endereco;
            string Bairro = objProfissao.Bairro;
            string Cidade = objProfissao.Cidade;
            string Uf = objProfissao.Uf;
            string Cep = objProfissao.Cep;
            string Email = objProfissao.Email;
            string Fone = objProfissao.Fone;
            string Fax = objProfissao.Fax;


            Profissao objProfissao1 = new Profissao();
            objProfissao1.Id_Profissao = objProfissao.Id_Profissao;
            verificacao = !objProfissaoDao.find(objProfissao1);
            if (!verificacao)
            {
                objProfissao.Estados = 9;
                return;
            }


            //se nao tem erro
            objProfissao.Estados = 99;
            objProfissaoDao.create(objProfissao);
            return;
        }
        public void update(Profissao objProfissao)
        {
            bool verificacao = true;
            //begin validar codigo retorna estado=1
            int Id_Profissao = objProfissao.Id_Profissao;
            string Cim = objProfissao.Cim.ToString();
            string Aposentado = objProfissao.Aposentado;
            string Profissao1 = objProfissao.Profissao1;
            string Empresa_Orgao = objProfissao.Empresa_Orgao;
            string Cargo = objProfissao.Cargo;
            string Endereco = objProfissao.Endereco;
            string Bairro = objProfissao.Bairro;
            string Cidade = objProfissao.Cidade;
            string Uf = objProfissao.Uf;
            string Cep = objProfissao.Cep;
            string Email = objProfissao.Email;
            string Fone = objProfissao.Fone;
            string Fax = objProfissao.Fax;


            // Inicia validacao

            /// ???????????

            //se nao tem erro
            objProfissao.Estados = 99;
            objProfissaoDao.update(objProfissao);
            return;
        }
        public void delete(Profissao objProfissao)
        {
            bool verificacao = true;
            //verificando se existe
            Profissao objProfissaoAux = new Profissao();
            objProfissaoAux.Id_Profissao = objProfissao.Id_Profissao;
            verificacao = objProfissaoDao.find(objProfissaoAux);
            if (!verificacao)
            {
                objProfissao.Estados = 33;
                return;
            }


            objProfissao.Estados = 99;
            objProfissaoDao.delete(objProfissao);
            return;
        }
        public bool find(Profissao objProfissao)
        {
            return objProfissaoDao.find(objProfissao);
        }
        public List<Profissao> findAll()
        {
            return objProfissaoDao.findAll();
        }
        public List<Profissao> findAllProfissao(Profissao objProfissao)
        {
            return objProfissaoDao.findAllProfissao(objProfissao);
        }
    }
}
