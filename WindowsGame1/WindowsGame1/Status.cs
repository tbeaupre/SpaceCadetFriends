﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spaceman
{
	public class Status
	{
		public string state;
		public int duration;

		public Status(string state, int duration)
		{
			this.state = state;
			this.duration = duration;
		}
	}
}
