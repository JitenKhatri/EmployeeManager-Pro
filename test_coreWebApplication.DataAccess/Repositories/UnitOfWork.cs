using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_coreWebApplication.DataAccess.Repositories.Interfaces;

namespace test_coreWebApplication.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration configuration;
        public UnitOfWork(IConfiguration _configuration)
        {
            configuration = _configuration;
            EmployeeRepository = new EmployeeRepository(configuration);
        }
        public IEmployeeRepository EmployeeRepository { get; private set; }
    }
}
