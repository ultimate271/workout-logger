using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using XmlSerializer;

namespace WorkoutLogger {
	using Model;
	public class ConsoleView {

		public static void Main(string[] args) {
			DerivedClass myTestObject = new DerivedClass() {
				TestInt1 = 21,
				TestInt2 = 15,
				TestInt3 = 9,
				TestString1 = "Fran",
				TestString2 = "Cindy",
				TestList = new List<int>(new int[] { 42, 24, 18, 21 }),
				TestObject = new AnotherTestClass() {
					AnotherTestInt1 = 20,
					AnotherTestInt2 = 10
				}
			};
			////Int32 x = 12;
			//System.Console.WriteLine(myTestObject);
			//System.Console.WriteLine(12.SerializeToXml());
			//System.Console.WriteLine("AnAnonymousString".SerializeToXml());
			//List<int> aList = new List<int>(new int[] { 4, 8, 15, 16, 23, 42 });
			//System.Console.WriteLine("{0}", aList.SerializeToXml());
			//List<List<int>> crazyList = new List<List<int>>(new List<int>[] { new List<int>() { 1, 3, 5, 7 }, new List<int>() { 2, 4, 6, 8 }, new List<int>() { 1, 4, 8 } });
			//System.Console.WriteLine("{0}", crazyList.SerializeToXml());

			System.Console.WriteLine("{0}", myTestObject.IsXmlSerializable());
			System.Console.WriteLine("{0}", myTestObject.TestObject.IsXmlSerializable());
			System.Console.WriteLine("{0}", myTestObject.SerializeToXml());

			
			
			//System.Console.WriteLine("{0}", (new List<string>()).GetType().GetGenericArguments().SingleOrDefault().IsXmlSerializable());

			//System.Console.WriteLine("This is the console view");
			//Workout myWorkout = new MiscWorkout {
			//	Name = "Workout name",
			//	Description = "A very complicated workout",
			//	Comment = "This is a comment about this workout",
			//	Id = 12,
			//	//XmlContext = new XmlSerializableContext {
			//	//	SerializeMode = XmlSerializableContext.XmlSerializeOptions.LocalFile,
			//	//	IncludeId = true
			//	//}
			//};

			//System.Console.WriteLine("{0}", myWorkout);
			//secondWorkout.LoadFromXml(myWorkout.ToXml());

			//System.Console.WriteLine(myWorkout.ToXml().ToString());
			//System.Console.WriteLine(secondWorkout.ToXml().ToString());

			

			//Model.TestClass myClass = new Model.TestClass(null);
			//myClass.XmlContext = new Model.XmlSerializableContext { IncludeMeta = false };
			//myClass.ToXml();
		}
	}
}
