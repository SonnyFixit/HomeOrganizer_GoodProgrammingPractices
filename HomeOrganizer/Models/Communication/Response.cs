using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeOrganizer.Models.Communication
{
    public struct Response
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public Response(bool status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
