using XmlSerializer;

namespace WorkoutLogger {
	namespace Model {
		[XmlSerializable]
		[WL_ResultCompatability(typeof(WL_Workout))]
		public class WL_ResultMisc : WL_Result {
			[XmlSerializable]
			public string Description { get; set; }
		}
	}
}