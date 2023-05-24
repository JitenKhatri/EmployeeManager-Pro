using test_coreWebApplication.Entities.Entities;

namespace test_coreWebApplication.DataAccess.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        string InsertData(Employee objemp);
        string UpdateData(Employee objemp);
        int DeleteData(String ID);
        List<Employee> Selectalldata();
        Employee SelectDatabyID(string EmployeeId);
        void LogError(Exception ex);
        IEnumerable<Country> GetallCountries();
        IEnumerable<City> GetCityByCountry(long CountryId);

    }
}
