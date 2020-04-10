using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ValorRegionaisDao : Obrigatorio<ValorRegionais>
    {
        private ConexaoDB objConexaoDB;
        private SqlCommand comando;
        private SqlDataReader reader;

        public ValorRegionaisDao()
        {
            objConexaoDB = ConexaoDB.saberEstado();
        }

        public List<ValorRegionais> BuscaPorData(string codregional, string dataini, string datafim)
        {
            List<ValorRegionais> listavaloresRegionais = new List<ValorRegionais>();
            string find = "select * from ValoresRegionais where DataInicial = '" + dataini + "' and DataFinal = '" + datafim + "' and CodRegional = '" + codregional + "'";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    ValorRegionais obj = new ValorRegionais();
                    obj.Id = Convert.ToInt32(reader["Id"].ToString());
                    obj.CodRegional = reader["CodRegional"].ToString();
                    obj.Regional = reader["Regional"].ToString();
                    obj.DataFinal = reader["DataFinal"].ToString();
                    obj.DataInicial = reader["DataInicial"].ToString();
                    obj.Descricao = reader["Descricao"].ToString();
                    obj.Ativo = Convert.ToBoolean(reader["Ativo"].ToString());
                    obj.DataCadastro = reader["DataCadastro"].ToString();

                    listavaloresRegionais.Add(obj);
                }

            }
            catch (Exception ex)
            {
                RemessaWeb obj2 = new RemessaWeb();
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listavaloresRegionais;
        }

        public List<ValorRegionais> BuscaPorRegional(string codregional)
        {
            List<ValorRegionais> listavaloresRegionais = new List<ValorRegionais>();
            string find = "select * from ValoresRegionais where CodRegional = '" + codregional + "' order by DataInicial";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    ValorRegionais obj = new ValorRegionais();
                    obj.Id = Convert.ToInt32(reader["Id"].ToString());
                    obj.CodRegional = reader["CodRegional"].ToString();
                    obj.Regional = reader["Regional"].ToString();
                    obj.DataFinal = reader["DataFinal"].ToString();
                    obj.DataInicial = reader["DataInicial"].ToString();
                    obj.Descricao = reader["Descricao"].ToString();
                    obj.Ativo = Convert.ToBoolean(reader["Ativo"].ToString());
                    obj.DataCadastro = reader["DataCadastro"].ToString();


                    listavaloresRegionais.Add(obj);
                }

            }
            catch (Exception ex)
            {
                RemessaWeb obj2 = new RemessaWeb();
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listavaloresRegionais;
        }

        public List<ValorRegionais> findAll()
        {
            List<ValorRegionais> listaRemessa = new List<ValorRegionais>();
            string find = "select * from ValoresRegionais order by DataInicial asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    ValorRegionais obj = new ValorRegionais();
                    obj.Id = Convert.ToInt32(reader["Id"].ToString());
                    obj.CodRegional = reader["CodRegional"].ToString();
                    obj.Regional = reader["Regional"].ToString();
                    obj.DataFinal = reader["DataFinal"].ToString();
                    obj.DataInicial = reader["DataInicial"].ToString();
                    obj.Descricao = reader["Descricao"].ToString();
                    obj.Ativo = Convert.ToBoolean(reader["Ativo"].ToString());
                    obj.DataCadastro = reader["DataCadastro"].ToString();

                    listaRemessa.Add(obj);
                }

            }
            catch (Exception ex)
            {
                RemessaWeb obj2 = new RemessaWeb();
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaRemessa;
        }

        List<ValorRegionais> Obrigatorio<ValorRegionais>.findAll()
        {
            List<ValorRegionais> listavaloresRegionais = new List<ValorRegionais>();
            string find = "select * from ValoresRegionais order by CodRegional, DataInicial";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    ValorRegionais obj = new ValorRegionais();
                    obj.Id = Convert.ToInt32(reader["Id"].ToString());
                    obj.CodRegional = reader["CodRegional"].ToString();
                    obj.Regional = reader["Regional"].ToString();
                    obj.DataFinal = reader["DataFinal"].ToString();
                    obj.Descricao = reader["Descricao"].ToString();
                    obj.DataInicial = reader["DataInicial"].ToString();
                    obj.Ativo = Convert.ToBoolean(reader["Ativo"].ToString());
                    obj.DataCadastro = reader["DataCadastro"].ToString();

                    listavaloresRegionais.Add(obj);
                }

            }
            catch (Exception ex)
            {
                RemessaWeb obj2 = new RemessaWeb();
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listavaloresRegionais;

        }


        public void create(ValorRegionais obj)
        {
            Random indrandomico = new Random();
            int id = indrandomico.Next();
            DateTime cadastro = DateTime.Now;
            string create = "insert into ValoresRegionais(Id,CodRegional,Regional,Valor,DataInicial,DataFinal,Ativo,DataCadastro,Descricao)  values('" + id.ToString() + "','" + obj.CodRegional + "','" + obj.Regional + "','" + obj.Valor + "','" + obj.DataInicial + "','" + obj.DataFinal + "','" + obj.Ativo + "','" + cadastro + "','" + obj.Descricao + "')";
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

        public void delete(ValorRegionais obj)
        {
            throw new NotImplementedException();
        }

        public bool find(ValorRegionais obj)
        {
            throw new NotImplementedException();
        }


        public void update(ValorRegionais obj)
        {
            throw new NotImplementedException();
        }

    }
}
