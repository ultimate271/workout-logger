using System;
using System.Linq;
using System.Xml.Linq;
using XmlSerializer;

namespace WorkoutLogger{
	[XmlSerializable]
	public class WL_Log{
		[XmlSerializable]
		public DateTime DateCompleted { get; set; }

		[XmlSerializable]
		public WL_Workout Workout { get; set; }

		[XmlSerializable]
		public WL_Result Result { get; set; }

		public override bool Equals(object obj) {
			if (obj == null || GetType() != obj.GetType()) {
				return false;
			}
			WL_Log inLog = (WL_Log)obj;
			return
				(this.DateCompleted != null ? this.DateCompleted.Equals(inLog.DateCompleted) : inLog.DateCompleted == null) &&
				(this.Result != null ? this.Result.Equals(inLog.Result) : inLog.Result == null) &&
				(this.Workout != null ? this.Workout.Equals(inLog.Workout) : inLog.Workout == null);

		}

		// override object.GetHashCode
		public override int GetHashCode() {
			return
				(this.DateCompleted != null ? this.DateCompleted.GetHashCode() : 1) *
				(this.Result != null ? this.Result.GetHashCode() : 1) *
				(this.Workout != null ? this.Workout.GetHashCode() : 1);
		}

		public override string ToString() {
			return $"{this.DateCompleted}\n{this.Workout}\n{this.Result}";
		}
	}

}