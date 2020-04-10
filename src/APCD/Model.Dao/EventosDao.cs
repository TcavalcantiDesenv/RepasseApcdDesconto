using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Model.Dao
{
    public class EventosDao : Obrigatorio<Eventos>
    {
        private ConexaoDB objConexaoDB;
        private SqlCommand comando;
        private SqlDataReader reader;

        public EventosDao()
        {
            objConexaoDB = ConexaoDB.saberEstado();
        }

        public void create(Eventos obj)
        {
            int fully = 0;
            if (obj.IsFullDay == true) fully = 1;
            string create = "insert into Eventos values('" + obj.Id_Evento + "','" + obj.Id_Loja + "','" + obj.Dt_Inicio + "','" + obj.Dt_Fim + "','" + obj.Tipo_Evento + "'," +
                "'" + obj.Titulo + "','" + obj.Tipo_Exibicao + "','" + obj.Subject + "','" + obj.Description + "','" + obj.ThemeColor + "','" + fully + "', convert(date, '" + obj?.Start + "', 103), convert(date, '" + obj?.Fim + "', 103) )";
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

        public void delete(Eventos obj)
        {
            string delete = "delete from Eventos where Id_Evento ='" + obj.Id_Evento + "'";
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

        public void update(Eventos obj)
        {
            string update = "";
            string dt = obj?.Fim.ToString();
            if (dt == "01/01/0001 00:00:00") dt = "null";
            if (dt == "null")
            {
                update = "update Eventos set Id_Loja='" + obj.Id_Loja + "',Dt_Inicio='" + obj.Dt_Inicio + "',Dt_Fim='" + obj.Dt_Fim +

           "',Subject='" + obj.Subject + "',Description='" + obj.Description + "',Start=convert(date, '" + obj?.Start + "', 103),Fim=convert(date, null, 103),ThemeColor='" + obj.ThemeColor + "',IsFullDay='" + obj.IsFullDay + "'    ";
            }
            else
            {
                update = "update Eventos set Id_Loja='" + obj.Id_Loja + "',Dt_Inicio='" + obj.Dt_Inicio + "',Dt_Fim='" + obj.Dt_Fim +

           "',Subject='" + obj.Subject + "',Description='" + obj.Description + "',Start=convert(date, '" + obj?.Start + "', 103),Fim=convert(date, '" + dt + "', 103),ThemeColor='" + obj.ThemeColor + "',IsFullDay='" + obj.IsFullDay + "'    ";
            }
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

        public bool find(Eventos obj)
        {
            bool temRegistros;
            string find = "select*from Eventos where Id_Evento='" + obj.Id_Evento + "'";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                temRegistros = reader.Read();
                if (temRegistros)
                {
                    obj.Id_Evento = Convert.ToInt32(reader["Id_Evento"].ToString());
                    obj.Id_Loja = Convert.ToInt32(reader["Id_Loja"].ToString());
                    obj.Dt_Inicio = Convert.ToDateTime(reader["Dt_Inicio"].ToString());
                    obj.Dt_Fim = Convert.ToDateTime(reader["Dt_Fim"].ToString());
                    obj.Tipo_Evento = reader["Tipo_Evento"].ToString();
                    obj.Titulo = reader["Titulo"].ToString();
                    obj.Tipo_Exibicao = reader["Tipo_Exibicao"].ToString();

                    obj.Subject = reader["Subject"].ToString();
                    obj.Description = reader["Description"].ToString();
                    obj.Start = Convert.ToDateTime(reader["Start"].ToString());
                    obj.Fim = Convert.ToDateTime(reader["Fim"].ToString());
                    obj.ThemeColor = reader["ThemeColor"].ToString();
                    if (reader["IsFullDay"].ToString() == "1") obj.IsFullDay = true;
                    if (reader["IsFullDay"].ToString() == "0") obj.IsFullDay = false;
                    //       obj.IsFullDay = Convert.ToBoolean(reader["IsFullDay"].ToString());

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

        List<Eventos> Obrigatorio<Eventos>.findAll()
        {
            List<Eventos> listaEventos = new List<Eventos>();
            string find = "select*from Eventos order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Eventos obj = new Eventos();
                    obj.Id_Evento = Convert.ToInt32(reader["Id Evento"].ToString());
                    obj.Id_Loja = Convert.ToInt32(reader["Id_Loja"].ToString());
                    obj.Dt_Inicio = Convert.ToDateTime(reader["Dt_Inicio"].ToString());
                    obj.Dt_Fim = Convert.ToDateTime(reader["Dt_Fim"].ToString());
                    obj.Tipo_Evento = reader["Tipo_Evento"].ToString();
                    obj.Titulo = reader["Titulo"].ToString();
                    obj.Tipo_Exibicao = reader["Tipo_Exibicao"].ToString();

                    obj.Subject = reader["Subject"].ToString();
                    obj.Description = reader["Description"].ToString();
                    obj.Start = Convert.ToDateTime(reader["Start"].ToString());
                    obj.Fim = Convert.ToDateTime(reader["Fim"].ToString());
                    obj.ThemeColor = reader["ThemeColor"].ToString();
                    obj.IsFullDay = Convert.ToBoolean(reader["IsFullDay"].ToString());

                    listaEventos.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Eventos obj2 = new Eventos();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaEventos;
        }

        public List<Eventos> findAll()
        {
            List<Eventos> listaEventos = new List<Eventos>();
            string find = "select*from Eventos ";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Eventos obj = new Eventos();
                    obj.Id_Evento = Convert.ToInt32(reader["Id_Evento"].ToString());
                    obj.Id_Loja = Convert.ToInt32(reader["Id_Loja"].ToString());
                    obj.Dt_Inicio = Convert.ToDateTime(reader["Dt_Inicio"].ToString());
                    obj.Dt_Fim = Convert.ToDateTime(reader["Dt_Fim"].ToString());
                    obj.Tipo_Evento = reader["Tipo_Evento"].ToString();
                    obj.Titulo = reader["Titulo"].ToString();
                    obj.Tipo_Exibicao = reader["Tipo_Exibicao"].ToString();

                    obj.Subject = reader["Subject"].ToString();
                    obj.Description = reader["Description"].ToString();
                    obj.Start = Convert.ToDateTime(reader["Start"].ToString());
                    if (reader["Fim"].ToString() != "")  obj.Fim = Convert.ToDateTime(reader["Fim"].ToString());
               //     obj.Fim = Convert.ToDateTime(reader["Fim"].ToString());
                    obj.ThemeColor = reader["ThemeColor"].ToString();
                    obj.IsFullDay = Convert.ToBoolean(reader["IsFullDay"].ToString());

                    listaEventos.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Eventos obj2 = new Eventos();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaEventos;
        }

        public List<Eventos> findAllEventos(Eventos objEventos)
        {
            List<Eventos> listaEventos = new List<Eventos>();
            string findAll = "select* from Eventos where Tipo_Evento like '%" + objEventos.Tipo_Evento + "%' or Titulo like '%" + objEventos.Titulo + "%' or Tipo_Exibicao like '%" + objEventos.Tipo_Exibicao + "%' ";
            try
            {

                comando = new SqlCommand(findAll, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Eventos obj = new Eventos();
                    obj.Id_Evento = Convert.ToInt32(reader["Id Evento"].ToString());
                    obj.Id_Loja = Convert.ToInt32(reader["Id_Loja"].ToString());
                    obj.Dt_Inicio = Convert.ToDateTime(reader["Dt_Inicio"].ToString());
                    obj.Dt_Fim = Convert.ToDateTime(reader["Dt_Fim"].ToString());
                    obj.Tipo_Evento = reader["Tipo_Evento"].ToString();
                    obj.Titulo = reader["Titulo"].ToString();
                    obj.Tipo_Exibicao = reader["Tipo_Exibicao"].ToString();

                    obj.Subject = reader["Subject"].ToString();
                    obj.Description = reader["Description"].ToString();
                    obj.Start = Convert.ToDateTime(reader["Start"].ToString());
                    obj.Fim = Convert.ToDateTime(reader["Fim"].ToString());
                    obj.ThemeColor = reader["ThemeColor"].ToString();
                    obj.IsFullDay = Convert.ToBoolean(reader["IsFullDay"].ToString());

                    listaEventos.Add(obj);

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

            return listaEventos;

        }

        public List<Eventos> findEventosPorId(Eventos objEventos)
        {
            List<Eventos> listaEventos = new List<Eventos>();
            string findAll = "select * from Eventos where Id_Evento = '" + objEventos.Id_Evento + "' ";
            try
            {

                comando = new SqlCommand(findAll, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Eventos obj = new Eventos();
                    obj.Id_Evento = Convert.ToInt32(reader["Id Evento"].ToString());
                    obj.Id_Loja = Convert.ToInt32(reader["Id_Loja"].ToString());
                    obj.Dt_Inicio = Convert.ToDateTime(reader["Dt_Inicio"].ToString());
                    obj.Dt_Fim = Convert.ToDateTime(reader["Dt_Fim"].ToString());
                    obj.Tipo_Evento = reader["Tipo_Evento"].ToString();
                    obj.Titulo = reader["Titulo"].ToString();
                    obj.Tipo_Exibicao = reader["Tipo_Exibicao"].ToString();

                    obj.Subject = reader["Subject"].ToString();
                    obj.Description = reader["Description"].ToString();
                    obj.Start = Convert.ToDateTime(reader["Start"].ToString());
                    obj.Fim = Convert.ToDateTime(reader["Fim"].ToString());
                    obj.ThemeColor = reader["ThemeColor"].ToString();
                    obj.IsFullDay = Convert.ToBoolean(reader["IsFullDay"].ToString());

                    listaEventos.Add(obj);

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

            return listaEventos;

        }
    }
}
