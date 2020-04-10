using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Model.Dao
{
    public class AcessosDao : Obrigatorio<Acessos>
    {
        private ConexaoDB objConexaoDB;
        private SqlCommand comando;
        private SqlDataReader reader;

        public AcessosDao()
        {
            objConexaoDB = ConexaoDB.saberEstado();
        }
        
        public bool find(Acessos obj)
        {
            bool temRegistros;
            string find = "select * from Acessos where IdAcesso='" + obj.IdAcesso + "'";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                temRegistros = reader.Read();
                if (temRegistros)
                {
                    obj.IdAcesso = Convert.ToInt32(reader["IdAcesso"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    obj.DataEntrada = Convert.ToDateTime(reader["DataEntrada"].ToString());
                    obj.DataSaida = Convert.ToDateTime(reader["DataSaida"].ToString());
                    obj.Empresa = reader["Empresa"].ToString();
                    obj.IP = reader["IP"].ToString();
                    obj.Usuario = reader["Usuario"].ToString();
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
        public List<Acessos> buscarAcessosPorUsuario(Acessos obj)
        {
            List<Acessos> listaClientes = new List<Acessos>();
            string find = "select * from Acessos where Usuario='" + obj.Usuario + "'"; 
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Acessos obj1 = new Acessos();
                    obj1.IdAcesso = Convert.ToInt32(reader["IdAcesso"].ToString());
                    obj1.Nome = reader["Nome"].ToString();
                    obj1.DataEntrada = Convert.ToDateTime(reader["DataEntrada"].ToString());
                    obj1.DataSaida = Convert.ToDateTime(reader["DataSaida"].ToString());
                    obj1.Empresa = reader["Empresa"].ToString();
                    obj1.IP = reader["IP"].ToString();
                    obj1.Usuario = reader["Usuario"].ToString();
                    obj1.Estados = 99;
                    listaClientes.Add(obj1);
                }

            }
            catch (Exception ex)
            {
                Acessos obj2 = new Acessos();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaClientes;
        }

        public int buscarUltimoID()
        {
            int ID = 0;
            List<Acessos> listaClientes = new List<Acessos>();
            string find = "SELECT MAX(IdAcesso) as ID FROM Acessos";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                   ID = Convert.ToInt32(reader["ID"].ToString());
                }

            }
            catch (Exception ex)
            {
                Acessos obj2 = new Acessos();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return ID;
        }

        public List<Acessos> findAll()
        {
            List<Acessos> listaClientes = new List<Acessos>();
            string find = "select top 10 * from Acessos order by DataEntrada desc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? newdate = null;
                    Acessos obj = new Acessos();
                    obj.IdAcesso = Convert.ToInt32(reader["IdAcesso"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    obj.DataEntrada = Convert.ToDateTime(reader["DataEntrada"].ToString());
                    try
                    {
                        obj.DataSaida = Convert.ToDateTime(reader["DataSaida"].ToString());
                    }
                    catch { }
                    obj.Empresa = reader["Empresa"].ToString();
                    obj.IP = reader["IP"].ToString();
                    obj.Usuario = reader["Usuario"].ToString();
                    obj.Estados = 99;
                    listaClientes.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Acessos obj2 = new Acessos();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaClientes;
        }

        public void AtualizaSaida(string id)
        {
            var saida = DateTime.Now;
            string update = "update Acessos set DataSaida = convert(datetime, '" + saida + "', 103) where IdAcesso = '"+id+"' ";
            try
            {
                comando = new SqlCommand(update, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {

                //obj.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }
        }
        public void create(Acessos obj)
        {
            string create = @"insert Acessos(Usuario,Nome,Empresa,IP,DataEntrada) values('" + obj?.Usuario + "','" + obj?.Nome + "','" + obj?.Empresa +
                "','" + obj?.IP +
                "', convert(datetime, '" + obj?.DataEntrada + "', 103)" + ")";
            try
            {
                comando = new SqlCommand(create, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                obj.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }
        }
        public void delete(Acessos obj)
        {
            string delete = "delete from Acessos where IdAcesso ='" + obj.IdAcesso + "'";
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
        public void deleteTodos()
        {
            string delete = "delete from Acessos ";
            try
            {
                comando = new SqlCommand(delete, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }
        }

        List<Acessos> Obrigatorio<Acessos>.findAll()
        {
            throw new System.NotImplementedException();
        }

        public void update(Acessos obj)
        {
            throw new NotImplementedException();
        }
    }
}
