using System;
using System.Xml.Linq;
namespace WorkoutLogger {
	namespace Model {
		/// <summary>
		/// Implemented by all classes in the model which can be interpreted as Xml
		/// A class which implements this interface can be converted to xml and created from xml.
		/// Default opts is ""
		/// </summary>
		public interface XmlSerializable {
			XmlSerializableContext XmlContext { get; set; }
			XElement ToXml();
			void LoadFromXml(XElement incomingXml);
		}

		//public static class XmlSerializableExtensions {

		//	public static XElement ToXml(this XmlSerializable xmlSerializableObj) {
		//		return xmlSerializableObj.ToXml(null);
		//	}
		//	public static void LoadFromXml(this XmlSerializable xmlSerializableObj, 
		//	                               XElement incomingXml) {
		//		xmlSerializableObj.LoadFromXml(incomingXml, null);
		//	}
		//}

		/// <summary>
		/// Includes properties to tell a class that implements XmlSerializable what sort of xml to output
		/// Simply add a property to this class, and the remaining functions will work as intended through reflection
		/// </summary>
		public class XmlSerializableContext {
			public bool IncludeMeta { get; set; }
			public bool AnotherThing { get; set; }
			public string SomeString { get; set; }

			public XmlSerializableContext() { }
			/// <summary>
			/// Uses reflection to deep copy every property from the clone object into this
			/// </summary>
			/// <param name="clone"></param>
			public XmlSerializableContext(XmlSerializableContext clone) {
				foreach (System.Reflection.PropertyInfo property in clone.GetType().GetProperties()) {
					this.GetType().GetProperty(property.Name).SetValue(this, property.GetValue(clone));
				}
			}

			/// <summary>
			/// Uses reflection to print out all of the properties of this class in the form Name=Value;
			/// </summary>
			/// <returns></returns>
			public override string ToString() {
				string retVal = "XmlSerializableContext: ";
				foreach(System.Reflection.PropertyInfo property in this.GetType().GetProperties()) {
					retVal += string.Format("{0}={1};", property.Name, property.GetValue(this));
				}
				return retVal;
			}
		}

		public class TestClass : XmlSerializable {
			public XmlSerializableContext XmlContext { get; set; } = new XmlSerializableContext();

			public TestClass(XmlSerializableContext XmlContext) {
				this.XmlContext = XmlContext;
			}
			public void LoadFromXml(XElement incomingXml) {
			}

			public XElement ToXml() {
				
				System.Console.WriteLine("TestClass.ToXml() {0}", "" + XmlContext);
				return null;
			}
		}
	}
}
