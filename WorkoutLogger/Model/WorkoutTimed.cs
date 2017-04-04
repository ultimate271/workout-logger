using System;
using System.Xml.Linq;
using System.Collections.Generic;
using XmlSerializer;

namespace WorkoutLogger {
	namespace Model {
		[XmlSerializable]
		public class WL_WorkoutTimed : WL_Workout {
			[XmlSerializable]
			public List<WL_Round> Rounds { get; set; }

			public override string ToString() {
				string retVal = $"{base.ToString()}";
				foreach (WL_Round round in this.Rounds){
					retVal += $"\n\t{round.ToString()}";
				}
				return retVal;
			}

			
		}
	}
}