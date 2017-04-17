using XmlSerializer;

namespace WorkoutLogger {
	
	[XmlSerializable]
	[WL_ResultCompatability(typeof(WL_Workout))]
	public class WL_ResultMisc : WL_Result {
		[XmlSerializable]
		public string Description { get; set; }


		public override bool Equals(object obj) {

			if (obj == null || GetType() != obj.GetType()) {
				return false;
			}

			WL_ResultMisc inResult = (WL_ResultMisc)obj;
			return this.Description != null ? Description.Equals(inResult.Description) : false;

		}

		public override int GetHashCode() {
			return this.Description.GetHashCode();
		}

		public override string ToString() {
			return $"Result: {this.Description}";
		}
	}
}