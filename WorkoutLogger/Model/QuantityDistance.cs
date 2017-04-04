using System;
using System.Xml.Linq;
using System.Collections.Generic;
using XmlSerializer;

namespace WorkoutLogger {
	namespace Model {
		[XmlSerializable]
		public class WL_QuantityDistance : WL_Quantity {
			[XmlSerializable]
			public int Distance { get; set; } //TODO add a format provider to allow Distance to be in miles or meters or yards or whatever
			public override string ToString() {
				return $"{Distance}m"; //TODO change this method when a format provider is implemented
			}
		}
	}
}