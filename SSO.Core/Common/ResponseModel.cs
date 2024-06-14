using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_Server.Core.Common
{
    public class ResponseModel<T>
    {
        public int StatusCode { get; set; }
        public string ErrorMassage { get; set; }
        public bool IsSucceed { get; set; }
        public T Result { get; set; }
    }
}
