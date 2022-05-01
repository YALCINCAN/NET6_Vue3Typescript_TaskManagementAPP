using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.SeriLog.ConfigurationModels
{
    public class PostgreSqlConfiguration
    {
        public string ConnectionString { get; set; }
    }
}
