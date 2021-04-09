using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginRegister.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateTıme { get; set; } = DateTime.Now;

    }
}
