﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanBlazorWASM.Shared
{
    internal class Villain 
    {
		public int Id { get; set; }

		public string FirstName { get; set; } = string.Empty;

		public string LastName { get; set; } = string.Empty;

		public string VillainName { get; set; } = string.Empty;

		public Comic? Comic { get; set; }

		public int ComicId { get; set; }
	}
}
