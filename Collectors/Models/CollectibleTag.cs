using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collectors.Models
{
    public class CollectibleTag
    {
        public int Id { get; set; }
        public int CollectibleId { get; set; }
        public int TagId { get; set; }
        public Collectible Collectible { get; set; }
        public Tag Tag { get; set; }
    }
}
