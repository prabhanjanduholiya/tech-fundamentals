using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Az.Samples.BlobStorage.Lib.Exceptions
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException(string name) : base($"Record not found with name {name}.")
        {

        }

        public RecordNotFoundException(int id) : base($"Record not found with id {id}.")
        {

        }
    }
}
