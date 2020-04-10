using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Model.Dao
{
    public class FamiliarDao : Obrigatorio<Familiar>
    {
        private ConexaoDB objConexaoDB;
        private SqlCommand comando;
        private SqlDataReader reader;

        public FamiliarDao()
        {
            objConexaoDB = ConexaoDB.saberEstado();
        }

        public void create(Familiar obj)
        {
            string create = "insert into Familiar(aniversario,Grau,Nome,Id_Loja,Id_Macon,Sexo,email) values(convert(datetime,'" + obj.Aniversario.ToString("yyyy-MM-dd") + "',120),'" + obj.Grau + "','" + obj.Nome + "','" + obj.Id_Loja + "','" + obj.Id_Macon + "','" + obj.Sexo + "','" + obj.Email + "')";
            try
            {
                comando = new SqlCommand(create, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
             //   obj.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }
        }

        public void delete(Familiar obj)
        {
            string delete = "delete from Familiar where Id_Familiar =" + obj.Id_Familiar + "";
            try
            {
                comando = new SqlCommand(delete, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var erro = ex.Message; //   obj.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }
        }

        public void update(Familiar obj)
        {
            string update = "update Familiar set Id_Familiar='" + obj.Id_Familiar + "',Cim='" + obj.Cim + "',Nome='" + obj.Nome + "',Grau='" + obj.Grau + "'";
            try
            {
                comando = new SqlCommand(update, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {

            //    obj.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }
        }

        public bool find(Familiar obj)
        {
            bool temRegistros;
            string find = "select*from Familiar where Id_Familiar='" + obj.Id_Familiar + "'";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                temRegistros = reader.Read();
                if (temRegistros)
                {
                    obj.Id_Familiar = Convert.ToInt32(reader["Id Arquivo"].ToString());
                //    obj.Cim = Convert.ToInt32(reader["Cim"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    obj.Grau = reader["Grau"].ToString();

                //    obj.Estados = 99;
                }
                else
                {
              //      obj.Estados = 1;
                }
            }
            catch (Exception ex)
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

        List<Familiar> Obrigatorio<Familiar>.findAll()
        {
            List<Familiar> listaFamiliar = new List<Familiar>();
            string find = "select*from Familiar order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Familiar obj = new Familiar();
                    obj.Id_Familiar = Convert.ToInt32(reader["Id Arquivo"].ToString());
                    obj.Cim = Convert.ToInt32(reader["Cim"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    obj.Grau = reader["Grau"].ToString();
                    listaFamiliar.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Familiar obj2 = new Familiar();
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaFamiliar;
        }

        public List<Familiar> findAll()
        {
            List<Familiar> listaFamiliar = new List<Familiar>();
            string find = "select*from Familiar order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Familiar obj = new Familiar();
                    obj.Id_Loja = Convert.ToInt32(reader["Id_Loja"].ToString());
                    obj.Id_Macon = reader["Id_Macon"].ToString();
                    obj.Id_Familiar = Convert.ToInt32(reader["Id_familiar"].ToString());
                    obj.Sexo = reader["Sexo"].ToString();
                    obj.Email = reader["Email"].ToString();
                    obj.Nome = reader["Nome"].ToString();
                    obj.Grau = reader["Grau"].ToString();
                    obj.Aniversario = Convert.ToDateTime(reader["Aniversario"]);
                    listaFamiliar.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Familiar obj2 = new Familiar();
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaFamiliar;
        }

        public List<Familiar> findAllFamiliar(Familiar objFamiliar)
        {
            List<Familiar> listaFamiliar = new List<Familiar>();
            string findAll = "select* from Familiar where nome like '%" + objFamiliar.Nome + "%' or Grau like '%" + objFamiliar.Grau + "%' ";
            try
            {

                comando = new SqlCommand(findAll, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Familiar obj = new Familiar();
                    obj.Id_Familiar = Convert.ToInt32(reader["Id Arquivo"].ToString());
                    obj.Cim = Convert.ToInt32(reader["Cim"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    obj.Grau = reader["Grau"].ToString();
                    listaFamiliar.Add(obj);

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

            return listaFamiliar;

        }

        public List<Familiar> findFamiliarPorId(Familiar objFamiliar)
        {
            List<Familiar> listaFamiliar = new List<Familiar>();
            string findAll = "select * from Familiar where Id_Familiar = '" + objFamiliar.Id_Familiar + "' ";
            try
            {

                comando = new SqlCommand(findAll, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Familiar obj = new Familiar();
                    obj.Id_Familiar = Convert.ToInt32(reader["Id Arquivo"].ToString());
                    obj.Cim = Convert.ToInt32(reader["Cim"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    obj.Grau = reader["Grau"].ToString();
                    listaFamiliar.Add(obj);

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

            return listaFamiliar;

        }
    }
}
