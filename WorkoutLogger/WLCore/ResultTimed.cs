using System;
using System.Linq;
using System.Xml.Linq;
using XmlSerializer;

namespace WLCore{

	[WL_ResultCompatability(typeof(WL_WorkoutTimed))]
	[XmlSerializable]
	public class WL_ResultTimed : WL_Result {
		[XmlSerializable]
		public TimeSpan Time { get; set; }

		public override bool Equals(object obj) {
			if (obj == null || GetType() != obj.GetType()) {
				return false;
			}
			WL_ResultTimed inTime = (WL_ResultTimed)obj;
			return this.Time != null ? this.Time.Equals(inTime.Time) : false;
		}

		public override int GetHashCode() {
			return Time.GetHashCode();
		}

		public override string ToString() {
			return $"Time: {this.Time.ToString()}";
		}

	}

}