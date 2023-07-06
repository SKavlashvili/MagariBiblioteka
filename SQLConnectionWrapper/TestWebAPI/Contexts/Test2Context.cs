using NpgSQLConnectionWrapper.UserTools;

namespace TestWebAPI.Contexts
{
    public class Test2Context : DbContext
    {
        public Test2Context() : base("User ID=postgres;Password=Europe1234;Host=localhost;Port=5432;Database=CampaignsDB;Pooling=true;Minimum Pool Size=0;Maximum Pool Size=30;Connection Lifetime=0;Timeout = 100")
        {

        }
    }
}
