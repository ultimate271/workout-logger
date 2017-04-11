using System;
using System.Xml.Linq;
using System.Collections.Generic;
using XmlSerializer;

namespace WorkoutLogger {
	namespace Model {
		[XmlSerializable]
		public class WL_QuantityReps : WL_Quantity {
			[XmlSerializable]
			public int Reps { get; set; }

			public override string ToString() {
				return $"{this.Reps} reps";
			}
			// override object.Equals
			public override bool Equals(object obj) {
				//       
				// See the full list of guidelines at
				//   http://go.microsoft.com/fwlink/?LinkID=85237  
				// and also the guidance for operator== at
				//   http://go.microsoft.com/fwlink/?LinkId=85238
				//

				if (obj == null || GetType() != obj.GetType()) {
					return false;
				}
				WL_QuantityReps inReps = (WL_QuantityReps)obj;

				return this.Reps == inReps.Reps;
			}

			// override object.GetHashCode
			public override int GetHashCode() {
				return this.Reps.GetHashCode();
			}
		}
	}
}