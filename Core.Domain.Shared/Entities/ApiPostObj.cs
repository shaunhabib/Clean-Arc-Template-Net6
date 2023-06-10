using Core.Domain.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Shared.Entities
{
    public class ApiPostObj
    {
        public string ApiBasicUrl { get; set; }
        public string Url { get; set; }
        public ApiPostCategoryType ApiPostCategoryType { get; set; }
    }
}
