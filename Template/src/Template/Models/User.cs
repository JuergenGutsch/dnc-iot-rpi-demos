using System.ComponentModel.DataAnnotations;
using Raspberry;

namespace MvcSample.Web.Models
{
    public class User
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
	public Board Board { get; set; }
    }
}
