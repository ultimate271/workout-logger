using System;
using System.Linq;
using System.Xml.Linq;
using XmlSerializer;

namespace WorkoutLogger{
	namespace Model{
		[WL_ResultCompatability(typeof(WL_MiscWorkout))]
		[XmlSerializable]
		public class WL_TimedResult : WL_Result {
			[XmlSerializable]
			public TimeSpan Time { get; set; }
		}
	}
}