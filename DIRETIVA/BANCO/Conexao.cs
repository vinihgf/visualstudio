namespace BANCO
{
    public class Conexao
    {
        public static string SERVER = "";
        public static string USER = "";
        public static string SENHA = "";
        public static string BANCO = "";
        public static string PORTA = "";
        public static string CONEXAO = "";

        protected static string montaDAO(string CONEXAO)
        {
            return CONEXAO = "Server=" + SERVER + ";Port=" + PORTA + ";User Id=" + USER + ";Password=" + SENHA + ";Database=" + BANCO;
        }
    }
}