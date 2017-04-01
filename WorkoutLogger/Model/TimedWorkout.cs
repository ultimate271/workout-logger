using System;
using System.Xml.Linq;
using System.Collections.Generic;
using XmlSerializer;

namespace WorkoutLogger {
	namespace Model {
		[XmlSerializable]
		public class WL_TimedWorkout : WL_Workout {
			[XmlSerializable]
			public List<string> Rounds { get; set; }

			public override bool Equals(object obj) {
				return base.Equals(obj);
			}
		}
	}
}