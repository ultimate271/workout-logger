using System;
using System.Xml.Linq;
using System.Collections.Generic;

using XmlSerializer;
namespace WorkoutLogger {
	namespace Model {
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

		[XmlSerializable]
		public class DerivedClass : TestClass{

		}

		[XmlSerializable]
		public class TestClass{
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
			public AnotherTestClass TestObject { get; set; }

			public TestClass() { }
			public override string ToString() {
				return string.Format("TestInt1: {0}\nTestInt2: {1}\nTestInt3: {2}\nTestString1: {3}\nTestString2: {4}\nTestList: {5}\nTestObject{6}",
					TestInt1,
					TestInt2,
					TestInt3,
					TestString1,
					TestString2,
					List2String(TestList),
					TestObject);
			}
			public static string List2String(List<int> l){
				string retVal = "";
				foreach (int i in l){
					retVal += string.Format("{0},", i);
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
			public AnotherTestClass() { }
			public override string ToString() {
				return string.Format("\n\tAnotherTestInt1: {0}\n\tAnotherTestInt2: {1}", AnotherTestInt1, AnotherTestInt2);
			}
		}
	}
}
