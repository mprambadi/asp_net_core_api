using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core_api.Model
{
    public class User
    {
        public int id { get; set; }
        [Required]
        [StringLength(30)]
        public string username { get; set; }
        public string salt  { get; set; }
        public string  email { get; set; }
        public string  profile { get; set; }
        [ForeignKey("userId")]
        public ICollection<Post> Posts {get;set;}

    }
}
