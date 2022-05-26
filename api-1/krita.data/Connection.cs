namespace Krita.Data
{
    public class Connection : IDisposable
    {
        public readonly SqlConnection connection;

        public Connection(IConfiguration configuration)
        {
            connection = new SqlConnection("Data Source=192.168.7.12;Initial Catalog=GestaoSMNHomolog;Persist Security Info=True;User ID=userSMN;Password=smn2022@!");
        }

        public void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }

        public void Dispose()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
    }
}
