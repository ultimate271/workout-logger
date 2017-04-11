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
				retVal += "\n\tFor Time:";
				foreach (WL_Round round in this.Rounds){
					retVal += $"\n\t{round.ToString()}";
				}
				return retVal;
			}

			// override object.Equals
			public override bool Equals(object obj) {
				if (obj == null || GetType() != obj.GetType()) {
					return false;
				}
				WL_WorkoutTimed inWorkout = (WL_WorkoutTimed)obj;

				bool retVal = base.Equals(obj);
				
				IEnumerator<WL_Round> enumerator1 = Rounds.GetEnumerator();
				IEnumerator<WL_Round> enumerator2 = inWorkout.Rounds.GetEnumerator();
				while(enumerator1.MoveNext() && enumerator2.MoveNext()){
					retVal &= enumerator1.Current.Equals(enumerator2.Current);
				}

				return retVal;
			}

			// override object.GetHashCode
			public override int GetHashCode() {
				// TODO: write your implementation of GetHashCode() here
				throw new NotImplementedException();
				return base.GetHashCode();
			}
			
		}
	}
}