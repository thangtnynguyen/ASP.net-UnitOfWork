using AFF_BE.Core.Models.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.Data.Payment;

namespace AFF_BE.Core.Data.Commission
{
    public class IndirectCommission : EntityBase<int>
    {

        public int OrderId { get; set; }

        public string Code { get; set; }

        public double Price { get; set; }

        public double Commission { get; set; }

        public int UserBuyId {  get; set; }

        public int UserReciveId { get; set; }

        public int BuyNumber { get; set; }

        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual User UserRecive { get; set; }


    }
}
