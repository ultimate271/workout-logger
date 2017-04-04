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
				return $"{this.Reps}reps";
			}
		}
	}
}