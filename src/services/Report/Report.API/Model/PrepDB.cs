using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Report.API.Model
{
    public class PrepDB
    {
        public static void PrepDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                PrepareDatabase(serviceScope.ServiceProvider.GetService<ModelContext>());
            }

        }
        public static void PrepareDatabase(ModelContext context)
        {
            Console.Write("Applying Migrations");
            context.Database.Migrate();
        }
    }
}
