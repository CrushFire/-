using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;

namespace Congratulations
{
	public class Birthday
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public DateTime Date { get; set; }
		public string? Name { get; set; } = null!;
		[Required]
		public string Category { get; set; } = null!;
		[Required]
		public int Age { get; set; }
		}
}
