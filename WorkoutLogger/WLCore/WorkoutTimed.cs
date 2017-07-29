using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using XmlSerializer;

namespace WLCore {
	
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

		public override bool Equals(object obj) {
			if (obj == null || GetType() != obj.GetType()) {
				return false;
			}
			
			WL_WorkoutTimed inWorkout = (WL_WorkoutTimed)obj;
			return this.Rounds.SequenceEqual(inWorkout.Rounds);
		}

		public override int GetHashCode() {
			return this.Rounds.Aggregate(0, (x, y) => x ^ y.GetHashCode()) * MetadataGetHashCode();
		}

			
	}
	
}