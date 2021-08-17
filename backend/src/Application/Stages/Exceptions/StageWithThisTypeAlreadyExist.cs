using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stages.Exceptions
{
    public class StageWithThisTypeAlreadyExist : Exception
    {
        public StageWithThisTypeAlreadyExist() :
            base(JsonConvert.SerializeObject(new
            {
                type = "StageWithThisTypeAlreadyExist",
                description = "Stage with this type already exist"
            }))
        { }
    }
}
