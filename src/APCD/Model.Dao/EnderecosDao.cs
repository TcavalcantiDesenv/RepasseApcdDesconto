using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class EnderecosDao : Obrigatorio<Enderecos>
    {
        private ConexaoDB objConexaoDB;
        private SqlCommand comando;
        private SqlDataReader reader;

        public EnderecosDao()
        {
            objConexaoDB = ConexaoDB.saberEstado();
        }
        public List<Enderecos> BuscaPorCodigo(string codigo)
        {
            List<Enderecos> listaEndereco = new List<Enderecos>();
            string find = "select * from Enderecoes where CodRegional = '" + codigo + "' ";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Enderecos obj = new Enderecos();
                    obj.CodRegional = Convert.ToInt32(reader["CodRegional"]);
                    obj.Descricao  = reader["Descricao"].ToString();
                    obj.Complemento  = reader["Complemento"].ToString();
                    obj.Cidade = reader["Cidade"].ToString();
                    obj.Cep = reader["Cep"].ToString();
                    obj.Bairro = reader["Bairro"].ToString();
                    obj.UF = reader["UF"].ToString();

                    listaEndereco.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Enderecos obj2 = new Enderecos();
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaEndereco;
        }

        public void create(Enderecos obj)
        {
            throw new NotImplementedException();
        }

        public void delete(Enderecos obj)
        {
            throw new NotImplementedException();
        }

        public bool find(Enderecos obj)
        {
            throw new NotImplementedException();
        }

        public List<Enderecos> findAll()
        {
            throw new NotImplementedException();
        }

        public void update(Enderecos obj)
        {
            throw new NotImplementedException();
        }
    }
}
