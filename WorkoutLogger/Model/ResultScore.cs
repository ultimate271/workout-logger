using System;
using System.Collections.Generic;
using XmlSerializer;

namespace WorkoutLogger {
	namespace Model {
		[WL_ResultCompatability(typeof(WL_WorkoutMisc))]
		[XmlSerializable]
		public class WL_ResultScore : WL_Result {
			[XmlSerializable]
			public int Score { get; set; }
		}
	}
}