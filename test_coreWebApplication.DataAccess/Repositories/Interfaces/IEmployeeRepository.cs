using test_coreWebApplication.Entities.Entities;

namespace test_coreWebApplication.DataAccess.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        
        List<Employee> Selectalldata();
        
        void LogError(Exception ex);
        

    }
}
