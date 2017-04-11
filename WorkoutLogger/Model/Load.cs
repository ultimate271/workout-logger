using System;
using System.Xml.Linq;
using System.Collections.Generic;
using XmlSerializer;

namespace WorkoutLogger {
	namespace Model {
		[XmlSerializable]
		public class WL_Load{
			[XmlSerializable]
			public int Load { get; set; } //TODO add format provider to allow Load to be in metric or english or pood or whatever

			public override string ToString() {
				return $"{this.Load}#"; //TODO change this method when a format provider is added
			}

			public override bool Equals(object obj) {

				if (obj == null || GetType() != obj.GetType()) {
					return false;
				}


				WL_Load inLoad = (WL_Load)obj;
				return this.Load == inLoad.Load;
			}

			public override int GetHashCode() {
				return this.Load.GetHashCode();
			}
		}
	}
}