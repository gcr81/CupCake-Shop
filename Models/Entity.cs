using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/// <summary>
/// Oligert Crroj
/// Creted on :7/1/2021 10:24 Am
/// Last changes made: 7/5/2021 12:04 am 
/// </summary>

namespace CupCakeShop.Models
{
    public abstract class Entity
    {
        public int ID { get; set; }
        public string CreatedByUser { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string ModifiedByUser { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}
