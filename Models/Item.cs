using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingList.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage ="Please enter the item name!")]
        public string Name { get; set; }

        public string Details { get; set; }

        [Required(ErrorMessage = "Please enter the item amount!")]
        public float Amount { get; set; }

    }
}
