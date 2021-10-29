using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Models.DataModels.ItemModels
{
    public class Receit : BaseModel
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public double TotalPrice { get; set; }
        public int ItemCount { get; set; }

        public virtual ICollection<ReceitItem> Items { get; set; }
    }
}