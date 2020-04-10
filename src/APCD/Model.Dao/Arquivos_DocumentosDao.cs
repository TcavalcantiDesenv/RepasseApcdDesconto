using Model.Entity;
using System;
using System.Collections.Generic; 
using System.Data.SqlClient;

namespace Model.Dao
{
    public class Arquivos_DocumentosDao : Obrigatorio<Arquivos_Documentos>
    {
        private ConexaoDB objConexaoDB;
        private SqlCommand comando;
        private SqlDataReader reader;

        public Arquivos_DocumentosDao()
        {
            objConexaoDB = ConexaoDB.saberEstado();
        }

        public void create(Arquivos_Documentos obj)
        {
            string create = "insert into Arquivos_Documentos values('" + obj.Id_Arquivos_Documentos + "','"+ obj.Tipo + "','" + obj.Titulo + "','" + obj.Nome + "','" + obj.Autor + "','" + obj.Dt + "','" + obj.Descricao + "')";
            try
            {
                comando = new SqlCommand(create, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                obj.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }
        }

        public void delete(Arquivos_Documentos obj)
        {
            string delete = "delete from Arquivos_Documentos where Id_Arquivos_Documentos ='" + obj.Id_Arquivos_Documentos + "'";
            try
            {
                comando = new SqlCommand(delete, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                obj.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }
        }

        public void update(Arquivos_Documentos obj)
        {
            string update = "update Arquivos_Documentos set Id_Arquivos_Documentos='" + obj.Id_Arquivos_Documentos + "',Tipo='" + obj.Tipo + "',Titulo='" + obj.Titulo + "',Nome='" + obj.Nome + "',Autor='" + obj.Autor + "',Dt='" + obj.Dt + "',Descricao='" + obj.Descricao + "'";
            try
            {
                comando = new SqlCommand(update, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {

                obj.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }
        }

        public bool find(Arquivos_Documentos obj)
        {
            bool temRegistros;
            string find = "select*from Arquivos_Documentos where Id_Arquivos_Documentos='" + obj.Id_Arquivos_Documentos + "'";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                temRegistros = reader.Read();
                if (temRegistros)
                {
                    obj.Id_Arquivos_Documentos = Convert.ToInt32(reader["Id Arquivo"].ToString());
                    obj.Tipo = reader["Tipo"].ToString();
                    obj.Titulo = reader["Título"].ToString();
                    obj.Nome = reader["Nome"].ToString();
                    obj.Autor = reader["Autor"].ToString();
                    obj.Dt = Convert.ToDateTime(reader["Data"].ToString());
                    obj.Descricao = reader["Descrição"].ToString();

                    obj.Estados = 99;
                }
                else
                {
                    obj.Estados = 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return temRegistros;
        }

        List<Arquivos_Documentos> Obrigatorio<Arquivos_Documentos>.findAll()
        {
            List<Arquivos_Documentos> listaArquivos_Documentos = new List<Arquivos_Documentos>();
            string find = "select*from Arquivos_Documentos order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Arquivos_Documentos obj = new Arquivos_Documentos();
                    obj.Id_Arquivos_Documentos = Convert.ToInt32(reader["Id Arquivo"].ToString());
                    obj.Tipo = reader["Tipo"].ToString();
                    obj.Titulo = reader["Título"].ToString();
                    obj.Nome = reader["Nome"].ToString();
                    obj.Autor = reader["Autor"].ToString();
                    obj.Dt = Convert.ToDateTime(reader["Data"].ToString());
                    obj.Descricao = reader["Descrição"].ToString();
                    listaArquivos_Documentos.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Arquivos_Documentos obj2 = new Arquivos_Documentos();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaArquivos_Documentos;
        }

        public List<Arquivos_Documentos> findAll()
        {
            List<Arquivos_Documentos> listaArquivos_Documentos = new List<Arquivos_Documentos>();
            string find = "select*from Arquivos_Documentos order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Arquivos_Documentos obj = new Arquivos_Documentos();
                    obj.Id_Arquivos_Documentos = Convert.ToInt32(reader["Id Arquivo"].ToString());
                    obj.Tipo = reader["Tipo"].ToString();
                    obj.Titulo = reader["Título"].ToString();
                    obj.Nome = reader["Nome"].ToString();
                    obj.Autor = reader["Autor"].ToString();
                    obj.Dt = Convert.ToDateTime(reader["Data"].ToString());
                    obj.Descricao = reader["Descrição"].ToString();
                    listaArquivos_Documentos.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Arquivos_Documentos obj2 = new Arquivos_Documentos();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaArquivos_Documentos;
        }

        public List<Arquivos_Documentos> findAllArquivos_Documentos(Arquivos_Documentos objArquivos_Documentos)
        {
            List<Arquivos_Documentos> listaArquivos_Documentos = new List<Arquivos_Documentos>();
            string findAll = "select* from Arquivos_Documentos where nome like '%" + objArquivos_Documentos.Tipo + "%' or Titulo like '%" + objArquivos_Documentos.Titulo + "%' or Nome like '%" + objArquivos_Documentos.Nome + "%' or Autor like '%" + objArquivos_Documentos.Autor + "%' or Descricao like '%" + objArquivos_Documentos.Descricao + "%' ";
            try
            {

                comando = new SqlCommand(findAll, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Arquivos_Documentos obj = new Arquivos_Documentos();
                    obj.Id_Arquivos_Documentos = Convert.ToInt32(reader["Id Arquivo"].ToString());
                    obj.Tipo = reader["Tipo"].ToString();
                    obj.Titulo = reader["Título"].ToString();
                    obj.Nome = reader["Nome"].ToString();
                    obj.Autor = reader["Autor"].ToString();
                    obj.Dt = Convert.ToDateTime(reader["Data"].ToString());
                    obj.Descricao = reader["Descrição"].ToString();
                    listaArquivos_Documentos.Add(obj);

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaArquivos_Documentos;

        }

        public List<Arquivos_Documentos> findArquivos_DocumentosPorId(Arquivos_Documentos objArquivos_Documentos)
        {
            List<Arquivos_Documentos> listaArquivos_Documentos = new List<Arquivos_Documentos>();
            string findAll = "select * from Arquivos_Documentos where Id_Arquivos_Documentos = '" + objArquivos_Documentos.Id_Arquivos_Documentos + "' ";
            try
            {

                comando = new SqlCommand(findAll, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Arquivos_Documentos obj = new Arquivos_Documentos();
                    obj.Id_Arquivos_Documentos = Convert.ToInt32(reader["Id Arquivo"].ToString());
                    obj.Tipo = reader["Tipo"].ToString();
                    obj.Titulo = reader["Título"].ToString();
                    obj.Nome = reader["Nome"].ToString();
                    obj.Autor = reader["Autor"].ToString();
                    obj.Dt = Convert.ToDateTime(reader["Data"].ToString());
                    obj.Descricao = reader["Descrição"].ToString();
                    listaArquivos_Documentos.Add(obj);

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaArquivos_Documentos;

        }
    }

}
