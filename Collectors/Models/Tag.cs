using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Collectors.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public virtual ICollection<CollectibleTag> CollectibleTags { get; set; }
    }
}