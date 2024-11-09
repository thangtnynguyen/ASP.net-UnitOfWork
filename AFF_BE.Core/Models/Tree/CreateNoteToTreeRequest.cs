using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.Models.Tree
{
    public class CreateNoteToTreeRequest
    {

        public int UserId { get; set; }

        public int? InviterId { get; set; }

    }
}
