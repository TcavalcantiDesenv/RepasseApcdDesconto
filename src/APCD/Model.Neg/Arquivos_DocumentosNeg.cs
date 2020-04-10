using Model.Dao;
using Model.Entity;
using System.Collections.Generic;

namespace Model.Neg
{
    public class Arquivos_DocumentosNeg
    {
        private Arquivos_DocumentosDao objArquivos_DocumentosDao;

        public Arquivos_DocumentosNeg()
        {
            objArquivos_DocumentosDao = new Arquivos_DocumentosDao();
        }

        public void create(Arquivos_Documentos objArquivos_Documentos)
        {
            bool verificacao = true;
            int Id_Arquivos_Documentos = objArquivos_Documentos.Id_Arquivos_Documentos;
            string Tipo = objArquivos_Documentos.Tipo;
            string Titulo = objArquivos_Documentos.Titulo;
            string Nome = objArquivos_Documentos.Nome;
            string Autor = objArquivos_Documentos.Autor;
            string Dt = objArquivos_Documentos.Dt.ToString();
            string Descricao = objArquivos_Documentos.Descricao;

            Arquivos_Documentos objArquivos_Documentos1 = new Arquivos_Documentos();
            objArquivos_Documentos1.Id_Arquivos_Documentos = objArquivos_Documentos.Id_Arquivos_Documentos;
            verificacao = !objArquivos_DocumentosDao.find(objArquivos_Documentos1);
            if (!verificacao)
            {
                objArquivos_Documentos.Estados = 9;
                return;
            }


            //se nao tem erro
            objArquivos_Documentos.Estados = 99;
            objArquivos_DocumentosDao.create(objArquivos_Documentos);
            return;
        }
        public void update(Arquivos_Documentos objArquivos_Documentos)
        {
            bool verificacao = true;
            //begin validar codigo retorna estado=1
            int Id_Arquivos_Documentos = objArquivos_Documentos.Id_Arquivos_Documentos;
            string Tipo = objArquivos_Documentos.Tipo;
            string Titulo = objArquivos_Documentos.Titulo;
            string Nome = objArquivos_Documentos.Nome;
            string Autor = objArquivos_Documentos.Autor;
            string Dt = objArquivos_Documentos.Dt.ToString();
            string Descricao = objArquivos_Documentos.Descricao;


            // Inicia validacao

            /// ???????????

            //se nao tem erro
            objArquivos_Documentos.Estados = 99;
            objArquivos_DocumentosDao.update(objArquivos_Documentos);
            return;
        }
        public void delete(Arquivos_Documentos objArquivos_Documentos)
        {
            bool verificacao = true;
            //verificando se existe
            Arquivos_Documentos objArquivos_DocumentosAux = new Arquivos_Documentos();
            objArquivos_DocumentosAux.Id_Arquivos_Documentos = objArquivos_Documentos.Id_Arquivos_Documentos;
            verificacao = objArquivos_DocumentosDao.find(objArquivos_DocumentosAux);
            if (!verificacao)
            {
                objArquivos_Documentos.Estados = 33;
                return;
            }


            objArquivos_Documentos.Estados = 99;
            objArquivos_DocumentosDao.delete(objArquivos_Documentos);
            return;
        }
        public bool find(Arquivos_Documentos objArquivos_Documentos)
        {
            return objArquivos_DocumentosDao.find(objArquivos_Documentos);
        }
        public List<Arquivos_Documentos> findAll()
        {
            return objArquivos_DocumentosDao.findAll();
        }
        public List<Arquivos_Documentos> findAllArquivos_Documentos(Arquivos_Documentos objArquivos_Documentos)
        {
            return objArquivos_DocumentosDao.findAllArquivos_Documentos(objArquivos_Documentos);
        }
    }
}
