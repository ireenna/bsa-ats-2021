using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stages.Exceptions
{
    public class StageWithThisIndexAlreadyExist : Exception
    {
        public StageWithThisIndexAlreadyExist() :
            base(JsonConvert.SerializeObject(new
            {
                type = "StageWithThisIndexAlreadyExist",
                description = "Stage with this index already exist"
            }))
        { }
    }
}
