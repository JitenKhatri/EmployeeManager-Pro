using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_coreWebApplication.Entities.Entities
{
    public class Country
    {
        [Key]
        public long CountryId { get; set; }

        public string CountryName { get; set; }
    }
}
