using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using XmlSerializer;
using WLCore;

namespace WLConsole {
	public class Counter {
		private int threshold;
		private int count = 0;
		public event EventHandler<ThresholdReachedEventArgs> ThresholdReached;

		public void SetThreshold(int threshold) {
			this.threshold = threshold;
		}
		public void IncreaseCount() {
			count += 1;
			if (count >= threshold) {
				ThresholdReached?.Invoke(this, new ThresholdReachedEventArgs() { Message = $"{this.count} is past the {this.threshold} threshold" });
				//EventPublisherThresholdReached(new Counter_ThresholdReachedEventArgs() { Message = $"{this.count} is past the {this.threshold} threshold" });
			}
		}
		public void PrintCounter() {
			System.Console.WriteLine(this.count);
		}

		protected virtual void OnThresholdReached(ThresholdReachedEventArgs args) {
			ThresholdReached?.Invoke(this, args);
		}
	}
	public class ThresholdReachedEventArgs : EventArgs{
		public string Message { get; set; }
	}


	public class Console {
		public delegate int ComparatorDelegate<T>(T a, T b);
		public static List<T> QuickSort<T>(List<T> list, ComparatorDelegate<T> comparator) {
			if (list.Count <= 1) {
				return list;
			}
			T median = list[0];
			List<T> ltList = new List<T>();
			List<T> equalList = new List<T>();
			List<T> gtList = new List<T>();
			foreach (T t in list) {
				if (comparator(t, median) < 0) {
					ltList.Add(t);
				}
				if (comparator(t, median) == 0) {
					equalList.Add(t);
				}
				else if (comparator(t, median) > 0) {
					gtList.Add(t);
				}
			}
			List<T> sortedLtList = QuickSort(ltList, comparator);
			List<T> sortedGtList = QuickSort(gtList, comparator);
			sortedLtList.AddRange(equalList);
			sortedLtList.AddRange(sortedGtList);
			return sortedLtList;
		}
		public static int CompareStrings(string a, string b) {
			return String.Compare(b, a);
		}

		public static void MyHandler(object o, ThresholdReachedEventArgs args) {
			System.Console.WriteLine("{0} and fuck you", args.Message);
		}

		public static void Main(string[] args) {
			List<string> strings = new List<string>() {
				"abc", "bbb", "aaa", "ccc", "cba", "bca", "fuckyou"
			};
			List<string> sortedStrings = QuickSort(strings, String.Compare);
			foreach (string s in sortedStrings) {
				System.Console.Write("{0}, ", s);
			}
			sortedStrings = QuickSort(strings, CompareStrings);
			System.Console.WriteLine();
			foreach (string s in sortedStrings) {
				System.Console.Write("{0}, ", s);
			}

			System.Console.WriteLine();
			List<int> ints = new List<int>() { 4, 2, 3, 56, 6, 42, 132, 22222, 15, 15, 13 };
			List<int> sortedInts = QuickSort(ints, (a, b) => a < b ? -1 : a == b ? 0 : 1);
			foreach (int i in sortedInts) {
				System.Console.Write("{0}, ", i);
			}

			System.Console.ReadLine();
			//Counter c = new Counter();
			//c.SetThreshold(10);
			//c.ThresholdReached += MyHandler;

			//for (int i = 0; i < 20; i++) {
			//	c.PrintCounter();
			//	c.IncreaseCount();
			//}
			//System.Console.ReadLine();

			//System.Console.WriteLine("Hello World!");
			//WL_Workout foo = new WL_WorkoutMisc() { Description = "This is the description of a complicated workout", Comment = "This is a comment about the workout", Name = "Obscure123" };
			//System.Console.WriteLine("" + foo);
			//System.Console.ReadLine();
			/*
			List<WL_Round> rounds = new List<WL_Round>();
			rounds.Add(new WL_Round() { Movement = "Thruster", Quantity = new WL_QuantityReps() { Reps = 21 }, Load = new WL_Load() { Load = 95 } });
			rounds.Add(new WL_Round() { Movement = "Pullup", Quantity = new WL_QuantityReps() { Reps = 21 } });
			rounds.Add(new WL_Round() { Movement = "Thruster", Quantity = new WL_QuantityReps() { Reps = 15 }, Load = new WL_Load() { Load = 95 } });
			rounds.Add(new WL_Round() { Movement = "Pullup", Quantity = new WL_QuantityReps() { Reps = 15 } });
			rounds.Add(new WL_Round() { Movement = "Thruster", Quantity = new WL_QuantityReps() { Reps = 9 }, Load = new WL_Load() { Load = 95 } });
			rounds.Add(new WL_Round() { Movement = "Pullup", Quantity = new WL_QuantityReps() { Reps = 9 } });

			List<WL_Round> rounds2 = new List<WL_Round>();
			rounds2.Add(new WL_Round() { Movement = "Thruster", Quantity = new WL_QuantityReps() { Reps = 21 }, Load = new WL_Load() { Load = 95 } });
			rounds2.Add(new WL_Round() { Movement = "Pullup", Quantity = new WL_QuantityReps() { Reps = 21 } });
			rounds2.Add(new WL_Round() { Movement = "Thruster", Quantity = new WL_QuantityReps() { Reps = 15 }, Load = new WL_Load() { Load = 95 } });
			rounds2.Add(new WL_Round() { Movement = "Pullup", Quantity = new WL_QuantityReps() { Reps = 15 } });
			rounds2.Add(new WL_Round() { Movement = "Thruster", Quantity = new WL_QuantityReps() { Reps = 9 }, Load = new WL_Load() { Load = 95 } });
			rounds2.Add(new WL_Round() { Movement = "Pullup", Quantity = new WL_QuantityReps() { Reps = 9 } });

			//WL_Workout workout = new WL_WorkoutMisc() { Comment = "A Comment", Description = "A Description", Name = "A Name" };
			//WL_Workout miscWorkout = new WL_WorkoutMisc { Comment = "A Comment", Description = "A Description", Name = "A Name" };
			WL_Workout timedWorkout1 = new WL_WorkoutTimed { Comment = "A Comment", Name = "A Name", Rounds = rounds };
			WL_Workout timedWorkout2 = new WL_WorkoutTimed { Comment = "A Comment", Name = "A Name", Rounds = rounds2 };
			
			//System.Console.WriteLine($"{workout}\n{miscWorkout}\n{workout.Equals(miscWorkout)}\n{miscWorkout.Equals(workout)}");
			System.Console.WriteLine($"{timedWorkout1}\n{timedWorkout2}\n{timedWorkout1.Equals(timedWorkout2)}\n{timedWorkout2.Equals(timedWorkout1)}");
			////WL_Workout w = XElement.Load("test.xml").DeserializeFromXml() as WL_Workout;
			//WL_Workout w1 = new WL_WorkoutMisc() { Name = "Fran", Comment = "The gnarliest of workouts", Description = "21-15-9 Thrusters and Pullups" };
			//WL_Workout w2 = new WL_WorkoutMisc() { Name = "Fran", Comment = "The gnarliest of workouts", Description = "21-15-9 Thrusters and Pullups" };
			////WL_Workout w3 = new WL_WorkoutTimed() { Name = "Fran", Comment = "The gnarliest of workouts", Rounds = new List<string>(new string[] { "Thruster", "Pullup" }) };

			//WL_Workout w3 = new WL_WorkoutTimed() { Name = "Fran", Comment = "Girls Benchmark", Rounds = new List<WL_Round>(rounds) };
			//rounds.Add(new WL_Round() { Movement = "What the Fuck", Quantity = new WL_QuantityReps() { Reps = 666 } });


			//System.Console.WriteLine("{0}", w3.SerializeToXml().DeserializeFromXml().SerializeToXml());
			//System.Console.WriteLine("{0}", w3.ToString());
			//System.Console.WriteLine("{0}", w3.SerializeToXml().DeserializeFromXml().ToString());


			//Model m = new Model();
			//WL_Log log = new WL_Log() { DateCompleted = new DateTime(2017, 11, 23), Workout = w1, Result = new WL_ResultScore() { Score = 14 } };
			//WL_Log log2 = new WL_Log() { DateCompleted = new DateTime(2017, 3, 31), Workout = w1, Result = new WL_ResultTimed() { Time = new TimeSpan(0, 12, 14) } };
			//WL_Log log3 = new WL_Log() { DateCompleted = new DateTime(), Workout = w1, Result = new WL_ResultMisc() { Description = "Some really obscure result" } };
			////m.AddLog(log);
			////m.AddLog(log2);
			////m.AddLog(log3);
			////System.Console.WriteLine(log.SerializeToXml().DeserializeFromXml().SerializeToXml());
			////System.Console.WriteLine(log2.SerializeToXml().DeserializeFromXml().SerializeToXml());
			////System.Console.WriteLine(log3.SerializeToXml());
			//System.Console.WriteLine(w1.Equals(w3));
			//System.Console.WriteLine(w3.Equals(w1));

			

			////m.Workouts.Add(w);
			
			
			////w.SerializeToXml().Save("test.xml");
			////System.Console.WriteLine("{0}\n{1}\n{2}", w, w.SerializeToXml("AThing"), w.SerializeToXml("AThing").DeserializeFromXml());
			






			/*
			 * All my testing for xmlserializer
			 */
			//TestClass myTestObject = new TestClass() {
			//	TestInt1 = 21,
			//	TestInt2 = 15,
			//	TestInt3 = 9,
			//	TestString1 = "Fran",
			//	TestString2 = "Cindy",
			//	TestList = new List<int>(new int[] { 42, 24, 18, 21 }),
			//	CrazyList = new List<List<int>>(new List<int>[] { new List<int>(new int[] { 3, 5, 7 }), new List<int>(new int[] { 2, 4, 6, 8 }) }),
			//	TestObject = new AnotherTestClass() {
			//		AnotherTestInt1 = 20,
			//		AnotherTestInt2 = 10
			//	},
			//	ObjectList = new List<AnotherTestClass>(new AnotherTestClass[]{
			//	new DerivedClass(){
			//		AnotherTestInt1 = 44,
			//		AnotherTestInt2 = 22,
			//		AnotherTestInt3 = 88,
			//		DerivedClassInt = 666
			//	}, new AnotherTestClass() {
			//		AnotherTestInt1 = 55,
			//		AnotherTestInt2 = 33
			//	}
			//	})
			//};
			//////Int32 x = 12;
			////System.Console.WriteLine(myTestObject);
			////System.Console.WriteLine(12.SerializeToXml());
			////System.Console.WriteLine("AnAnonymousString".SerializeToXml());
			////List<int> aList = new List<int>(new int[] { 4, 8, 15, 16, 23, 42 });
			////System.Console.WriteLine("{0}", aList.SerializeToXml());
			////List<List<int>> crazyList = new List<List<int>>(new List<int>[] { new List<int>() { 1, 3, 5, 7 }, new List<int>() { 2, 4, 6, 8 }, new List<int>() { 1, 4, 8 } });
			////System.Console.WriteLine("{0}", crazyList.SerializeToXml());

			////System.Console.WriteLine("{0}", myTestObject.IsXmlSerializable());
			////System.Console.WriteLine("{0}", myTestObject.TestObject.IsXmlSerializable());
			////System.Console.WriteLine("{0}", myTestObject.SerializeToXml());

			////System.Console.WriteLine("{0}", "list".StringToType("TestClass"));

			//System.Console.WriteLine("{0}\n{1}\n{2}",
			//	myTestObject,
			//	myTestObject.SerializeToXml(),
			//	myTestObject.SerializeToXml().DeserializeFromXml());

			////System.Console.WriteLine(myTestObject);
			////System.Console.WriteLine(myTestObject.SerializeToXml());
			////System.Console.WriteLine(myTestObject.SerializeToXml().DeserializeFromXml());
			////myTestObject.SerializeToXml().Save("TestObject.xml");
			////System.Console.WriteLine(XElement.Load("TestObject.xml"));
			////System.Console.WriteLine(XElement.Load("TestObject.xml").DeserializeFromXml());

			////System.Console.WriteLine(myTestObject.SerializeToXml());
			////Match m1 = Regex.Match("list<list<int>>", @"list<(.*)>");
			////List<List<int>> crazyList = new List<List<int>>();
			////Type crazyType = crazyList.GetType();
			////System.Console.WriteLine("{0}", crazyType.TypeToString());
			////System.Console.WriteLine("{0} {1}", m1.Value, m1.Groups[1].Value);




			////XElement testXml = (new List<int>() { 2, 3, 1, 5 }).SerializeToXml();
			////IList list = testXml.DeserializeFromXml() as IList;
			////System.Console.WriteLine("{0}\n{1}", testXml, list);
			////foreach (object o in list) {
			////	System.Console.WriteLine(o);
			////}
			////System.Console.WriteLine("{0}", 0.SerializeToXml().DeserializeFromXml());



			////System.Console.WriteLine("{0}", (new List<string>()).GetType().GetGenericArguments().SingleOrDefault().IsXmlSerializable());

			////System.Console.WriteLine("This is the console view");
			////Workout myWorkout = new MiscWorkout {
			////	Name = "Workout name",
			////	Description = "A very complicated workout",
			////	Comment = "This is a comment about this workout",
			////	Id = 12,
			////	//XmlContext = new XmlSerializableContext {
			////	//	SerializeMode = XmlSerializableContext.XmlSerializeOptions.LocalFile,
			////	//	IncludeId = true
			////	//}
			////};

			////System.Console.WriteLine("{0}", myWorkout);
			////secondWorkout.LoadFromXml(myWorkout.ToXml());

			////System.Console.WriteLine(myWorkout.ToXml().ToString());
			////System.Console.WriteLine(secondWorkout.ToXml().ToString());



			////Model.TestClass myClass = new Model.TestClass(null);
			////myClass.XmlContext = new Model.XmlSerializableContext { IncludeMeta = false };
			////myClass.ToXml();

		}
	}
}
