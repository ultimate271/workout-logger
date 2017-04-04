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

			public override string ToString() {
				return $"Round: {this.Movement} {this.Quantity} {this.Load}";
			}
		}
	}
}