using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class VideosDao : Obrigatorio<Videos>
    {
        private ConexaoDB objConexaoDB;
        private SqlCommand comando;
        private SqlDataReader reader;

        public VideosDao()
        {
            objConexaoDB = ConexaoDB.saberEstado();
        }

        public void create(Videos obj)
        {
            throw new NotImplementedException();
        }

        public void delete(Videos obj)
        {
            throw new NotImplementedException();
        }

        public bool find(Videos obj)
        {
            throw new NotImplementedException();
        }

        public List<Videos> findAll()
        {
            List<Videos> listaGraduacao = new List<Videos>();
            string find = "select*from Videos order by Title asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Videos obj = new Videos();
                    obj.CompanyId = Convert.ToInt32(reader["CompanyId"].ToString());
                    obj.CreatedAt = Convert.ToDateTime(reader["CreatedAt"].ToString());
                    obj.Description = reader["Description"].ToString();
                    obj.FileName = reader["FileName"].ToString();
                    obj.IsPublic = Convert.ToInt32(reader["IsPublic"].ToString());
                    obj.Title = reader["Dt_Inicial"].ToString();
                    obj.VideoId = Convert.ToInt32(reader["Dt_Final"].ToString());
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

        public void update(Videos obj)
        {
            throw new NotImplementedException();
        }
    }
}
