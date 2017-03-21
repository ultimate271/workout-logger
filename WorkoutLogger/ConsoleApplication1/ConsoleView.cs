
namespace WorkoutLogger {
	using Model;
	public class ConsoleView {

		public static void Main(string[] args) {
			System.Console.WriteLine("This is the console view");
			MiscWorkout myWorkout = new MiscWorkout {
				Name = "My Workout",
				Description = "A very complicated workout",
				XmlContext = new XmlSerializableContext {
					SerializeMode = XmlSerializableContext.XmlSerializeOptions.LocalFile,
					IncludeId = false
				}
			};

			System.Console.WriteLine(myWorkout.ToXml().ToString());

			

			//Model.TestClass myClass = new Model.TestClass(null);
			//myClass.XmlContext = new Model.XmlSerializableContext { IncludeMeta = false };
			//myClass.ToXml();
		}
	}
}
