using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WorkoutLogger {
	using Model;
	public class ConsoleView {

		public static void Main(string[] args) {
			System.Console.WriteLine("This is the console view");
			Workout myWorkout = new MiscWorkout {
				Name = "Workout name",
				Description = "A very complicated workout",
				Comment = "This is a comment about this workout",
				Id = 12,
				XmlContext = new XmlSerializableContext {
					SerializeMode = XmlSerializableContext.XmlSerializeOptions.LocalFile,
					IncludeId = true
				}
			};

			Workout secondWorkout = new MiscWorkout();
			secondWorkout.LoadFromXml(myWorkout.ToXml());

			System.Console.WriteLine(myWorkout.ToXml().ToString());
			System.Console.WriteLine(secondWorkout.ToXml().ToString());

			

			//Model.TestClass myClass = new Model.TestClass(null);
			//myClass.XmlContext = new Model.XmlSerializableContext { IncludeMeta = false };
			//myClass.ToXml();
		}
	}
}
