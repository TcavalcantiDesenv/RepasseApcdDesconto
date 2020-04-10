using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Model.Dao
{
    public class LojasDao : Obrigatorio<Lojas>
    {
        private ConexaoDB objConexaoDB;
        private SqlCommand comando;
        private SqlDataReader reader;

        public LojasDao()
        {
            objConexaoDB = ConexaoDB.saberEstado();
        }

        public void create(Lojas obj)
        {
            throw new System.NotImplementedException();
        }

        public void delete(Lojas obj)
        {
            throw new System.NotImplementedException();
        }

        public bool find(Lojas obj)
        {
            throw new System.NotImplementedException();
        }

        public List<Lojas> findAll()
        {

            List<Lojas> listaVisitante = new List<Lojas>();
            string find = "select*from Lojas order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Lojas obj = new Lojas();
                    obj.Id_Loja = Convert.ToInt32(reader["Id_Loja"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    obj.Veneravel = reader["Veneravel"].ToString();
                    obj.Potencia = reader["Potencia"].ToString();
                    listaVisitante.Add(obj);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaVisitante;
        }

        public void update(Lojas obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
