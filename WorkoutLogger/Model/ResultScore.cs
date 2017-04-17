using System;
using System.Collections.Generic;
using XmlSerializer;

namespace WorkoutLogger {
	
	[WL_ResultCompatability(typeof(WL_WorkoutMisc))]
	[XmlSerializable]
	public class WL_ResultScore : WL_Result {
		[XmlSerializable]
		public int Score { get; set; }
		// override object.Equals
		public override bool Equals(object obj) {
			if (obj == null || GetType() != obj.GetType()) {
				return false;
			}
			WL_ResultScore inScore = (WL_ResultScore)obj;
			return this.Score == inScore.Score;
		}

		public override int GetHashCode() {
			return this.Score.GetHashCode();
		}

		public override string ToString() {
			return $"Score: {this.Score}";
		}
	}
	

}