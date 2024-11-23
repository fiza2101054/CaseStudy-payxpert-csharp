using Microsoft.Extensions.Configuration;
using Payxpert.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payxpert.Utility
{
    internal class DBConnUtil
    {

            private static IConfiguration _iconfiguration;

            static DBConnUtil()
            {
                GetAppSettingsFile();
            }

            private static void GetAppSettingsFile()
            {
                var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json");
                _iconfiguration = builder.Build();
            }

            public static string GetConnectionString()
            {
            try
            {
                return _iconfiguration.GetConnectionString("LocalConnectionString");
            }
            catch (Exception ex) { 
 
                throw new DatabaseConnectionException($"Error retrieving connection string: {ex.Message}");
            }
        }
        }
}
