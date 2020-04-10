using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model.Entity;

namespace Model.Dao
{
    public class GraduacaoDao : Obrigatorio<Graduacao>
    {
        private ConexaoDB objConexaoDB;
        private SqlCommand comando;
        private SqlDataReader reader;

        public GraduacaoDao()
        {
            objConexaoDB = ConexaoDB.saberEstado();
        }

        public void create(Graduacao obj)
        {
            string create = "insert into Graduacao values('" + obj.Id_Graduacao + "','" + obj.Descricao + "','" + obj.Numero + "','" + obj.Sigla + "','" + obj.Meses_Intersticio + "','" + obj.Dt_Inicial + "','" + obj.Dt_Final + "')";
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

        public void delete(Graduacao obj)
        {
            string delete = "delete from Graduacao where Id_Graduacao ='" + obj.Id_Graduacao + "'";
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

        public void update(Graduacao obj)
        {
            string update = "update Graduacao set Id_Graduacao='" + obj.Id_Graduacao + "',Descricao='" + obj.Descricao + "',Numero='" + obj.Numero + "',Sigla='" + obj.Sigla + "',Meses_Intersticio='" + obj.Meses_Intersticio + "',Dt_Inicial='" + obj.Dt_Inicial + "',Dt_Final='" + obj.Dt_Final + "'";
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

        public bool find(Graduacao obj)
        {
            bool temRegistros;
            string find = "select*from Graduacao where Id_Graduacao='" + obj.Id_Graduacao + "'";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                temRegistros = reader.Read();
                if (temRegistros)
                {
                    obj.Id_Graduacao = Convert.ToInt32(reader["Id Graduação"].ToString());
                    obj.Descricao = reader["Descrição"].ToString();
                    obj.Numero = Convert.ToInt32(reader["Número"].ToString());
                    obj.Sigla = reader["Sigla"].ToString();
                    obj.Meses_Intersticio = reader["Meses_Interstício"].ToString();
                    obj.Dt_Inicial = Convert.ToDateTime(reader["Dt_Inicial"].ToString());
                    obj.Dt_Final = Convert.ToDateTime(reader["Dt_Final"].ToString());

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

        List<Graduacao> Obrigatorio<Graduacao>.findAll()
        {
            List<Graduacao> listaGraduacao = new List<Graduacao>();
            string find = "select*from Graduacao order by Descricao asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Graduacao obj = new Graduacao();
                    obj.Id_Graduacao = Convert.ToInt32(reader["Id Graduação"].ToString());
                    obj.Descricao = reader["Descrição"].ToString();
                    obj.Numero = Convert.ToInt32(reader["Número"].ToString());
                    obj.Sigla = reader["Sigla"].ToString();
                    obj.Meses_Intersticio = reader["Meses_Interstício"].ToString();
                    obj.Dt_Inicial = Convert.ToDateTime(reader["Dt_Inicial"].ToString());
                    obj.Dt_Final = Convert.ToDateTime(reader["Dt_Final"].ToString());
                    listaGraduacao.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Graduacao obj2 = new Graduacao();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaGraduacao;
        }

        public List<Graduacao> findAll()
        {
            List<Graduacao> listaGraduacao = new List<Graduacao>();
            string find = "select*from Graduacao order by Descricao asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Graduacao obj = new Graduacao();
                    obj.Id_Graduacao = Convert.ToInt32(reader["Id_Graduacao"].ToString());
                    obj.Descricao = reader["Descricao"].ToString();
                    obj.Numero = Convert.ToInt32(reader["Numero"].ToString());
                    obj.Sigla = reader["Sigla"].ToString();
                    obj.Meses_Intersticio = reader["Meses_Intersticio"].ToString();
                    //obj.Dt_Inicial = Convert.ToDateTime(reader["Dt_Inicial"].ToString());
                    //obj.Dt_Final = Convert.ToDateTime(reader["Dt_Final"].ToString());
                    listaGraduacao.Add(obj);
                }

            }
            catch (Exception ex)
            {
                //Graduacao obj2 = new Graduacao();
                //obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaGraduacao;
        }
        public List<Parentesco> Listar()
        {
            List<Parentesco> listaGraduacao = new List<Parentesco>();
            string find = "select*from Parentesco order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Parentesco obj1 = new Parentesco();
                    obj1.IdParentesco = Convert.ToInt32(reader["IdParentesco"].ToString());
                    obj1.Nome = reader["Nome"].ToString();
                    listaGraduacao.Add(obj1);
                }

            }
            catch (Exception ex)
            {
                //Graduacao obj2 = new Graduacao();
                //obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaGraduacao;
        }

        public List<Graduacao> findAllGraduacao(Graduacao objGraduacao)
        {
            List<Graduacao> listaGraduacao = new List<Graduacao>();
            string findAll = "select* from Graduacao where Descricao like '%" + objGraduacao.Descricao + "%' or Sigla like '%" + objGraduacao.Sigla + "%' or Meses_Intersticio like '%" + objGraduacao.Meses_Intersticio + "%' ";
            try
            {

                comando = new SqlCommand(findAll, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Graduacao obj = new Graduacao();
                    obj.Id_Graduacao = Convert.ToInt32(reader["Id Graduação"].ToString());
                    obj.Descricao = reader["Descrição"].ToString();
                    obj.Numero = Convert.ToInt32(reader["Número"].ToString());
                    obj.Sigla = reader["Sigla"].ToString();
                    obj.Meses_Intersticio = reader["Meses_Interstício"].ToString();
                    obj.Dt_Inicial = Convert.ToDateTime(reader["Dt_Inicial"].ToString());
                    obj.Dt_Final = Convert.ToDateTime(reader["Dt_Final"].ToString());
                    listaGraduacao.Add(obj);

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

            return listaGraduacao;

        }

        public List<Graduacao> findGraduacaoPorId(Graduacao objGraduacao)
        {
            List<Graduacao> listaGraduacao = new List<Graduacao>();
            string findAll = "select * from Graduacao where Id_Graduacao = '" + objGraduacao.Id_Graduacao + "' ";
            try
            {

                comando = new SqlCommand(findAll, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Graduacao obj = new Graduacao();
                    obj.Id_Graduacao = Convert.ToInt32(reader["Id Graduação"].ToString());
                    obj.Descricao = reader["Descrição"].ToString();
                    obj.Numero = Convert.ToInt32(reader["Número"].ToString());
                    obj.Sigla = reader["Sigla"].ToString();
                    obj.Meses_Intersticio = reader["Meses_Interstício"].ToString();
                    obj.Dt_Inicial = Convert.ToDateTime(reader["Dt_Inicial"].ToString());
                    obj.Dt_Final = Convert.ToDateTime(reader["Dt_Final"].ToString());
                    listaGraduacao.Add(obj);

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

            return listaGraduacao;

        }

    }
}
