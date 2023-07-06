using Npgsql;

namespace NpgSQLConnectionWrapper.UserTools
{
    public abstract class DbContext : IDisposable
    {
        public bool ConnectionEstablished { get; private set; }

        private NpgsqlConnection Connection;
        public DbContext(string connectionString)
        {
            ConnectionEstablished = false;
            Connection = new NpgsqlConnection(connectionString);
        }

        public async Task<TRes> RunQueryAsync<TRes>(Func<NpgsqlConnection, Task<TRes>> command)
        {
            if (!ConnectionEstablished)
            {
                Connection.Open();
                ConnectionEstablished = true;
            }
            TRes res;
            NpgsqlTransaction transaction = await Connection.BeginTransactionAsync();
            res = await command(transaction.Connection);
            await transaction.CommitAsync();
            return res;
        }

        public TRes RunQuery<TRes>(Func<NpgsqlConnection, TRes> command)
        {
            if (!ConnectionEstablished)
            {
                Connection.Open();
                ConnectionEstablished = true;
            }
            TRes res;
            NpgsqlTransaction transaction = Connection.BeginTransaction();
            res = command(transaction.Connection);
            transaction.Commit();
            return res;
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
