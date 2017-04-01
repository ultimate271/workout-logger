using System;
using System.Linq;
using System.Xml.Linq;
using XmlSerializer;

namespace WorkoutLogger{
	namespace Model{
		[XmlSerializable]
		public class WL_Log{
			[XmlSerializable]
			public DateTime DateCompleted{ get; set; }

			[XmlSerializable]
			public WL_Workout Workout { get; set; }

			[XmlSerializable]
			public WL_Result Result { get; set; }
		}
	}
}