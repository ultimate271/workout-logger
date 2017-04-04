using System;
using System.Linq;
using System.Xml.Linq;
using XmlSerializer;

namespace WorkoutLogger{
	namespace Model{
		[WL_ResultCompatability(typeof(WL_WorkoutMisc))]
		[XmlSerializable]
		public class WL_ResultTimed : WL_Result {
			[XmlSerializable]
			public TimeSpan Time { get; set; }
		}
	}
}