namespace WorkoutLogger {
	public class ConsoleView {

		public static void Main(string[] args) {
			System.Console.WriteLine("This is the console view");
			Model.XmlSerializableContext context1 = new Model.XmlSerializableContext { IncludeMeta = true };
			Model.XmlSerializableContext context2 = new Model.XmlSerializableContext(context1);
			context1.IncludeMeta = false;
			context1.SomeString = "This is a string!";
			System.Console.WriteLine(context1);
			System.Console.WriteLine(context2);

			//Model.TestClass myClass = new Model.TestClass(null);
			//myClass.XmlContext = new Model.XmlSerializableContext { IncludeMeta = false };
			//myClass.ToXml();
		}
	}
}
