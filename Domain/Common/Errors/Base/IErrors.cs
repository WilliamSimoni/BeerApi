using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Errors.Base
{
    public interface IError
    {
        public string Message { get; }
        public string Code { get; }
        public int Number { get;  }
    }

}
