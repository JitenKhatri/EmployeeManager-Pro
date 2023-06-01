using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_coreWebApplication.Entities.InputModels
{
    public class GetEmpInput
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string SearchValue { get; set; } = string.Empty;
        public string SortColumn { get; set; } = string.Empty;
        public string SortColumnDirection { get; set; } = string.Empty;
        public string EmailSearch { get; set; } = string.Empty;
        public string EmployeeIdSearch { get; set; } = string.Empty;
        public string AddressSearch { get; set; } = string.Empty;
        public string MobileNoSearch { get; set; } = string.Empty;
        public string EmployeeNameSearch { get; set; } = string.Empty;
    }
}
