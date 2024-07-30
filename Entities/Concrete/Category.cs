using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class Category:IEntity
    {
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
