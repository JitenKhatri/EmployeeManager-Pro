using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_coreWebApplication.Entities.Entities
{
    public class City
    {
        [Key]
        public long CityId { get; set; }

        public string CityName { get; set; }

        public long CountryId { get; set; }
    }
}
