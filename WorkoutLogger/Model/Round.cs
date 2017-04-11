using System;
using System.Xml.Linq;
using System.Collections.Generic;
using XmlSerializer;

namespace WorkoutLogger{
	namespace Model{
		[XmlSerializable]
		public class WL_Round {
			/// <summary>
			/// The name of the movement, e.g. Thruster or Pullup or Run
			/// </summary>
			[XmlSerializable]
			public string Movement { get; set; }
			/// <summary>
			/// The quantity of the movement, such as "21 reps" or "500 m"
			/// </summary>
			[XmlSerializable]
			public WL_Quantity Quantity { get; set; }
			/// <summary>
			/// The load of the movement, such as "95#" or "100kg" or "2pood"
			/// Null indicates a bodyweight movement
			/// </summary>
			[XmlSerializable]
			public WL_Load Load { get; set; }

			// override object.Equals
			public override bool Equals(object obj) {
				if (obj == null || GetType() != obj.GetType()) {
					return false;
				}
				WL_Round inRound = (WL_Round)obj;

				return
					this.Movement == inRound.Movement &&
					this.Quantity.Equals(inRound.Quantity) &&
					this.Load.Equals(inRound.Load);
			}

			// override object.GetHashCode
			public override int GetHashCode() {
				// TODO: write your implementation of GetHashCode() here
				throw new NotImplementedException();
				return base.GetHashCode();
			}
			public override string ToString() {
				return $"{this.Quantity} of {this.Movement}" +
					(this.Load != null ? $" @ {this.Load}" : "");
			}
		}
	}
}