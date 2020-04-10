using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Model.Dao
{
    public class BalaustresDao : Obrigatorio<Balaustres>
    {
        private ConexaoDB objConexaoDB;
        private SqlCommand comando;
        private SqlDataReader reader;

        public BalaustresDao()
        {
            objConexaoDB = ConexaoDB.saberEstado();
        }

        public void create(Balaustres obj)
        {
            string create = "insert into Balaustres values('" + obj.Id_Macon + "','" + obj.Id_Loja + "','" + obj.Nome + "','" + obj.DataLoja + "')";
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

        public void delete(Balaustres obj)
        {
            string delete = "delete from Balaustres where Id_Macon, Id_Loja ='" + obj.Id_Macon + "'" + obj.Id_Loja + "'";
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

        public void update(Balaustres obj)
        {
            string update = "update Balaustres set Id_Loja='" + obj.Id_Loja + "',Nome='" + obj.Nome + "',DataLoja='" + obj.DataLoja + "'";
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

        public bool find(Balaustres obj)
        {
            bool temRegistros;
            string find = "select*from Balaustres where Id_Macon='" + obj.Id_Macon + "'";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                temRegistros = reader.Read();
                if (temRegistros)
                {
                    obj.Id_Loja =Convert.ToInt32( reader["Loja"].ToString());
                    obj.Id_Macon = Convert.ToInt32(reader["Id_Macon"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    obj.DataLoja = Convert.ToDateTime( reader["Data Loja"].ToString());

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

        List<Balaustres> Obrigatorio<Balaustres>.findAll()
        {
            List<Balaustres> listaBalaustres = new List<Balaustres>();
            string find = "select*from Balaustres order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Balaustres obj = new Balaustres();
                    obj.Id_Loja = Convert.ToInt32(reader["Loja"].ToString());
                    obj.Id_Macon = Convert.ToInt32(reader["Id_Macon"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    obj.DataLoja = Convert.ToDateTime(reader["Data Loja"].ToString());
                    listaBalaustres.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Balaustres obj2 = new Balaustres();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaBalaustres;
        }

        public List<Balaustres> findAll()
        {
            List<Balaustres> listaBalaustres = new List<Balaustres>();
            string find = "select*from Balaustres order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Balaustres obj = new Balaustres();
                    obj.Id_Loja = Convert.ToInt32(reader["Loja"].ToString());
                    obj.Id_Macon = Convert.ToInt32(reader["Id_Macon"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    obj.DataLoja = Convert.ToDateTime(reader["Data Loja"].ToString());
                    listaBalaustres.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Balaustres obj2 = new Balaustres();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaBalaustres;
        }

        public List<Balaustres> findAllBalaustres(Balaustres objBalaustres)
        {
            List<Balaustres> listaBalaustres = new List<Balaustres>();
            string findAll = "select* from Balaustres where nome like '%" + objBalaustres.Nome + "%' ";
            try
            {

                comando = new SqlCommand(findAll, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Balaustres obj = new Balaustres();
                    obj.Id_Loja = Convert.ToInt32(reader["Loja"].ToString());
                    obj.Id_Macon = Convert.ToInt32(reader["Id_Macon"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    obj.DataLoja = Convert.ToDateTime(reader["Data Loja"].ToString());
                    listaBalaustres.Add(obj);

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

            return listaBalaustres;

        }

        public List<Balaustres> findBalaustresPorId(Balaustres objBalaustres)
        {
            List<Balaustres> listaBalaustres = new List<Balaustres>();
            string findAll = "select * from Balaustres where Id_Loja = '" + objBalaustres.Id_Loja + "' and DataLoja = '" + objBalaustres.DataLoja + "' ";
            try
            {

                comando = new SqlCommand(findAll, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Balaustres obj = new Balaustres();
                    obj.Id_Loja = Convert.ToInt32(reader["Loja"].ToString());
                    obj.Id_Macon = Convert.ToInt32(reader["Id_Macon"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    obj.DataLoja = Convert.ToDateTime(reader["Data Loja"].ToString());
                    listaBalaustres.Add(obj);

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

            return listaBalaustres;

        }
    }
}
