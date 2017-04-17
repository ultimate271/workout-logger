using System;
using System.Xml.Linq;
using System.Collections.Generic;
using XmlSerializer;

namespace WorkoutLogger{

	[XmlSerializable]
	public class WL_Round{
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

		
		public override bool Equals(object obj) {
			if (obj == null || GetType() != obj.GetType()) {
				return false;
			}

			WL_Round inRound = (WL_Round)obj;
			return
				(this.Movement != null ? this.Movement.Equals(inRound.Movement) : inRound.Movement == null) &&
				(this.Load != null ? this.Load.Equals(inRound.Load) : inRound.Load == null) &&
				(this.Quantity != null ? this.Quantity.Equals(inRound.Quantity) : inRound.Quantity == null);
		}

		// override object.GetHashCode
		public override int GetHashCode() {
			return (this.Load.GetHashCode() * this.Movement.GetHashCode() * this.Quantity.GetHashCode());
		}
		public override string ToString() {
			return $"Round: {this.Movement} {this.Quantity} {this.Load}";
		}
	}
}