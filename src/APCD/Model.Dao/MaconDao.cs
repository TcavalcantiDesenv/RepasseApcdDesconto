using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model.Entity;

namespace Model.Dao
{
    public class MaconDao : Obrigatorio<Macon>
    {
        private ConexaoDB objConexaoDB;
        private SqlCommand comando;
        private SqlDataReader reader;

        public MaconDao()
        {
            objConexaoDB = ConexaoDB.saberEstado();
        }

        public void create(Macon obj)
        {
            if (obj.Situacao == "1") obj.Situacao = "Em Loja";
            if (obj.Situacao == "Ativo") obj.Situacao = "Em Loja";
            if (obj.Situacao == "2") obj.Situacao = "Adormecido";
            if (obj.Cim == null) obj.Cim = "0";

            string create = "insert into Macon(Id_Condecoracao,Id_Cargo,Cim,Id_Loja,Id_Graduacao,Nome,Nome_Tratamento,Grau,Situacao,Email,Guide)  values(0,0,'" + obj.Cim + "','" + obj.Id_Loja + "','" + obj.Id_Graduacao + "','" + obj.Nome + "','" + obj.Nome_Tratamento + "','" + obj.Grau + "','" + obj.Situacao + "','" + obj.Email + "','" + obj.Guide + "')";

            //string create = "insert into Macon values('" + obj.Cim + "','" + obj.Id_Loja + "','" + obj.Id_Graduacao + "','" + obj.Id_Cargo + "','" + obj.Id_Condecoracao + "','" + obj.Nome + "','" + obj.Nome_Tratamento + "','" + obj.Grau + "','" + obj.Senha + "','" + obj.Situacao + "','" + obj.CPF + "','" + obj.Login + "','" + obj.Email + "','" + obj.Email_Outros + "','" + obj.Dt_Nascimento + "','" + obj.Naturalidade + "','" + obj.Uf + "','" + obj.Nacionalidade + "','" + obj.Religiao + "','" + obj.Escolaridade + "','" + obj.Sexo + "','" + obj.Tipo_Sanguíneo + "','" + obj.Estado_Civil + "','" + obj.Data_Casamento + "','" + obj.Tipo_Di + "','" + obj.Num_Di+ "','" + obj.Emissor_Di + "','" + obj.Dt_Emissao_Di + "','" + obj.Uf_Di + "','" + obj.Tit_Eleitor + "','" + obj.Zona_Eleitoral + "','" + obj.Secao_Eleitoral + "','" + obj.Nome_Pai + "','" + obj.Nome_Mae + "','" + obj.End_Res + "','" + obj.Bairro_Res + "','" + obj.Cidade_Res + "','" + obj.Uf_Res + "','" + obj.Cep_Res + "','" + obj.cx_postal + "','" + obj.celular + "','" + obj.tel_res + "','" + obj.fax + "','" + obj.obs + "','" + obj.correspondencia_end + "','" + obj.foto + "','" + obj.Dt_Iniciado + "','" + obj.Guide + "')";
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

        public void delete(Macon obj)
        {
            string delete = "delete from Macon where Id_Macon ='" + obj.Id_Macon + "'";
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

        public void update(Macon obj)
        {
            DateTime datainiciante = DateTime.Now;
            DateTime datanascimento = DateTime.Now;
            DateTime datacasamento = DateTime.Now;
            DateTime dataemissao = DateTime.Now;
            //if (obj.Dt_Iniciado == null) obj.Dt_Iniciado = Convert.ToDateTime("01/01/1970");
            //if (obj.Dt_Nascimento == null) obj.Dt_Nascimento = "01/01/1970";
            //if (obj.Dt_Emissao_Di == null) obj.Dt_Emissao_Di = "01/01/1970";
            //if (obj.Data_Casamento == null) obj.Data_Casamento = "01/01/1970";
            if (obj.Dt_Iniciado != null) { datainiciante = Convert.ToDateTime(obj.Dt_Iniciado.ToString()); };
            if (obj.Dt_Nascimento != null) { datanascimento = Convert.ToDateTime(obj.Dt_Nascimento.ToString()); };
            if (obj.Dt_Emissao_Di != null) {  dataemissao = Convert.ToDateTime(obj.Dt_Emissao_Di.ToString()); };
            if (obj.Data_Casamento != null) { datacasamento = Convert.ToDateTime(obj.Data_Casamento.ToString()); };

            if (datainiciante < Convert.ToDateTime("01/01/1970")) datainiciante = Convert.ToDateTime("01/01/1970");
            if (datanascimento < Convert.ToDateTime("01/01/1970")) datanascimento = Convert.ToDateTime("01/01/1970");
            if (dataemissao < Convert.ToDateTime("01/01/1970")) dataemissao = Convert.ToDateTime("01/01/1970");
            if (datacasamento < Convert.ToDateTime("01/01/1970")) datacasamento = Convert.ToDateTime("01/01/1970");


            string update = "update Macon set Dt_Iniciado=convert(datetime,'" +  datainiciante.ToString("yyyy-MM-dd") + "',120),Cim='" + obj.Cim + "',Id_Loja='" + obj.Id_Loja + "',Id_Graduacao='" + obj.Id_Graduacao + "',Id_Cargo='" + obj.Id_Cargo + "',Id_Condecoracao='" + obj.Id_Condecoracao + "',Nome='" + obj.Nome + "',Nome_Tratamento='" + obj.Nome_Tratamento + "',Senha='" + obj.Senha + "',Situacao='" + obj.Situacao + "',CPF='" + obj.CPF + "',Login='" + obj.Login + "',Email='" + obj.Email + "',Email_Outros='" + obj.Email_Outros + "',Dt_Nascimento=convert(datetime,'" + datanascimento.ToString("yyyy-MM-dd") + "',120),Naturalidade='" + obj.Naturalidade + "',Uf='" + obj.Uf + "',Nacionalidade='" + obj.Nacionalidade + "',Religiao='" + obj.Religiao + "',Escolaridade='" + obj.Escolaridade + "',Sexo='" + obj.Sexo + "',Tipo_Sanguíneo='" + obj.Tipo_Sanguíneo + "',Estado_Civil='" + obj.Estado_Civil + "',Data_Casamento=convert(datetime,'" + datacasamento.ToString("yyyy-MM-dd") + "',120),Tipo_Di='" + obj.Tipo_Di + "',Num_Di='" + obj.Num_Di + "',Emissor_Di='" + obj.Emissor_Di + "',Dt_Emissao_Di=convert(datetime,'" + dataemissao.ToString("yyyy-MM-dd") + "',120),Uf_Di='" + obj.Uf_Di + "',Tit_Eleitor='" + obj.Tit_Eleitor + "',Zona_Eleitoral='" + obj.Zona_Eleitoral + "',Secao_Eleitoral='" + obj.Secao_Eleitoral + "',Nome_Pai='" + obj.Nome_Pai + "',Nome_Mae='" + obj.Nome_Mae + "',End_Res='" + obj.End_Res + "',Bairro_Res='" + obj.Bairro_Res + "',Cidade_Res='" + obj.Cidade_Res + "',Uf_Res='" + obj.Uf_Res + "',Cep_Res='" + obj.Cep_Res + "',cx_postal='" + obj.cx_postal + "',celular='" + obj.celular + "',tel_res='" + obj.tel_res + "',fax='" + obj.fax + "',obs='" + obj.obs + "',correspondencia_end='" + obj.correspondencia_end + "',foto='" + obj.foto + "' where Id_Macon = '" + obj.Id_Macon + "' ";
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

        public bool find(Macon obj)
        {
            bool temRegistros;
            string find = "select*from Macon where Id_Macon='" + obj.Id_Macon + "'";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                temRegistros = reader.Read();
                if (temRegistros)
                {
                    obj.Id_Macon = Convert.ToInt32(reader["Id_Macon"].ToString());
                    obj.Cim = reader["Cim"].ToString();
                    obj.Id_Loja = Convert.ToInt32(reader["Id_Loja"].ToString());
                    obj.Id_Graduacao = Convert.ToInt32(reader["Id_Graduacao"].ToString());
                    obj.Id_Cargo = Convert.ToInt32(reader["Id_Cargo"].ToString());
                    obj.Id_Condecoracao = Convert.ToInt32(reader["Id_Condecoracao"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    obj.Guide = reader["Guide"].ToString();
                    //if (("Dt_Iniciado"] != null) obj.Dt_Iniciado = reader["Dt_Iniciado"] ;
                    obj.Grau = reader["Grau"].ToString();
                    obj.Nome_Tratamento = reader["Nome_Tratamento"].ToString();
                    obj.Senha = reader["Senha"].ToString();
                    obj.Situacao = reader["Situacao"].ToString();
                    obj.CPF = reader["CPF"].ToString();
                    obj.Login = reader["Login"].ToString();
                    obj.Email = reader["Email"].ToString();
                    obj.Email_Outros = reader["Email_Outros"].ToString();
                  //  obj.Dt_Nascimento = reader["Dt_Nascimento"].ToString();
                    if ((reader["Dt_Nascimento"].ToString()) != "" || (reader["Dt_Nascimento"].ToString()) == null) obj.Dt_Nascimento = Convert.ToDateTime(reader["Dt_Nascimento"].ToString());
                    if ((reader["Dt_Iniciado"].ToString()) != "" || (reader["Dt_Iniciado"].ToString()) == null) obj.Dt_Iniciado = Convert.ToDateTime(reader["Dt_Iniciado"].ToString());
                    if ((reader["Data_Casamento"].ToString()) != "" || (reader["Data_Casamento"].ToString()) == null) obj.Data_Casamento = Convert.ToDateTime(reader["Data_Casamento"].ToString());
                    if ((reader["Dt_Emissao_Di"].ToString()) != "" || (reader["Dt_Emissao_Di"].ToString()) == null) obj.Dt_Emissao_Di = Convert.ToDateTime(reader["Dt_Emissao_Di"].ToString());

                    obj.Naturalidade = reader["Naturalidade"].ToString();
                    obj.Uf = reader["Uf"].ToString();
                    obj.Nacionalidade = reader["Nacionalidade"].ToString();
                    obj.Religiao = reader["Religiao"].ToString();
                    obj.Escolaridade = reader["Escolaridade"].ToString();
                    obj.Sexo = reader["Sexo"].ToString();
                    //obj.Tipo_Sanguíneo = reader["Tipo_Sanguíneo"].ToString();
                    obj.Estado_Civil = reader["Estado_Civil"].ToString();
                  //  obj.Data_Casamento = reader["Data_Casamento"].ToString();

                    obj.Tipo_Di = reader["Tipo_Di"].ToString();
                    obj.Num_Di = reader["Num_Di"].ToString();
                    obj.Emissor_Di = reader["Emissor_Di"].ToString();
                  //  obj.Dt_Emissao_Di = reader["Dt_Emissao_Di"].ToString();

                    obj.Uf_Di = reader["Uf_Di"].ToString();
                    obj.Tit_Eleitor = reader["Tit_Eleitor"].ToString();
                    obj.Zona_Eleitoral = reader["Zona_Eleitoral"].ToString();
                    obj.Secao_Eleitoral = reader["Secao_Eleitoral"].ToString();
                    obj.Nome_Pai = reader["Nome_Pai"].ToString();
                    obj.Nome_Mae = reader["Nome_Mae"].ToString();
                    obj.End_Res = reader["End_Res"].ToString();
                    obj.Bairro_Res = reader["Bairro_Res"].ToString();
                    obj.Cidade_Res = reader["Cidade_Res"].ToString();
                    obj.Uf_Res = reader["Uf_Res"].ToString();
                    obj.Cep_Res = reader["Cep_Res"].ToString();
                    obj.cx_postal = reader["Cx_postal"].ToString();
                    obj.celular = reader["Celular"].ToString();
                    obj.tel_res = reader["Tel_res"].ToString();
                    obj.fax = reader["Fax"].ToString();
                    obj.obs = reader["Obs"].ToString();
                    obj.correspondencia_end = reader["correspondencia_end"].ToString();
                    ////    obj.foto = reader["Foto"].ToString();
                    //if (obj.Situacao == "Em Loja" ) obj.Situacao = "1";
                    //if (obj.Situacao == "Adormecido") obj.Situacao = "2";
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

        List<Macon> Obrigatorio<Macon>.findAll()
        {
            List<Macon> listaMacon = new List<Macon>();
            string find = "select*from Macon order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Macon obj = new Macon();
                    obj.Id_Macon = Convert.ToInt32(reader["Id Id_Macon"].ToString());
                    obj.Cim = reader["Cim"].ToString();
                    obj.Id_Loja = Convert.ToInt32(reader["Id_Loja"].ToString());
                    obj.Id_Graduacao = Convert.ToInt32(reader["Id_Graduacao"].ToString());
                    obj.Id_Cargo = Convert.ToInt32(reader["Id_Cargo"].ToString());
                    obj.Id_Condecoracao = Convert.ToInt32(reader["Id_Condecoracao"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    obj.Guide = reader["Guide"].ToString();
                    obj.Grau = reader["Grau"].ToString();
                    obj.Nome_Tratamento = reader["Nome_Tratamento"].ToString();
                    obj.Senha = reader["Senha"].ToString();
                    obj.Situacao = reader["Situacao"].ToString();
                    obj.CPF = reader["CPF"].ToString();
                    obj.Login = reader["Login"].ToString();
                    obj.Dt_Iniciado = Convert.ToDateTime(reader["Dt_Iniciado"].ToString());
                    obj.Email = reader["Email"].ToString();
                    obj.Email_Outros = reader["Email_Outros"].ToString();
                    if ((reader["Dt_Nascimento"].ToString()) != "" || (reader["Dt_Nascimento"].ToString()) == null) obj.Dt_Nascimento = Convert.ToDateTime(reader["Dt_Nascimento"].ToString());
                    if ((reader["Dt_Iniciado"].ToString()) != "" || (reader["Dt_Iniciado"].ToString()) == null) obj.Dt_Iniciado = Convert.ToDateTime(reader["Dt_Iniciado"].ToString());
                    if ((reader["Data_Casamento"].ToString()) != "" || (reader["Data_Casamento"].ToString()) == null) obj.Data_Casamento = Convert.ToDateTime(reader["Data_Casamento"].ToString());
                    if ((reader["Dt_Emissao_Di"].ToString()) != "" || (reader["Dt_Emissao_Di"].ToString()) == null) obj.Dt_Emissao_Di = Convert.ToDateTime(reader["Dt_Emissao_Di"].ToString());

                 //   obj.Dt_Nascimento = reader["Dt_Nascimento"].ToString();
                    obj.Naturalidade = reader["Naturalidade"].ToString();
                    obj.Uf = reader["Uf"].ToString();
                    obj.Nacionalidade = reader["Nacionalidade"].ToString();
                    obj.Religiao = reader["Religiao"].ToString();
                    obj.Escolaridade = reader["Escolaridade"].ToString();
                    obj.Sexo = reader["Sexo"].ToString();
                    obj.Tipo_Sanguíneo = reader["Tipo_Sanguíneo"].ToString();
                    obj.Estado_Civil = reader["Estado_Civil"].ToString();
                //    obj.Data_Casamento = reader["Data_Casamento"].ToString();
                    obj.Tipo_Di = reader["Tipo_Di"].ToString();
                    obj.Num_Di = reader["Num_Di"].ToString();
                    obj.Emissor_Di = reader["Emissor_Di"].ToString();
                //    obj.Dt_Emissao_Di = reader["Dt_Emissao_Di"].ToString();
                    obj.Uf_Di = reader["Uf_Di"].ToString();
                    obj.Tit_Eleitor = reader["Tit_Eleitor"].ToString();
                    obj.Zona_Eleitoral = reader["Zona_Eleitoral"].ToString();
                    obj.Secao_Eleitoral = reader["Secao_Eleitoral"].ToString();
                    obj.Nome_Pai = reader["Nome_Pai"].ToString();
                    obj.Nome_Mae = reader["Nome_Mae"].ToString();
                    obj.End_Res = reader["End_Res"].ToString();
                    obj.Bairro_Res = reader["Bairro_Res"].ToString();
                    obj.Cidade_Res = reader["Cidade_Res"].ToString();
                    obj.Uf_Res = reader["Uf_Res"].ToString();
                    obj.Cep_Res = reader["Cep_Res"].ToString();
                    obj.cx_postal = reader["cx_postal"].ToString();
                    obj.celular = reader["celular"].ToString();
                    obj.tel_res = reader["tel_res"].ToString();
                    obj.fax = reader["fax"].ToString();
                    obj.obs = reader["obs"].ToString();
                    obj.correspondencia_end = reader["correspondencia_end"].ToString();
                    //   obj.foto = reader["foto"].ToString();
                    //if (obj.Situacao == "Em Loja") obj.Situacao = "1";
                    //if (obj.Situacao == "Adormecido") obj.Situacao = "2";
                    listaMacon.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Macon obj2 = new Macon();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaMacon;
        }

        public List<Macon> FindPorId(Macon obj1)
        {
            List<Macon> listaMacon = new List<Macon>();
            string find = "select*from Macon where Id_Macon = '" + obj1.Id_Macon + "' order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Macon obj = new Macon();
                    obj.Id_Macon = Convert.ToInt32(reader["Id_Macon"].ToString());
                    obj.Cim = reader["Cim"].ToString();
                    obj.Id_Loja = Convert.ToInt32(reader["Id_Loja"].ToString());
                    obj.Id_Graduacao = Convert.ToInt32(reader["Id_Graduacao"].ToString());
                    obj.Id_Cargo = Convert.ToInt32(reader["Id_Cargo"].ToString());
                    obj.Id_Condecoracao = Convert.ToInt32(reader["Id_Condecoracao"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    obj.Guide = reader["Guide"].ToString();
                    obj.Grau = reader["Grau"].ToString();
                    obj.Nome_Tratamento = reader["Nome_Tratamento"].ToString();
                    obj.Senha = reader["Senha"].ToString();
                    obj.Situacao = reader["Situacao"].ToString();
                    obj.CPF = reader["CPF"].ToString();
                    obj.Login = reader["Login"].ToString();
                    obj.Dt_Iniciado = Convert.ToDateTime(reader["Dt_Iniciado"].ToString());
                    obj.Email = reader["Email"].ToString();
                    obj.Email_Outros = reader["Email_Outros"].ToString();
                    if ((reader["Dt_Nascimento"].ToString()) != "" || (reader["Dt_Nascimento"].ToString()) == null) obj.Dt_Nascimento = Convert.ToDateTime(reader["Dt_Nascimento"].ToString());
                    if ((reader["Dt_Iniciado"].ToString()) != "" || (reader["Dt_Iniciado"].ToString()) == null) obj.Dt_Iniciado = Convert.ToDateTime(reader["Dt_Iniciado"].ToString());
                    if ((reader["Data_Casamento"].ToString()) != "" || (reader["Data_Casamento"].ToString()) == null) obj.Data_Casamento = Convert.ToDateTime(reader["Data_Casamento"].ToString());
                    if ((reader["Dt_Emissao_Di"].ToString()) != "" || (reader["Dt_Emissao_Di"].ToString()) == null) obj.Dt_Emissao_Di = Convert.ToDateTime(reader["Dt_Emissao_Di"].ToString());

               //     obj.Dt_Nascimento = reader["Dt_Nascimento"].ToString();
                    obj.Naturalidade = reader["Naturalidade"].ToString();
                    obj.Uf = reader["Uf"].ToString();
                    obj.Nacionalidade = reader["Nacionalidade"].ToString();
                    obj.Religiao = reader["Religiao"].ToString();
                    obj.Escolaridade = reader["Escolaridade"].ToString();
                    obj.Sexo = reader["Sexo"].ToString();
                    obj.Tipo_Sanguíneo = reader["Tipo_Sanguíneo"].ToString();
                    obj.Estado_Civil = reader["Estado_Civil"].ToString();
              //      obj.Data_Casamento = reader["Data_Casamento"].ToString();
                    obj.Tipo_Di = reader["Tipo_Di"].ToString();
                    obj.Num_Di = reader["Num_Di"].ToString();
                    obj.Emissor_Di = reader["Emissor_Di"].ToString();
              //      obj.Dt_Emissao_Di = reader["Dt_Emissao_Di"].ToString();
                    obj.Uf_Di = reader["Uf_Di"].ToString();
                    obj.Tit_Eleitor = reader["Tit_Eleitor"].ToString();
                    obj.Zona_Eleitoral = reader["Zona_Eleitoral"].ToString();
                    obj.Secao_Eleitoral = reader["Secao_Eleitoral"].ToString();
                    obj.Nome_Pai = reader["Nome_Pai"].ToString();
                    obj.Nome_Mae = reader["Nome_Mae"].ToString();
                    obj.End_Res = reader["End_Res"].ToString();
                    obj.Bairro_Res = reader["Bairro_Res"].ToString();
                    obj.Cidade_Res = reader["Cidade_Res"].ToString();
                    obj.Uf_Res = reader["Uf_Res"].ToString();
                    obj.Cep_Res = reader["Cep_Res"].ToString();
                    obj.cx_postal = reader["cx_postal"].ToString();
                    obj.celular = reader["celular"].ToString();
                    obj.tel_res = reader["tel_res"].ToString();
                    obj.fax = reader["fax"].ToString();
                    obj.obs = reader["obs"].ToString();
                    obj.correspondencia_end = reader["correspondencia_end"].ToString();
                    //   obj.foto = reader["foto"].ToString();

                    //if (obj.Situacao == "Em Loja") obj.Situacao = "1";
                    //if (obj.Situacao == "Adormecido") obj.Situacao = "2";

                    listaMacon.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Macon obj2 = new Macon();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaMacon;
        }
        public List<Macon> FindPorGuide(Macon obj1)
        {
            List<Macon> listaMacon = new List<Macon>();
            string find = "select*from Macon where Guide = '" + obj1.Guide + "' order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Macon obj = new Macon();
                    obj.Id_Macon = Convert.ToInt32(reader["Id_Macon"].ToString());
                    obj.Cim = reader["Cim"].ToString();
                    obj.Id_Loja = Convert.ToInt32(reader["Id_Loja"].ToString());
                    obj.Id_Graduacao = Convert.ToInt32(reader["Id_Graduacao"].ToString());
                    obj.Id_Cargo = Convert.ToInt32(reader["Id_Cargo"].ToString());
                    obj.Id_Condecoracao = Convert.ToInt32(reader["Id_Condecoracao"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    obj.Guide = reader["Guide"].ToString();
                    obj.Grau = reader["Grau"].ToString();
                    obj.Nome_Tratamento = reader["Nome_Tratamento"].ToString();
                    obj.Senha = reader["Senha"].ToString();
                    obj.Situacao = reader["Situacao"].ToString();
                    obj.CPF = reader["CPF"].ToString();
                    obj.Login = reader["Login"].ToString();
                    obj.Dt_Iniciado = Convert.ToDateTime(reader["Dt_Iniciado"].ToString());
                    obj.Email = reader["Email"].ToString();
                    obj.Email_Outros = reader["Email_Outros"].ToString();
                    if ((reader["Dt_Nascimento"].ToString()) != "" || (reader["Dt_Nascimento"].ToString()) == null) obj.Dt_Nascimento = Convert.ToDateTime(reader["Dt_Nascimento"].ToString());
                    if ((reader["Dt_Iniciado"].ToString()) != "" || (reader["Dt_Iniciado"].ToString()) == null) obj.Dt_Iniciado = Convert.ToDateTime(reader["Dt_Iniciado"].ToString());
                    if ((reader["Data_Casamento"].ToString()) != "" || (reader["Data_Casamento"].ToString()) == null) obj.Data_Casamento = Convert.ToDateTime(reader["Data_Casamento"].ToString());
                    if ((reader["Dt_Emissao_Di"].ToString()) != "" || (reader["Dt_Emissao_Di"].ToString()) == null) obj.Dt_Emissao_Di = Convert.ToDateTime(reader["Dt_Emissao_Di"].ToString());

               //     obj.Dt_Nascimento = reader["Dt_Nascimento"].ToString();
                    obj.Naturalidade = reader["Naturalidade"].ToString();
                    obj.Uf = reader["Uf"].ToString();
                    obj.Nacionalidade = reader["Nacionalidade"].ToString();
                    obj.Religiao = reader["Religiao"].ToString();
                    obj.Escolaridade = reader["Escolaridade"].ToString();
                    obj.Sexo = reader["Sexo"].ToString();
                    obj.Tipo_Sanguíneo = reader["Tipo_Sanguíneo"].ToString();
                    obj.Estado_Civil = reader["Estado_Civil"].ToString();
                 //   obj.Data_Casamento = reader["Data_Casamento"].ToString();
                    obj.Tipo_Di = reader["Tipo_Di"].ToString();
                    obj.Num_Di = reader["Num_Di"].ToString();
                    obj.Emissor_Di = reader["Emissor_Di"].ToString();
                 //   obj.Dt_Emissao_Di = reader["Dt_Emissao_Di"].ToString();
                    obj.Uf_Di = reader["Uf_Di"].ToString();
                    obj.Tit_Eleitor = reader["Tit_Eleitor"].ToString();
                    obj.Zona_Eleitoral = reader["Zona_Eleitoral"].ToString();
                    obj.Secao_Eleitoral = reader["Secao_Eleitoral"].ToString();
                    obj.Nome_Pai = reader["Nome_Pai"].ToString();
                    obj.Nome_Mae = reader["Nome_Mae"].ToString();
                    obj.End_Res = reader["End_Res"].ToString();
                    obj.Bairro_Res = reader["Bairro_Res"].ToString();
                    obj.Cidade_Res = reader["Cidade_Res"].ToString();
                    obj.Uf_Res = reader["Uf_Res"].ToString();
                    obj.Cep_Res = reader["Cep_Res"].ToString();
                    obj.cx_postal = reader["cx_postal"].ToString();
                    obj.celular = reader["celular"].ToString();
                    obj.tel_res = reader["tel_res"].ToString();
                    obj.fax = reader["fax"].ToString();
                    obj.obs = reader["obs"].ToString();
                    obj.correspondencia_end = reader["correspondencia_end"].ToString();
                    //   obj.foto = reader["foto"].ToString();
                    listaMacon.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Macon obj2 = new Macon();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaMacon;
        }
        public List<Macon> PesquisarGuide(string usuario)
        {
            List<Macon> listaMacon = new List<Macon>();
            string find = "select*from users where username = '" + usuario + "' ";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Macon obj = new Macon();
                    obj.Guide = reader["UserId"].ToString();
                    obj.Grau = reader["UserType"].ToString();
                    listaMacon.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Macon obj2 = new Macon();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaMacon;
        }


        public List<Macon> findAll()
        {
            List<Macon> listaMacon = new List<Macon>();
            string find = "select * from Macon order by Nome asc";
            try
            {
                int Id_Macon = 0;
                int Cim = 0;
                int Id_Loja = 0;
                int Id_Graduacao = 0;
                int Id_Cargo = 0;
                int Id_Condecoracao = 0;

                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Macon obj = new Macon();
                    if (reader["Id_Macon"].ToString() == "") Id_Macon = 0;
                    if (reader["Cim"].ToString() == "") Cim = 0; ;
                    if (reader["Id_Loja"].ToString() == "") Id_Loja = 0; ;
                    if (reader["Id_Graduacao"].ToString() == "") Id_Graduacao = 0; ;
                    if (reader["Id_Cargo"].ToString() == "") Id_Cargo = 0; ;
                    if (reader["Id_Condecoracao"].ToString() == "") Id_Condecoracao = 0; ;

                    if (reader["Id_Macon"].ToString() != "") Id_Macon = Convert.ToInt32(reader["Id_Macon"].ToString());
                    if (reader["Id_Loja"].ToString() != "") Id_Loja = Convert.ToInt32(reader["Id_Loja"].ToString());
                    if (reader["Id_Graduacao"].ToString() != "") Convert.ToInt32(reader["Id_Graduacao"].ToString());
                    if (reader["Id_Cargo"].ToString() != "") Id_Cargo = Convert.ToInt32(reader["Id_Cargo"].ToString());
                    if (reader["Id_Condecoracao"].ToString() != "") Id_Condecoracao = Convert.ToInt32(reader["Id_Condecoracao"].ToString());

                    obj.Id_Graduacao = Convert.ToInt32(reader["Id_Graduacao"].ToString());
                    obj.Id_Macon = Id_Macon;
                    //          obj.Cim = Cim;
                    obj.Id_Loja = Id_Loja;
                    obj.Id_Cargo = Id_Cargo;
                    obj.Id_Condecoracao = Id_Condecoracao;
                    obj.Nome = reader["Nome"].ToString();
                    obj.Guide = reader["Guide"].ToString();

                    obj.Cim = reader["Cim"].ToString();
                    //         obj.Dt_Iniciado = Convert.ToDateTime(reader["Dt_Iniciado"].ToString());
                    if ((reader["Dt_Nascimento"].ToString()) != "" || (reader["Dt_Nascimento"].ToString()) == null) obj.Dt_Nascimento = Convert.ToDateTime(reader["Dt_Nascimento"].ToString());
                    if ((reader["Dt_Iniciado"].ToString()) != "" || (reader["Dt_Iniciado"].ToString()) == null) obj.Dt_Iniciado = Convert.ToDateTime(reader["Dt_Iniciado"].ToString());
                    if ((reader["Data_Casamento"].ToString()) != "" || (reader["Data_Casamento"].ToString()) == null) obj.Data_Casamento = Convert.ToDateTime(reader["Data_Casamento"].ToString());
                    if ((reader["Dt_Emissao_Di"].ToString()) != "" || (reader["Dt_Emissao_Di"].ToString()) == null) obj.Dt_Emissao_Di = Convert.ToDateTime(reader["Dt_Emissao_Di"].ToString());

                    //obj.Dt_Nascimento = reader["Dt_Nascimento"].ToString();
                    //obj.Data_Casamento = reader["Data_Casamento"].ToString();
                    //obj.Dt_Emissao_Di = reader["Dt_Emissao_Di"].ToString();

                    obj.Emissor_Di = reader["Emissor_Di"].ToString();

                    obj.Grau = reader["Grau"].ToString();
                    obj.Nome_Tratamento = reader["Nome_Tratamento"].ToString();
                    obj.Senha = reader["Senha"].ToString();
                    obj.Situacao = reader["Situacao"].ToString();
                    obj.CPF = reader["CPF"].ToString();
                    obj.Login = reader["Login"].ToString();
                    obj.Email = reader["Email"].ToString();
                    obj.Email_Outros = reader["Email_Outros"].ToString();

                    obj.Naturalidade = reader["Naturalidade"].ToString();
                    obj.Uf = reader["Uf"].ToString();
                    obj.Nacionalidade = reader["Nacionalidade"].ToString();
                    obj.Religiao = reader["Religiao"].ToString();
                    obj.Escolaridade = reader["Escolaridade"].ToString();
                    obj.Sexo = reader["Sexo"].ToString();
                    obj.Tipo_Sanguíneo = reader["Tipo_Sanguíneo"].ToString();
                    obj.Estado_Civil = reader["Estado_Civil"].ToString();

                    obj.Tipo_Di = reader["Tipo_Di"].ToString();
                    obj.Num_Di = reader["Num_Di"].ToString();
                    obj.Uf_Di = reader["Uf_Di"].ToString();
                    obj.Tit_Eleitor = reader["Tit_Eleitor"].ToString();
                    obj.Zona_Eleitoral = reader["Zona_Eleitoral"].ToString();
                    obj.Secao_Eleitoral = reader["Secao_Eleitoral"].ToString();
                    obj.Nome_Pai = reader["Nome_Pai"].ToString();
                    obj.Nome_Mae = reader["Nome_Mae"].ToString();
                    obj.End_Res = reader["End_Res"].ToString();
                    obj.Bairro_Res = reader["Bairro_Res"].ToString();
                    obj.Cidade_Res = reader["Cidade_Res"].ToString();
                    obj.Uf_Res = reader["Uf_Res"].ToString();
                    obj.Cep_Res = reader["Cep_Res"].ToString();
                    obj.cx_postal = reader["cx_postal"].ToString();
                    obj.celular = reader["celular"].ToString();
                    obj.tel_res = reader["tel_res"].ToString();
                    obj.fax = reader["fax"].ToString();
                    obj.obs = reader["obs"].ToString();
                    obj.correspondencia_end = reader["correspondencia_end"].ToString();
                    //  obj.foto = reader["foto"].ToString();
                    listaMacon.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Macon obj2 = new Macon();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaMacon;
        }

        public List<Macon> findAllMacon(Macon objMacon)
        {
            List<Macon> listaMacon = new List<Macon>();
            string findAll = "select* from Macon where Nome like '%" + objMacon.Nome + "%' or Nome_Tratamento like '%" + objMacon.Nome_Tratamento + "%' or Senha like '%" + objMacon.Senha + "%' or Situacao like '%" + objMacon.Situacao + "%' or CPF like '%" + objMacon.CPF + "%' or Login like '%" + objMacon.Login + "%' or Email like '%" + objMacon.Email + "%' or Email_Outros like '%" + objMacon.Email_Outros + "%' or Naturalidade like '%" + objMacon.Naturalidade + "%' or Uf like '%" + objMacon.Uf + "%' or Nacionalidade like '%" + objMacon.Nacionalidade + "%' or Religiao like '%" + objMacon.Religiao + "%' or Escolaridade like '%" + objMacon.Escolaridade + "%' or Sexo like '%" + objMacon.Sexo + "%' or Tipo_Sanguíneo like '%" + objMacon.Tipo_Sanguíneo + "%' or Estado_Civil like '%" + objMacon.Estado_Civil + "%' or Tipo_Di like '%" + objMacon.Tipo_Di + "%' or Num_Di like '%" + objMacon.Num_Di + "%' or Emissor_Di like '%" + objMacon.Emissor_Di + "%' or Uf_Di like '%" + objMacon.Uf_Di + "%' or Tit_Eleitor like '%" + objMacon.Tit_Eleitor + "%' or Zona_Eleitoral like '%" + objMacon.Zona_Eleitoral + "%' or Secao_Eleitoral like '%" + objMacon.Secao_Eleitoral + "%' or Nome_Pai like '%" + objMacon.Nome_Pai + "%' or Nome_Mae like '%" + objMacon.Nome_Mae + "%' or End_Res like '%" + objMacon.End_Res + "%' or Bairro_Res like '%" + objMacon.Bairro_Res + "%' or Cidade_Res like '%" + objMacon.Cidade_Res + "%' or Uf_Res like '%" + objMacon.Uf_Res + "%' or Cep_Res like '%" + objMacon.Cep_Res + "%' or cx_postal like '%" + objMacon.cx_postal + "%' or celular like '%" + objMacon.celular + "%' or tel_res like '%" + objMacon.tel_res + "%' or fax like '%" + objMacon.fax + "%' or obs like '%" + objMacon.obs + "%' or correspondencia_end like '%" + objMacon.correspondencia_end + "%' ";
            try
            {
                int Id_Macon = 0;
                int Cim = 0;
                int Id_Loja = 0;
                int Id_Graduacao = 0;
                int Id_Cargo = 0;
                int Id_Condecoracao = 0;

                comando = new SqlCommand(findAll, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["Id_Macon"].ToString() == "") Id_Macon = 0;
                    if (reader["Cim"].ToString() == "") Cim = 0; ;
                    if (reader["Id_Loja"].ToString() == "") Id_Loja = 0; ;
                    if (reader["Id_Graduacao"].ToString() == "") Id_Graduacao = 0; ;
                    if (reader["Id_Cargo"].ToString() == "") Id_Cargo = 0; ;
                    if (reader["Id_Condecoracao"].ToString() == "") Id_Condecoracao = 0; ;

                    if (reader["Id_Macon"].ToString() != "") Id_Macon = Convert.ToInt32(reader["Id Id_Macon"].ToString());
                    if (reader["Cim"].ToString() != "") Cim = Convert.ToInt32(reader["Cim"].ToString());
                    if (reader["Id_Loja"].ToString() != "") Id_Loja = Convert.ToInt32(reader["Id_Loja"].ToString());
                    if (reader["Id_Graduacao"].ToString() != "") Convert.ToInt32(reader["Id_Graduacao"].ToString());
                    if (reader["Id_Cargo"].ToString() != "") Id_Cargo = Convert.ToInt32(reader["Id_Cargo"].ToString());
                    if (reader["Id_Condecoracao"].ToString() != "") Id_Condecoracao = Convert.ToInt32(reader["Id_Condecoracao"].ToString());

                    Macon obj = new Macon();
                    obj.Id_Macon = Id_Macon;// Convert.ToInt32(reader["Id Id_Macon"].ToString();
                    obj.Cim = reader["Cim"].ToString();
                    obj.Id_Loja = Id_Loja;// Convert.ToInt32(reader["Id_Loja"].ToString();
                    obj.Id_Graduacao = Id_Graduacao;// Convert.ToInt32(reader["Id_Graduacao"].ToString();
                    obj.Id_Cargo = Id_Cargo;// Convert.ToInt32(reader["Id_Cargo"].ToString();
                    obj.Id_Condecoracao = Id_Condecoracao;// Convert.ToInt32(reader["Id_Condecoracao"].ToString();
                    obj.Nome = reader["Nome"].ToString();
                    obj.Guide = reader["Guide"].ToString();
                    obj.Grau = reader["Grau"].ToString();
                    obj.Nome_Tratamento = reader["Nome_Tratamento"].ToString();
                    obj.Senha = reader["Senha"].ToString();
                    obj.Situacao = reader["Situacao"].ToString();
                    obj.Dt_Iniciado = Convert.ToDateTime(reader["Dt_Iniciado"].ToString());
                    obj.CPF = reader["CPF"].ToString();
                    obj.Login = reader["Login"].ToString();
                    obj.Email = reader["Email"].ToString();
                    obj.Email_Outros = reader["Email_Outros"].ToString();
                    if ((reader["Dt_Nascimento"].ToString()) != "" || (reader["Dt_Nascimento"].ToString()) == null) obj.Dt_Nascimento = Convert.ToDateTime(reader["Dt_Nascimento"].ToString());
                    if ((reader["Dt_Iniciado"].ToString()) != "" || (reader["Dt_Iniciado"].ToString()) == null) obj.Dt_Iniciado = Convert.ToDateTime(reader["Dt_Iniciado"].ToString());
                    if ((reader["Data_Casamento"].ToString()) != "" || (reader["Data_Casamento"].ToString()) == null) obj.Data_Casamento = Convert.ToDateTime(reader["Data_Casamento"].ToString());
                    if ((reader["Dt_Emissao_Di"].ToString()) != "" || (reader["Dt_Emissao_Di"].ToString()) == null) obj.Dt_Emissao_Di = Convert.ToDateTime(reader["Dt_Emissao_Di"].ToString());

             //       obj.Dt_Nascimento = reader["Dt_Nascimento"].ToString();
                    obj.Naturalidade = reader["Naturalidade"].ToString();
                    obj.Uf = reader["Uf"].ToString();
                    obj.Nacionalidade = reader["Nacionalidade"].ToString();
                    obj.Religiao = reader["Religiao"].ToString();
                    obj.Escolaridade = reader["Escolaridade"].ToString();
                    obj.Sexo = reader["Sexo"].ToString();
                    obj.Tipo_Sanguíneo = reader["Tipo_Sanguíneo"].ToString();
                    obj.Estado_Civil = reader["Estado_Civil"].ToString();
               //     obj.Data_Casamento = reader["Data_Casamento"].ToString();
                    obj.Tipo_Di = reader["Tipo_Di"].ToString();
                    obj.Num_Di = reader["Num_Di"].ToString();
                    obj.Emissor_Di = reader["Emissor_Di"].ToString();
               //     obj.Dt_Emissao_Di = reader["Dt_Emissao_Di"].ToString();
                    obj.Uf_Di = reader["Uf_Di"].ToString();
                    obj.Tit_Eleitor = reader["Tit_Eleitor"].ToString();
                    obj.Zona_Eleitoral = reader["Zona_Eleitoral"].ToString();
                    obj.Secao_Eleitoral = reader["Secao_Eleitoral"].ToString();
                    obj.Nome_Pai = reader["Nome_Pai"].ToString();
                    obj.Nome_Mae = reader["Nome_Mae"].ToString();
                    obj.End_Res = reader["End_Res"].ToString();
                    obj.Bairro_Res = reader["Bairro_Res"].ToString();
                    obj.Cidade_Res = reader["Cidade_Res"].ToString();
                    obj.Uf_Res = reader["Uf_Res"].ToString();
                    obj.Cep_Res = reader["Cep_Res"].ToString();
                    obj.cx_postal = reader["cx_postal"].ToString();
                    obj.celular = reader["celular"].ToString();
                    obj.tel_res = reader["tel_res"].ToString();
                    obj.fax = reader["fax"].ToString();
                    obj.obs = reader["obs"].ToString();
                    obj.correspondencia_end = reader["correspondencia_end"].ToString();
                    // obj.foto = reader["foto"].ToString();
                    listaMacon.Add(obj);

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

            return listaMacon;

        }

        public List<Macon> findMaconPorId(Macon objMacon)
        {
            List<Macon> listaMacon = new List<Macon>();
            string findAll = "select * from Macon where Id_Macon = '" + objMacon.Id_Macon + "' ";
            try
            {

                comando = new SqlCommand(findAll, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Macon obj = new Macon();
                    obj.Id_Macon = Convert.ToInt32(reader["Id Id_Macon"].ToString());
                    obj.Cim = reader["Cim"].ToString();
                    obj.Id_Loja = Convert.ToInt32(reader["Id_Loja"].ToString());
                    obj.Id_Graduacao = Convert.ToInt32(reader["Id_Graduacao"].ToString());
                    obj.Id_Cargo = Convert.ToInt32(reader["Id_Cargo"].ToString());
                    obj.Id_Condecoracao = Convert.ToInt32(reader["Id_Condecoracao"].ToString());
                    obj.Guide = reader["Guide"].ToString();
                    obj.Nome = reader["Nome"].ToString();
                    obj.Grau = reader["Grau"].ToString();
                    obj.Nome_Tratamento = reader["Nome_Tratamento"].ToString();
                    obj.Senha = reader["Senha"].ToString();
                    obj.Situacao = reader["Situacao"].ToString();
                    obj.CPF = reader["CPF"].ToString();
                    obj.Dt_Iniciado = Convert.ToDateTime(reader["Dt_Iniciado"].ToString());
                    obj.Login = reader["Login"].ToString();
                    obj.Email = reader["Email"].ToString();
                    obj.Email_Outros = reader["Email_Outros"].ToString();
                    if ((reader["Dt_Nascimento"].ToString()) != "" || (reader["Dt_Nascimento"].ToString()) == null) obj.Dt_Nascimento = Convert.ToDateTime(reader["Dt_Nascimento"].ToString());
                    if ((reader["Dt_Iniciado"].ToString()) != "" || (reader["Dt_Iniciado"].ToString()) == null) obj.Dt_Iniciado = Convert.ToDateTime(reader["Dt_Iniciado"].ToString());
                    if ((reader["Data_Casamento"].ToString()) != "" || (reader["Data_Casamento"].ToString()) == null) obj.Data_Casamento = Convert.ToDateTime(reader["Data_Casamento"].ToString());
                    if ((reader["Dt_Emissao_Di"].ToString()) != "" || (reader["Dt_Emissao_Di"].ToString()) == null) obj.Dt_Emissao_Di = Convert.ToDateTime(reader["Dt_Emissao_Di"].ToString());

              //      obj.Dt_Nascimento = reader["Dt_Nascimento"].ToString();
                    obj.Naturalidade = reader["Naturalidade"].ToString();
                    obj.Uf = reader["Uf"].ToString();
                    obj.Nacionalidade = reader["Nacionalidade"].ToString();
                    obj.Religiao = reader["Religiao"].ToString();
                    obj.Escolaridade = reader["Escolaridade"].ToString();
                    obj.Sexo = reader["Sexo"].ToString();
                    obj.Tipo_Sanguíneo = reader["Tipo_Sanguíneo"].ToString();
                    obj.Estado_Civil = reader["Estado_Civil"].ToString();
              //      obj.Data_Casamento = reader["Data_Casamento"].ToString();
                    obj.Tipo_Di = reader["Tipo_Di"].ToString();
                    obj.Num_Di = reader["Num_Di"].ToString();
                    obj.Emissor_Di = reader["Emissor_Di"].ToString();
             //       obj.Dt_Emissao_Di = reader["Dt_Emissao_Di"].ToString();
                    obj.Uf_Di = reader["Uf_Di"].ToString();
                    obj.Tit_Eleitor = reader["Tit_Eleitor"].ToString();
                    obj.Zona_Eleitoral = reader["Zona_Eleitoral"].ToString();
                    obj.Secao_Eleitoral = reader["Secao_Eleitoral"].ToString();
                    obj.Nome_Pai = reader["Nome_Pai"].ToString();
                    obj.Nome_Mae = reader["Nome_Mae"].ToString();
                    obj.End_Res = reader["End_Res"].ToString();
                    obj.Bairro_Res = reader["Bairro_Res"].ToString();
                    obj.Cidade_Res = reader["Cidade_Res"].ToString();
                    obj.Uf_Res = reader["Uf_Res"].ToString();
                    obj.Cep_Res = reader["Cep_Res"].ToString();
                    obj.cx_postal = reader["cx_postal"].ToString();
                    obj.celular = reader["celular"].ToString();
                    obj.tel_res = reader["tel_res"].ToString();
                    obj.fax = reader["fax"].ToString();
                    obj.obs = reader["obs"].ToString();
                    obj.correspondencia_end = reader["correspondencia_end"].ToString();
                    //   obj.foto = reader["foto"].ToString();
                    listaMacon.Add(obj);

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

            return listaMacon;

        }

        public List<Situacao> ListaSituacao()
        {
            List<Situacao> listaMacon = new List<Situacao>();
            string find = "select * from Situacao order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Situacao obj = new Situacao();
                    obj.Id = Convert.ToInt32(reader["Id"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    listaMacon.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Macon obj2 = new Macon();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaMacon;
        }
        public List<Tipo_Sanguíneo> ListaTipoSangue()
        {
            List<Tipo_Sanguíneo> listaMacon = new List<Tipo_Sanguíneo>();
            string find = "select * from TipoSangue order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Tipo_Sanguíneo obj = new Tipo_Sanguíneo();
                    obj.Id = Convert.ToInt32(reader["Id"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    listaMacon.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Macon obj2 = new Macon();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaMacon;
        }
        public List<Sexo> ListaSexo()
        {
            List<Sexo> listaMacon = new List<Sexo>();
            string find = "select * from Sexo order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Sexo obj = new Sexo();
                    obj.Id = Convert.ToInt32(reader["Id"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    listaMacon.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Macon obj2 = new Macon();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaMacon;
        }
        public List<Estado_Civil> ListaEstado_Civil()
        {
            List<Estado_Civil> listaMacon = new List<Estado_Civil>();
            string find = "select * from Civil order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Estado_Civil obj = new Estado_Civil();
                    obj.Id = Convert.ToInt32(reader["Id"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    listaMacon.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Macon obj2 = new Macon();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaMacon;
        }
        public List<Escolaridade> ListaEscolaridade()
        {
            List<Escolaridade> listaMacon = new List<Escolaridade>();
            string find = "select * from Escolaridade order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Escolaridade obj = new Escolaridade();
                    obj.Id = Convert.ToInt32(reader["Id"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    listaMacon.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Macon obj2 = new Macon();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaMacon;
        }
        public List<Religiao> ListaReligiao()
        {
            List<Religiao> listaMacon = new List<Religiao>();
            string find = "select * from Religiao order by Nome asc";
            try
            {
                comando = new SqlCommand(find, objConexaoDB.getCon());
                objConexaoDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Religiao obj = new Religiao();
                    obj.Id = Convert.ToInt32(reader["Religiao"].ToString());
                    obj.Nome = reader["Nome"].ToString();
                    listaMacon.Add(obj);
                }

            }
            catch (Exception ex)
            {
                Macon obj2 = new Macon();
                obj2.Estados = 1000;
            }
            finally
            {
                objConexaoDB.getCon().Close();
                objConexaoDB.CloseDB();
            }

            return listaMacon;
        }

    }

}
