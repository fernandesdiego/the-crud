using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheCRUD.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public Entity()
        {

        }
    }
}
