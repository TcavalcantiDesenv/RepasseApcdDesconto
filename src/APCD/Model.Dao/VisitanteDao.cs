using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Loja;

namespace Model.Dao
{
    public class VisitanteDao : Obrigatorio<Visitante>
    {
        private ConexaoDB objConexaoDB;
        private SqlCommand comando;
        private SqlDataReader reader;

        public VisitanteDao()
        {
            objConexaoDB = ConexaoDB.saberEstado();
        }

        public void create(Visitante obj)
        {
            string create = "insert into Visitante values('" + obj.Id_Visitante + "','" + obj.Id_Loja + "','" + obj.Cim + "','" + obj.Obediencia + "','" + obj.Dt_Visita + "')";
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

        public void delete(Visitante obj)
        {
            string delete = "delete from Visitante where Id_Visitante ='" + obj.Id_Visitante + "'";
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

        public void update(Visitante obj)
        {
            string update = "update Visitante set Id_Visitante='" + obj.Id_Visitante + "',Id_Loja='" + obj.Id_Loja + "',Cim='" + obj.Cim + "',Obediencia='" + obj.Obediencia + "',Dt_Visita='" + obj.Dt_Visita + "'";
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

        public bool find(Visitante obj)
        {
            bool temRegistros;
            string find = "select*from Visitante where Id_Visitante='" + obj.Id_Visitante + "'";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                temRegistros = reader.Read();
                if (temRegistros)
                {
                    obj.Id_Visitante = Convert.ToInt32(reader["Id Visitante"].ToString());
                    obj.Id_Loja = Convert.ToInt32(reader["Id Loja"].ToString());
                    obj.Cim = Convert.ToInt32(reader["Cim"].ToString());
                    obj.Obediencia = reader["Obediencia"].ToString();
                    obj.Dt_Visita =Convert.ToDateTime( reader["Dt_Visita"].ToString());

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

        List<Visitante> Obrigatorio<Visitante>.findAll()
        {
            List<Visitante> listaVisitante = new List<Visitante>();
            string find = "select*from Visitante order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Visitante obj = new Visitante();
                    obj.Id_Visitante = Convert.ToInt32(reader["Id Visitante"].ToString());
                    obj.Id_Loja = Convert.ToInt32(reader["Id Loja"].ToString());
                    obj.Cim = Convert.ToInt32(reader["Cim"].ToString());
                    obj.Obediencia = reader["Obediencia"].ToString();
                    obj.Dt_Visita = Convert.ToDateTime(reader["Dt_Visita"].ToString());
                    listaVisitante.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Visitante obj2 = new Visitante();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaVisitante;
        }

        public List<Visitante> findAll()
        {
            List<Visitante> listaVisitante = new List<Visitante>();
            string find = "select*from Visitante order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Visitante obj = new Visitante();
                    obj.Id_Visitante = Convert.ToInt32(reader["Id Visitante"].ToString());
                    obj.Id_Loja = Convert.ToInt32(reader["Id Loja"].ToString());
                    obj.Cim = Convert.ToInt32(reader["Cim"].ToString());
                    obj.Obediencia = reader["Obediencia"].ToString();
                    obj.Dt_Visita = Convert.ToDateTime(reader["Dt_Visita"].ToString());
                    listaVisitante.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Visitante obj2 = new Visitante();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaVisitante;
        }

        public List<Visitante> findAllVisitante(Visitante objVisitante)
        {
            List<Visitante> listaVisitante = new List<Visitante>();
            string findAll = "select* from Visitante where nome like '%" + objVisitante.Obediencia + "%' ";
            try
            {

                comando = new SqlCommand(findAll, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Visitante obj = new Visitante();
                    obj.Id_Visitante = Convert.ToInt32(reader["Id Visitante"].ToString());
                    obj.Id_Loja = Convert.ToInt32(reader["Id Loja"].ToString());
                    obj.Cim = Convert.ToInt32(reader["Cim"].ToString());
                    obj.Obediencia = reader["Obediencia"].ToString();
                    obj.Dt_Visita = Convert.ToDateTime(reader["Dt_Visita"].ToString());
                    listaVisitante.Add(obj);

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

            return listaVisitante;

        }

        public List<Visitante> findVisitantePorId(Visitante objVisitante)
        {
            List<Visitante> listaVisitante = new List<Visitante>();
            string findAll = "select * from Visitante where Id_Visitante = '" + objVisitante.Id_Visitante + "' ";
            try
            {

                comando = new SqlCommand(findAll, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Visitante obj = new Visitante();
                    obj.Id_Visitante = Convert.ToInt32(reader["Id Visitante"].ToString());
                    obj.Id_Loja = Convert.ToInt32(reader["Id Loja"].ToString());
                    obj.Cim = Convert.ToInt32(reader["Cim"].ToString());
                    obj.Obediencia = reader["Obediencia"].ToString();
                    obj.Dt_Visita = Convert.ToDateTime(reader["Dt_Visita"].ToString());
                    listaVisitante.Add(obj);

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

            return listaVisitante;

        }

    }
}
