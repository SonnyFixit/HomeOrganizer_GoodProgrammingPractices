using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeOrganizer.Models.Communication
{
    /// <summary>
    /// Represents a response from an operation, including a status and a message.
    /// </summary>
    public struct Response
    {
        // Indicates the success or failure of the operation.
        public bool Status { get; set; }

        // Provides additional information about the operation's outcome.
        public string Message { get; set; }

        /// <summary>
        /// Constructor for creating a new Response.
        /// </summary>
        /// <param name="status">The status of the operation (true for success, false for failure).</param>
        /// <param name="message">The message providing more details about the operation's outcome.</param>
        public Response(bool status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
