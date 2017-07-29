using System;
using System.Xml.Linq;
using System.Collections.Generic;
using XmlSerializer;

namespace WLCore {
	
	[XmlSerializable]
	public class WL_QuantityReps : WL_Quantity {
		[XmlSerializable]
		public int Reps { get; set; }

		public override bool Equals(object obj) {
			if (obj == null || GetType() != obj.GetType()) {
				return false;
			}

			WL_QuantityReps inReps = (WL_QuantityReps)obj;

			return this.Reps == inReps.Reps;
		}

		public override int GetHashCode() {
			return this.Reps.GetHashCode();
		}

		public override string ToString() {
			return $"{this.Reps}reps";
		}
	}
	
}