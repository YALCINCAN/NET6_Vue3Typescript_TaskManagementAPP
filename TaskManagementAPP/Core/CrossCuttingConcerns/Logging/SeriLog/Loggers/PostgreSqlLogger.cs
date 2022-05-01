using Core.CrossCuttingConcerns.Logging.SeriLog.ConfigurationModels;
using Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.SeriLog.Loggers
{
    public class PostgreSqlLogger:LoggerServiceBase
    {
        public PostgreSqlLogger()
        {
			var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

			var logConfig = configuration.GetSection("SeriLogConfigurations:PostgreSqlConfiguration")
					.Get<PostgreSqlConfiguration>() ?? throw new Exception("PostgreSQLConnectionString is null");
			var seriLogConfig = new LoggerConfiguration()
										.WriteTo.PostgreSQL(connectionString: logConfig.ConnectionString,tableName:"Logs",needAutoCreateTable:true)
										.CreateLogger();
			Logger = seriLogConfig;
		}
    }
}
