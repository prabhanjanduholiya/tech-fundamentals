using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Az.Samples.BlobStorage.Lib.Exceptions
{
    public class RecordAlreadExistsException : Exception
    {
        public RecordAlreadExistsException(string name) : base($"Record already exists with name {name}.") 
        {
        
        }

        public RecordAlreadExistsException(int id) : base($"Record already exists with id {id}.")
        {

        }
    }
}
