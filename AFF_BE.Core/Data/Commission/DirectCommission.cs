using AFF_BE.Core.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Data.Commission
{
    public class DirectCommission : EntityBase<int>
    {

        public int UserId {get; set; }

        public double Amount {get; set; }

        public virtual User User {get; set; }
    }
}
