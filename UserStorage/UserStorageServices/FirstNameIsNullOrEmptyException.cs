using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class FirstNameIsNullOrEmptyException : Exception
    {
        public FirstNameIsNullOrEmptyException(string message) : base(message)
        {
        }
    }
}