using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace PasswordManagement.DatabaseBuilder
{
    public class Database
    {
        public async Task Build<T>(bool deleteExisting, params Assembly[] assemblies) where T : class
        {
            BuilderContext<T> context = new BuilderContext<T>(assemblies ,"MARS", "PasswordManagement");
            
            if (deleteExisting)
            {
                await context.Database.EnsureDeletedAsync();
            }

            await context.Database.EnsureCreatedAsync();
            await context.SaveChangesAsync();
        }
    }
}