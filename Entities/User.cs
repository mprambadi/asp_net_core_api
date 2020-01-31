using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core_api.Entities
{
    public class UserEntities
    {
        public int id { get; set; }
        public string username { get; set; }
        public string token  { get; set; }
        public string  email { get; set; }
    }
}
