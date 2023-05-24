using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_coreWebApplication.DataAccess.Repositories.Interfaces;

namespace test_coreWebApplication.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
    }
}
