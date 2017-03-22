using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WorkoutLogger {
	using Model;
	public class ConsoleView {

		public static void Main(string[] args) {
			System.Console.WriteLine("This is the console view");
			MiscWorkout myWorkout = new MiscWorkout {
				//Name = "My Workout",
				Description = "A very complicated workout",
				XmlContext = new XmlSerializableContext {
					SerializeMode = XmlSerializableContext.XmlSerializeOptions.LocalFile,
					IncludeId = false
				}
			};

			XElement xml = myWorkout.ToXml();

			var myQuery = from nameElement in xml.Elements()
						  where nameElement.Name == "Name"
						  select nameElement.Value;

			string name = myQuery.SingleOrDefault();

			System.Console.WriteLine(name ?? "Null String");

			

			//Model.TestClass myClass = new Model.TestClass(null);
			//myClass.XmlContext = new Model.XmlSerializableContext { IncludeMeta = false };
			//myClass.ToXml();
		}
	}
}
