using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class PresencaDao : Obrigatorio<Presenca>
    {
        private ConexaoDB objConexaoDB;
        private SqlCommand comando;
        private SqlDataReader reader;

        public PresencaDao()
        {
            objConexaoDB = ConexaoDB.saberEstado();
        }

        public void create(Presenca obj)
        {
            string create = "insert into Presenca(Nome,Data)  values('" + obj.Nome + "',convert(datetime,'" + obj.Data + "',105) )";

            try
            {
                comando = new SqlCommand(create, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }
        }

        public void delete(Presenca obj)
        {
            string delete = "delete from Presenca where Id ='" + obj.Id + "'";
            try
            {
                comando = new SqlCommand(delete, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }
        }

        public List<Presenca> BuscarTodos()
        {
            List<Presenca> listaMacon = new List<Presenca>();
            string find = "select*from Presenca order by Data,Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Presenca obj1 = new Presenca();
                    obj1.Id = Convert.ToInt32(reader["Id"].ToString());
                    obj1.Nome = reader["Nome"].ToString();
                    obj1.Data = Convert.ToDateTime(reader["Data"].ToString());

                    listaMacon.Add(obj1);
                }

            }
            catch (Exception ex)
            {
                var erro = ex.Message;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaMacon;
        }
        public List<Presenca> BuscarPorId(Presenca obj)
        {
            List<Presenca> listaMacon = new List<Presenca>();
            string find = "select*from Presenca where Id = '" + obj.Id + "' order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Presenca obj1 = new Presenca();
                    obj1.Id = Convert.ToInt32(reader["Id"].ToString());
                    obj1.Nome = reader["Nome"].ToString();
                    obj1.Data = Convert.ToDateTime(reader["Data"].ToString());

                    listaMacon.Add(obj1);
                }

            }
            catch (Exception ex)
            {
                var erro = ex.Message;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaMacon;
        }
        public List<Presenca> BuscarPorData(Presenca obj)
        {
            List<Presenca> listaMacon = new List<Presenca>();
            string find = "select*from Presenca where Data = convert(datetime,'" + obj.Data + "',105) order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Presenca obj1 = new Presenca();
                    obj1.Id = Convert.ToInt32(reader["Id"].ToString());
                    obj1.Nome = reader["Nome"].ToString();
                    obj1.Data = Convert.ToDateTime(reader["Data"].ToString());

                    listaMacon.Add(obj1);
                }

            }
            catch (Exception ex)
            {
                var erro = ex.Message;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaMacon;
        }

        public bool find(Presenca obj)
        {
            throw new NotImplementedException();
        }

        public List<Presenca> findAll()
        {
            throw new NotImplementedException();
        }

        public void update(Presenca obj)
        {
            throw new NotImplementedException();
        }
    }
}
