using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceFlow.Model
{
    public class CheckOut
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string LastName { get; set; }

        public DateTime CheckOutDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
