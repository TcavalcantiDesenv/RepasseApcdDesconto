using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model.Entity;

namespace Model.Dao
{
    public class ProfissaoDao : Obrigatorio<Profissao>
    {
        private ConexaoDB objConexaoDB;
        private SqlCommand comando;
        private SqlDataReader reader;

        public ProfissaoDao()
        {
            objConexaoDB = ConexaoDB.saberEstado();
        }

        public void create(Profissao obj)
        {
            string create = "insert into Profissao values('" + obj.Id_Profissao + "','" + obj.Cim + "','" + obj.Aposentado + "','" + obj.Profissao1 + "','" + obj.Empresa_Orgao + "','" + obj.Cargo + "','" + obj.Endereco + "','" + obj.Bairro + "','" + obj.Cidade + "','" + obj.Uf + "','" + obj.Cep + "','" + obj.Email + "','" + obj.Fone + "','" + obj.Fax + "')";
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

        public void delete(Profissao obj)
        {
            string delete = "delete from Profissao where Id_Profissao ='" + obj.Id_Profissao + "'";
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

        public void update(Profissao obj)
        {
            string update = "update Profissao set Id_Profissao='" + obj.Id_Profissao + "',Cim='" + obj.Cim + "',Aposentado='" + obj.Aposentado + "',Profissao1='" + obj.Profissao1 + "',Empresa_Orgao='" + obj.Empresa_Orgao + "',Cargo='" + obj.Cargo + "',Endereco='" + obj.Endereco + "',Bairro='" + obj.Bairro + "',Cidade='" + obj.Cidade + "',Uf='" + obj.Uf + "',Cep='" + obj.Cep + "',Email='" + obj.Email + "',Fone='" + obj.Fone + "',Fax='" + obj.Fax + "'";
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

        public bool find(Profissao obj)
        {
            bool temRegistros;
            string find = "select*from Profissao where Id_Profissao='" + obj.Id_Profissao + "'";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                temRegistros = reader.Read();
                if (temRegistros)
                {
                    obj.Id_Profissao = Convert.ToInt32(reader["Id_Profissão"].ToString());
                    obj.Cim = Convert.ToInt32(reader["Cim"].ToString());
                    obj.Aposentado = reader["Aposentado"].ToString();
                    obj.Profissao1 = reader["Profissão"].ToString();
                    obj.Empresa_Orgao = reader["Empresa_Órgão"].ToString();
                    obj.Cargo = reader["Cargo"].ToString();
                    obj.Endereco = reader["Endereço"].ToString();
                    obj.Bairro = reader["Bairro"].ToString();
                    obj.Cidade = reader["Cidade"].ToString();
                    obj.Uf = reader["Uf"].ToString();
                    obj.Cep = reader["Cep"].ToString();
                    obj.Email = reader["Email"].ToString();
                    obj.Fone = reader["Fone"].ToString();
                    obj.Fax = reader["Fax"].ToString();


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

        List<Profissao> Obrigatorio<Profissao>.findAll()
        {
            List<Profissao> listaProfissao = new List<Profissao>();
            string find = "select*from Profissao order by Profissao1 asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Profissao obj = new Profissao();
                    obj.Id_Profissao = Convert.ToInt32(reader["Id_Profissão"].ToString());
                    obj.Cim = Convert.ToInt32(reader["Cim"].ToString());
                    obj.Aposentado = reader["Aposentado"].ToString();
                    obj.Profissao1 = reader["Profissão"].ToString();
                    obj.Empresa_Orgao = reader["Empresa_Órgão"].ToString();
                    obj.Cargo = reader["Cargo"].ToString();
                    obj.Endereco = reader["Endereço"].ToString();
                    obj.Bairro = reader["Bairro"].ToString();
                    obj.Cidade = reader["Cidade"].ToString();
                    obj.Uf = reader["Uf"].ToString();
                    obj.Cep = reader["Cep"].ToString();
                    obj.Email = reader["Email"].ToString();
                    obj.Fone = reader["Fone"].ToString();
                    obj.Fax = reader["Fax"].ToString();
                    listaProfissao.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Profissao obj2 = new Profissao();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaProfissao;
        }

        public List<Profissao> findAll()
        {
            List<Profissao> listaProfissao = new List<Profissao>();
            string find = "select*from Profissao order by Profissao1 asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Profissao obj = new Profissao();
                    obj.Id_Profissao = Convert.ToInt32(reader["Id_Profissão"].ToString());
                    obj.Cim = Convert.ToInt32(reader["Cim"].ToString());
                    obj.Aposentado = reader["Aposentado"].ToString();
                    obj.Profissao1 = reader["Profissão"].ToString();
                    obj.Empresa_Orgao = reader["Empresa_Órgão"].ToString();
                    obj.Cargo = reader["Cargo"].ToString();
                    obj.Endereco = reader["Endereço"].ToString();
                    obj.Bairro = reader["Bairro"].ToString();
                    obj.Cidade = reader["Cidade"].ToString();
                    obj.Uf = reader["Uf"].ToString();
                    obj.Cep = reader["Cep"].ToString();
                    obj.Email = reader["Email"].ToString();
                    obj.Fone = reader["Fone"].ToString();
                    obj.Fax = reader["Fax"].ToString();
                    listaProfissao.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Profissao obj2 = new Profissao();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaProfissao;
        }

        public List<Profissao> findAllProfissao(Profissao objProfissao)
        {
            List<Profissao> listaProfissao = new List<Profissao>();
            string findAll = "select* from Profissao where Aposentado like '%" + objProfissao.Aposentado + "%' or Profissao1 like '%" + objProfissao.Profissao1 + "%' or Empresa_Orgao like '%" + objProfissao.Empresa_Orgao + "%' or Cargo like '%" + objProfissao.Cargo + "%' or Endereco like '%" + objProfissao.Endereco + "%' or Bairro like '%" + objProfissao.Bairro + "%' or Cidade like '%" + objProfissao.Cidade + "%' or Uf like '%" + objProfissao.Uf + "%' or Cep like '%" + objProfissao.Cep + "%' or Email like '%" + objProfissao.Email + "%' ";
            try
            {

                comando = new SqlCommand(findAll, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Profissao obj = new Profissao();
                    obj.Id_Profissao = Convert.ToInt32(reader["Id_Profissão"].ToString());
                    obj.Cim = Convert.ToInt32(reader["Cim"].ToString());
                    obj.Aposentado = reader["Aposentado"].ToString();
                    obj.Profissao1 = reader["Profissão"].ToString();
                    obj.Empresa_Orgao = reader["Empresa_Órgão"].ToString();
                    obj.Cargo = reader["Cargo"].ToString();
                    obj.Endereco = reader["Endereço"].ToString();
                    obj.Bairro = reader["Bairro"].ToString();
                    obj.Cidade = reader["Cidade"].ToString();
                    obj.Uf = reader["Uf"].ToString();
                    obj.Cep = reader["Cep"].ToString();
                    obj.Email = reader["Email"].ToString();
                    obj.Fone = reader["Fone"].ToString();
                    obj.Fax = reader["Fax"].ToString();
                    listaProfissao.Add(obj);

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

            return listaProfissao;

        }

        public List<Profissao> findProfissaoPorId(Profissao objProfissao)
        {
            List<Profissao> listaProfissao = new List<Profissao>();
            string findAll = "select * from Profissao where Id_Profissao = '" + objProfissao.Id_Profissao + "' ";
            try
            {

                comando = new SqlCommand(findAll, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Profissao obj = new Profissao();
                    obj.Id_Profissao = Convert.ToInt32(reader["Id_Profissão"].ToString());
                    obj.Cim = Convert.ToInt32(reader["Cim"].ToString());
                    obj.Aposentado = reader["Aposentado"].ToString();
                    obj.Profissao1 = reader["Profissão"].ToString();
                    obj.Empresa_Orgao = reader["Empresa_Órgão"].ToString();
                    obj.Cargo = reader["Cargo"].ToString();
                    obj.Endereco = reader["Endereço"].ToString();
                    obj.Bairro = reader["Bairro"].ToString();
                    obj.Cidade = reader["Cidade"].ToString();
                    obj.Uf = reader["Uf"].ToString();
                    obj.Cep = reader["Cep"].ToString();
                    obj.Email = reader["Email"].ToString();
                    obj.Fone = reader["Fone"].ToString();
                    obj.Fax = reader["Fax"].ToString();
                    listaProfissao.Add(obj);

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

            return listaProfissao;

        }
    }
}
