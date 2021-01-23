using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApi.Dto
{
    interface IHasValidation
    {
        IEnumerable<string> Validate();
    }
}
