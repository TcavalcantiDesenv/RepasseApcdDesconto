using System.Data.SqlClient;

namespace Model.Dao
{
    public class ConexaoDB
    {
        private static ConexaoDB objConexaoDB = null;
        private SqlConnection con;

        private ConexaoDB()
        {
            //CONEXAO NO SERVIDOR Legisig
            con = new SqlConnection("Data Source=sql5041.site4now.net;packet size=4096;user id=DB_A1938F_APCDADM_admin;pwd=4rtr4x2308apcd;persist security info=False;initial catalog=DB_A1938F_APCDADM");
         //   con = new SqlConnection("Data Source=10.1.0.2;packet size=4096;user id=desenv_sap;pwd=Des20@20sap;persist security info=False;initial catalog=REPASSE_SAP");

        }

        public static ConexaoDB saberEstado()
        {
            if (objConexaoDB == null)
            {
                objConexaoDB = new ConexaoDB();
            }

            return objConexaoDB;
        }


        public SqlConnection getCon()
        {
            return con;
        }

        public void CloseDB()
        {
            objConexaoDB = null;
        }
    }
}
