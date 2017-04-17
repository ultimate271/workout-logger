using System;
using System.Xml.Linq;
using System.Collections.Generic;

using XmlSerializer;
using System.Runtime.Serialization;

namespace WorkoutLogger {
	
		/// <summary>
		/// Used to tag any class which extends WL_Result with the types of workouts that result is compatible with
		/// </summary>
		[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
		public class WL_ResultCompatabilityAttribute : Attribute{
			/// <summary>
			/// Indicates the types of workouts that the WL_Result class that is tagged by this attribute is compatible with
			/// </summary>
			public Type[] WorkoutTypes { get; set; }
			public WL_ResultCompatabilityAttribute(params Type[] WorkoutTypes){
				this.WorkoutTypes = WorkoutTypes;
			}
		}
		#region Exceptions
		/// <summary>
		/// Does nothing except extend Exception. Used to differentiate when I as a developer have thrown an exception, vs when I have made a mistake and an exception gets thrown by my code
		/// </summary>
		public class WL_Exception : Exception {
			public WL_Exception() {
			}

			public WL_Exception(string message) : base(message) {
			}

			public WL_Exception(string message, Exception innerException) : base(message, innerException) {
			}

			protected WL_Exception(SerializationInfo info, StreamingContext context) : base(info, context) {
			}
		}
		#endregion
		#region Old Stuff
		/// <summary>
		/// Implemented by all classes in the model which can be interpreted as Xml
		/// A class which implements this interface can be converted to xml and created from xml.
		/// </summary>
		//public interface IXmlSerializable {
		//	/// <summary>
		//	/// The default or "current" XmlContext, to be used in the other methods if the
		//	/// XmlSerializableContext is null or not given.
		//	/// </summary>
		//	XmlSerializableContext XmlContext { get; set; }
		//	/// <summary>
		//	/// This method will be used to return an XElement which represents the current object
		//	/// under the XmlContext. If XmlContext is not given, the extension method
		//	/// XElement ToXml() will use this.XmlContext in its place. This should be implemented
		//	/// similarly if the XmlContext passed to the method is null.
		//	/// </summary>
		//	/// <param name="XmlContext"></param>
		//	/// <returns></returns>
		//	XElement ToXml(
		//		XmlSerializableContext XmlContext
		//	);

		//	/// <summary>
		//	/// This method is used to fill the current object based upon the existing xml, under
		//	/// the XmlContext. If XmlContext is not given, the extension method
		//	/// void LoadFromXml(XElement incomingXml) will use this.XmlContext in its place. This should be 
		//	/// implemented similarly if the XmlContext passed to the method is null.
		//	/// </summary>
		//	/// <param name="incomingXml"></param>
		//	/// <param name="XmlContext"></param>
		//	void LoadFromXml(
		//		XElement incomingXml, 
		//		XmlSerializableContext XmlContext
		//	);
		//}

		/// <summary>
		/// Provides Extentions methods for IXmlSerializable
		/// These methods will be invoked if the XmlSerializableContext is not given in an IXmlSerializable object
		/// </summary>
		//public static class IXmlSerializableExtensions
		//{

		//	public static XElement ToXml(this IXmlSerializable xmlSerializableObj)
		//	{
		//		return xmlSerializableObj.ToXml(xmlSerializableObj.XmlContext);
		//	}
		//	public static void LoadFromXml(this IXmlSerializable xmlSerializableObj,
		//								   XElement incomingXml)
		//	{
		//		xmlSerializableObj.LoadFromXml(incomingXml, xmlSerializableObj.XmlContext);
		//	}
		//}

		/// <summary>
		/// Includes properties to tell a class that implements XmlSerializable what sort of xml to output
		/// Simply add a property to this class, and the remaining functions will work as intended through reflection
		/// </summary>
		//public class XmlSerializableContext {
		//	public enum XmlSerializeOptions { LocalFile, DBRecord };
		//	public XmlSerializeOptions SerializeMode { get; set; } = XmlSerializeOptions.LocalFile;
		//	public bool IncludeId { get; set; } = false;

		//	public XmlSerializableContext() { }
		//	/// <summary>
		//	/// Uses reflection to deep copy every property from the clone object into this
		//	/// </summary>
		//	/// <param name="clone"></param>
		//	public XmlSerializableContext(XmlSerializableContext clone) {
		//		foreach (System.Reflection.PropertyInfo property in clone.GetType().GetProperties()) {
		//			this.GetType().GetProperty(property.Name).SetValue(this, property.GetValue(clone));
		//		}
		//	}

		//	/// <summary>
		//	/// Uses reflection to print out all of the properties of this class in the form Name=Value;
		//	/// </summary>
		//	/// <returns></returns>
		//	public override string ToString() {
		//		string retVal = "XmlSerializableContext: ";
		//		foreach(System.Reflection.PropertyInfo property in this.GetType().GetProperties()) {
		//			retVal += string.Format("{0}={1};", property.Name, property.GetValue(this));
		//		}
		//		return retVal;
		//	}
		//}
		#endregion
		/* A bunch of stuff that was used for testing the xml serializer
		#region Debug Stuff
		[XmlSerializable]
		public class DerivedClass : AnotherTestClass{
			[XmlSerializable]
			public int DerivedClassInt { get; set; }
			public override string ToString() {
				return $"\n\tDerivedClassInt: {DerivedClassInt}{base.ToString()}";
			}
		}

		[XmlSerializable]
		public class TestClass {
			[XmlSerializable]
			public int TestInt1 { get; set; }
			[XmlSerializable]
			public int TestInt2 { get; set; }
			[XmlSerializable]
			public int TestInt3 { get; set; }
			[XmlSerializable]
			public string TestString1 { get; set; }
			[XmlSerializable]
			public string TestString2 { get; set; }
			[XmlSerializable]
			public List<int> TestList { get; set; }
			[XmlSerializable]
			public List<List<int>> CrazyList { get; set; } = new List<List<int>>();
			[XmlSerializable]
			public AnotherTestClass TestObject { get; set; }
			[XmlSerializable]
			public List<AnotherTestClass> ObjectList { get; set; }

			public TestClass() { }
			public override string ToString() {
				return string.Format("TestInt1: {0}\nTestInt2: {1}\nTestInt3: {2}\nTestString1: {3}\nTestString2: {4}\nTestList: {5}\nCrazyList:{6}\nTestObject{7}\nObjectList:{8}",
					TestInt1,
					TestInt2,
					TestInt3,
					TestString1,
					TestString2,
					List2String(TestList),
					CrazyList2String(CrazyList),
					TestObject,
					ObjectList2String(ObjectList));
			}
			public static string ObjectList2String(List<AnotherTestClass> l){
				string retVal = "";
				foreach(AnotherTestClass c in l){
					retVal += "\n" + c.ToString();
				}
				return retVal;
			}
			public static string CrazyList2String(List<List<int>> l){
				string retVal = "";
				foreach(List<int> sublist in l){
					retVal += "|" + List2String(sublist);
				}
				return retVal;
			}
			public static string List2String(List<int> l){
				string retVal = "";
				foreach (int i in l){
					retVal += $"{i}";
				}
				return retVal;
			}
		}
		[XmlSerializable]
		public class AnotherTestClass{
			[XmlSerializable]
			public int AnotherTestInt1 { get; set; }
			[XmlSerializable]
			public int AnotherTestInt2 { get; set; }
			public int AnotherTestInt3 { get; set; }
			public AnotherTestClass() { }
			public override string ToString() {
				return string.Format("\n\tAnotherTestInt1: {0}\n\tAnotherTestInt2: {1}\n\tAnotherTestInt3: {2}", AnotherTestInt1, AnotherTestInt2, AnotherTestInt3);
			}
		}
		#endregion
		*/
	
}
