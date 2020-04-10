using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class RegionaisDao : Obrigatorio<Regionais>
    {
        private ConexaoDB objConexaoDB;
        private SqlCommand comando;
        private SqlDataReader reader;

        public RegionaisDao()
        {
            objConexaoDB = ConexaoDB.saberEstado();
        }

        public List<Regionais> BuscaPorCodigo(string codigo)
        {
            List<Regionais> listaRemessa = new List<Regionais>();
            string find = "select * from v_Regionais where CodRegional = '" + codigo + "' ";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Regionais obj = new Regionais();
                    obj.CodRegional = reader["CodRegional"].ToString();
                    obj.NomeRegional = reader["NomeRegional"].ToString();
                   
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
        public List<Regionais> BuscaPorNome(string nome)
        {
            List<Regionais> listaRemessa = new List<Regionais>();
            string find = "select * from v_Regionais where NomeRegional = '" + nome + "' ";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Regionais obj = new Regionais();
                    obj.CodRegional = reader["CodRegional"].ToString();
                    obj.NomeRegional = reader["NomeRegional"].ToString();

                    listaRemessa.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Regionais obj2 = new Regionais();
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaRemessa;
        }

        public void create(Regionais obj)
        {
            throw new NotImplementedException();
        }

        public void delete(Regionais obj)
        {
            throw new NotImplementedException();
        }

        public bool find(Regionais obj)
        {
            throw new NotImplementedException();
        }

        public List<Regionais> findAll()
        {
            List<Regionais> listaRemessa = new List<Regionais>();
            string find = "select * from v_Regionais order by NomeRegional asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Regionais obj = new Regionais();

                    obj.CodRegional = reader["CodRegional"].ToString();
                    obj.NomeRegional = reader["NomeRegional"].ToString();
                  
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

        public void update(Regionais obj)
        {
            throw new NotImplementedException();
        }

    }
}
