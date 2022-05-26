namespace Krita.Data.Repositories
{
    public class BaseRepository
    {
        private readonly Connection _connection;
        private SqlCommand _command;

        public BaseRepository(Connection connection)
        {
            _connection = connection;
        }

        protected void SetProcedure(object procedureName)
        {
            _command = new SqlCommand(procedureName.ToString(), _connection.connection);
            _command.CommandType = CommandType.StoredProcedure;
        }

        protected void AddParameter(string name, object value)
        {
            _command.Parameters.AddWithValue("@" + name, value);
        }

        protected int ExecuteNonQuery()
        {
            _connection.OpenConnection();
            return _command.ExecuteNonQuery();
        }

        protected SqlDataReader ExecuteReader()
        {
            _connection.OpenConnection();
            return _command.ExecuteReader();
        }

        protected void Close()
        {
            _connection.Dispose();
        }
    }
}
