using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.DataModels.SearchModels
{
    public class Index : BaseModel
    {
        public string Token { get; set; }
        public virtual ICollection<IndexItems> Items { get; set; }
    }
}