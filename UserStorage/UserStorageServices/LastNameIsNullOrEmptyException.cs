using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class LastNameIsNullOrEmptyException : Exception
    {
        public LastNameIsNullOrEmptyException(string message) : base(message)
        {
        }
    }
}