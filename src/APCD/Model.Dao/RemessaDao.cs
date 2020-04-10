using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
   public class RemessaDao : Obrigatorio<RemessaWeb>
    {
        private ConexaoDB objConexaoDB;
        private SqlCommand comando;
        private SqlDataReader reader;

        public RemessaDao()
        {
            objConexaoDB = ConexaoDB.saberEstado();
        }

        public void create(RemessaWeb obj)
        {
                string create = "insert into Repasses(sequencia,Matricula,Nome,Categoria,Vencimento,Valor,LCM,Codigo,ValorRepasse,Pago,CodRegional,NomeRegional,PN,MesAno,ativo,empresa,conta) "+
                "values('" + obj.sequencia + "','" + obj.Matricula + "','" + obj.Nome + "','" + obj.Categoria + "','" + obj.Vencimento + "','" + obj.Valor + "','" + obj.LCM + "','" + obj.Codigo + "','" + obj.ValorRepasse + "','" + obj.Pago + "','" + obj.CodRegional + "','" + obj.NomeRegional + "','" + obj.PN + "','" + obj.MesAno + "','" + obj.Ativo + "','" + obj.Empresa + "','" + obj.Conta + "')";

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
        public void delete(RemessaWeb obj)
        {
            throw new NotImplementedException();
        }
        public bool find(RemessaWeb obj)
        {
            throw new NotImplementedException();
        }
        public List<RemessaWeb> BuscaPorId(string Id)
        {
            List<RemessaWeb> listaRemessa = new List<RemessaWeb>();
            string find = "select * from Repasses where indice = '" + Id + "' order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    RemessaWeb obj = new RemessaWeb();

                    obj.Categoria = reader["Categoria"].ToString();
                    obj.CodRegional = reader["CodRegional"].ToString();
                    obj.NomeRegional = reader["NomeRegional"].ToString();
                    obj.PN = reader["PN"].ToString();
                    obj.Codigo = reader["Codigo"].ToString();
                    obj.Documento = reader["Documento"].ToString();
                    obj.indice = Convert.ToInt32(reader["indice"].ToString());
                    obj.LCM = reader["LCM"].ToString();
                    obj.Matricula = reader["Matricula"].ToString();
                    obj.Nome = reader["Nome"].ToString();
                    obj.Pago = Convert.ToBoolean(reader["Pago"].ToString());
                    obj.sequencia = reader["sequencia"].ToString();
                    obj.Valor = reader["Valor"].ToString();
                    obj.ValorRepasse = reader["ValorRepasse"].ToString();
                    obj.Vencimento = reader["Vencimento"].ToString();
                    obj.MesAno = reader["MesAno"].ToString();
                    obj.Empresa = reader["Empresa"].ToString();
                    obj.Conta = reader["Conta"].ToString();
                    obj.Ativo = reader["Ativo"].ToString();
                    obj.DataPagto = reader["DataPagto"].ToString();
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
        public List<RemessaWeb> BuscaPorRegional(string regional, bool pago, string ativo, string mesano, DateTime DataIni, DateTime DataFim, bool Ativo, string novoValor)
        {
            List<RemessaWeb> listaRemessa = new List<RemessaWeb>();
            string find = "select * from Repasses where CodRegional = '"+ regional + "' and pago = '" + pago + "' and mesano = '" + mesano + "' order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    RemessaWeb obj = new RemessaWeb();

                    obj.Categoria = reader["Categoria"].ToString();
                    obj.CodRegional = reader["CodRegional"].ToString();
                    obj.NomeRegional = reader["NomeRegional"].ToString();
                    obj.PN = reader["PN"].ToString();
                    obj.Codigo = reader["Codigo"].ToString();
                    obj.Documento = reader["Documento"].ToString();
                    obj.indice = Convert.ToInt32(reader["indice"].ToString());
                    obj.LCM = reader["LCM"].ToString();
                    obj.Matricula = reader["Matricula"].ToString();
                    obj.Nome = reader["Nome"].ToString();
                    obj.Pago = Convert.ToBoolean(reader["Pago"].ToString());
                    obj.sequencia = reader["sequencia"].ToString();
                    //if ((DateTime.Now >=  DataIni   || DateTime.Now <=  DataFim  ) && Ativo == true)
                    //{
                    //    obj.Valor = novoValor; 
                    //}
                    //else
                    //{
                    //    obj.Valor = reader["Valor"].ToString();
                    //}
                    obj.Valor = reader["Valor"].ToString();
                    obj.ValorRepasse = reader["ValorRepasse"].ToString();
                    obj.Vencimento = reader["Vencimento"].ToString();
                    obj.MesAno = reader["MesAno"].ToString();
                    obj.Ativo = reader["Ativo"].ToString();
                    obj.Empresa = reader["Empresa"].ToString();
                    obj.Conta = reader["Conta"].ToString();
                    obj.Ativo = reader["Ativo"].ToString();
                    obj.DataPagto = reader["DataPagto"].ToString();
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
        public List<RemessaWeb> BuscaPorCodRegional(string regional,string mesano, DateTime DataIni, DateTime DataFim, bool Ativo, string novoValor)
        {
            List<RemessaWeb> listaRemessa = new List<RemessaWeb>();
            string find = "select * from Repasses where CodRegional = '" + regional + "' and mesano = '" + mesano + "' and ativo = 'true' and codigo = '0' order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    RemessaWeb obj = new RemessaWeb();

                    obj.Categoria = reader["Categoria"].ToString();
                    obj.CodRegional = reader["CodRegional"].ToString();
                    obj.NomeRegional = reader["NomeRegional"].ToString();
                    obj.PN = reader["PN"].ToString();
                    obj.Codigo = reader["Codigo"].ToString();
                    obj.Documento = reader["Documento"].ToString();
                    obj.indice = Convert.ToInt32(reader["indice"].ToString());
                    obj.LCM = reader["LCM"].ToString();
                    obj.Matricula = reader["Matricula"].ToString();
                    obj.Nome = reader["Nome"].ToString();
                    obj.Pago = Convert.ToBoolean(reader["Pago"].ToString());
                    obj.sequencia = reader["sequencia"].ToString();
                    obj.Valor = reader["Valor"].ToString();
                    obj.ValorRepasse = reader["ValorRepasse"].ToString();
                    //if ((DateTime.Now >= DataIni && DateTime.Now <= DataFim) && Ativo == true)
                    //{
                    //    obj.Valor = novoValor;
                    //}
                    //else
                    //{
                    //    obj.Valor = reader["Valor"].ToString();
                    //}

                    obj.Vencimento = reader["Vencimento"].ToString();
                    obj.MesAno = reader["MesAno"].ToString();
                    obj.Ativo = reader["Ativo"].ToString();
                    obj.Empresa = reader["Empresa"].ToString();
                    obj.Conta = reader["Conta"].ToString();
                    obj.Ativo = reader["Ativo"].ToString();
                    obj.DataPagto = reader["DataPagto"].ToString();
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
        public List<RemessaWeb> BuscaPagos(string ativo)
        {
            List<RemessaWeb> listaRemessa = new List<RemessaWeb>();
            string find = "select * from Repasses where pago = 1 and ativo = '"+ativo+"' order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    RemessaWeb obj = new RemessaWeb();

                    obj.Categoria = reader["Categoria"].ToString();
                    obj.CodRegional = reader["CodRegional"].ToString();
                    obj.NomeRegional = reader["NomeRegional"].ToString();
                    obj.PN = reader["PN"].ToString();
                    obj.Codigo = reader["Codigo"].ToString();
                    obj.Documento = reader["Documento"].ToString();
                    obj.indice = Convert.ToInt32(reader["indice"].ToString());
                    obj.LCM = reader["LCM"].ToString();
                    obj.Matricula = reader["Matricula"].ToString();
                    obj.Nome = reader["Nome"].ToString();
                    obj.Pago = Convert.ToBoolean(reader["Pago"].ToString());
                    obj.sequencia = reader["sequencia"].ToString();
                    obj.Valor = reader["Valor"].ToString();
                    obj.ValorRepasse = reader["ValorRepasse"].ToString();
                    obj.Vencimento = reader["Vencimento"].ToString();
                    obj.MesAno = reader["MesAno"].ToString();
                    obj.Ativo = reader["Ativo"].ToString();
                    obj.Empresa = reader["Empresa"].ToString();
                    obj.Conta = reader["Conta"].ToString();
                    obj.Ativo = reader["Ativo"].ToString();
                    obj.DataPagto = reader["DataPagto"].ToString();
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
        public List<RemessaWeb> findAll()
        {
            List<RemessaWeb> listaRemessa = new List<RemessaWeb>();
            string find = "select * from Repasses order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    RemessaWeb obj = new RemessaWeb();

                    obj.Categoria = reader["Categoria"].ToString();
                    obj.CodRegional = reader["CodRegional"].ToString();
                    obj.NomeRegional = reader["NomeRegional"].ToString();
                    obj.PN = reader["PN"].ToString();
                    obj.Codigo = reader["Codigo"].ToString();
                    obj.Documento = reader["Documento"].ToString();
                    obj.indice = Convert.ToInt32(reader["indice"].ToString());
                    obj.LCM = reader["LCM"].ToString();
                    obj.Matricula = reader["Matricula"].ToString();
                    obj.Nome = reader["Nome"].ToString();
                    obj.Pago = Convert.ToBoolean(reader["Pago"].ToString());
                    obj.sequencia = reader["sequencia"].ToString();
                    obj.Valor = reader["Valor"].ToString();
                    obj.ValorRepasse = reader["ValorRepasse"].ToString();
                    obj.Vencimento = reader["Vencimento"].ToString();
                    obj.MesAno = reader["MesAno"].ToString();
                    obj.Empresa = reader["Empresa"].ToString();
                    obj.Conta = reader["Conta"].ToString();
                    obj.Ativo = reader["Ativo"].ToString();
                    obj.DataPagto = reader["DataPagto"].ToString();
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
        public void update(RemessaWeb obj)
        {//update repasses set ativo =  'false' , pago = 1 where indice = 144805
            string update = "update Repasses set Pago = '" + obj.Pago + "', ativo = '" + obj.Ativo + "', DataPagto = '" + obj.DataPagto + "', Codigo = '" + obj.Codigo + "'   where Indice = '" + obj.indice + "' ";
                try
                {
                    comando = new SqlCommand(update, objConexaoDB.getCon());
                    objConexaoDB.getCon().Open();
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    var erro = ex.Message;//    obj.Estados = 1000;
                }
                finally
                {
                    objConexaoDB.getCon().Close();
                    objConexaoDB.CloseDB();
                }
        }
        public void baixaLote(string Pago, string Ativo, string CodRegional, string MesAno, string ListaLCM)
        {//update repasses set ativo =  'false' , pago = 1 where indice = 144805
            string update = "update Repasses set Pago = '" + Pago + "', ativo = '" + Ativo + "'  where LCM in ('" + ListaLCM + "') and CodRegional = '" + CodRegional + "' and mesano = '" + MesAno + "'  ";
            try
            {
                comando = new SqlCommand(update, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                var erro = ex.Message;//    obj.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }
        }
    }
}
