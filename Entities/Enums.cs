using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class Enums
	{
		public const int TEMP_USER_ID = 1;
		public enum Status
		{
			Active = 1,
			NotActive = 2
		}

		public enum TaskStatus
		{
			InProgress = 1,
			Success = 2,
			Fail = 3,
			PartialSuccess = 4,
			Submitted = 5
		}
	}
}
