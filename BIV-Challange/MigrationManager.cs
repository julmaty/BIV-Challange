using Microsoft.EntityFrameworkCore;

namespace BIV_Challange
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetRequiredService<ApplicationContext>();
                    db.Database.Migrate();
                }
                catch
                {
                    //log errors or ...
                    throw;
                }
            }
            return host;
        }
    }
}
