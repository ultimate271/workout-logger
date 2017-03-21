using System;
using System.Xml.Linq;
namespace WorkoutLogger {
	namespace Model {
		/// <summary>
		/// Implemented by all classes in the model which can be interpreted as Xml
		/// A class which implements this interface can be converted to xml and created from xml.
		/// </summary>
		public interface IXmlSerializable {
			/// <summary>
			/// The default or "current" XmlContext, to be used in the other methods if the
			/// XmlSerializableContext is null or not given.
			/// </summary>
			XmlSerializableContext XmlContext { get; set; }
			/// <summary>
			/// This method will be used to return an XElement which represents the current object
			/// under the XmlContext. If XmlContext is not given, the extension method
			/// XElement ToXml() will use this.XmlContext in its place. This should be implemented
			/// similarly if the XmlContext passed to the method is null.
			/// </summary>
			/// <param name="XmlContext"></param>
			/// <returns></returns>
			XElement ToXml(
				XmlSerializableContext XmlContext
			);

			/// <summary>
			/// This method is used to fill the current object based upon the existing xml, under
			/// the XmlContext. If XmlContext is not given, the extension method
			/// void LoadFromXml(XElement incomingXml) will use this.XmlContext in its place. This should be 
			/// implemented similarly if the XmlContext passed to the method is null.
			/// </summary>
			/// <param name="incomingXml"></param>
			/// <param name="XmlContext"></param>
			void LoadFromXml(
				XElement incomingXml, 
				XmlSerializableContext XmlContext
			);
		}

		/// <summary>
		/// Provides Extentions methods for IXmlSerializable
		/// These methods will be invoked if the XmlSerializableContext is not given in an IXmlSerializable object
		/// </summary>
		public static class IXmlSerializableExtensions
		{

			public static XElement ToXml(this IXmlSerializable xmlSerializableObj)
			{
				return xmlSerializableObj.ToXml(xmlSerializableObj.XmlContext);
			}
			public static void LoadFromXml(this IXmlSerializable xmlSerializableObj,
										   XElement incomingXml)
			{
				xmlSerializableObj.LoadFromXml(incomingXml, xmlSerializableObj.XmlContext);
			}
		}

		/// <summary>
		/// Includes properties to tell a class that implements XmlSerializable what sort of xml to output
		/// Simply add a property to this class, and the remaining functions will work as intended through reflection
		/// </summary>
		public class XmlSerializableContext {
			public enum XmlSerializeOptions { LocalFile, DBRecord };
			public XmlSerializeOptions SerializeMode { get; set; } = XmlSerializeOptions.LocalFile;
			public bool IncludeId { get; set; } = false;

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

		public class TestClass : IXmlSerializable {
			public XmlSerializableContext XmlContext { get; set; } = new XmlSerializableContext();

			public TestClass(XmlSerializableContext XmlContext) {
				this.XmlContext = XmlContext;
			}
			public void LoadFromXml(XElement incomingXml, XmlSerializableContext XmlContext) {
			}

			public XElement ToXml(XmlSerializableContext XmlContext) {
				System.Console.WriteLine("TestClass.ToXml() {0}", "" + XmlContext);
				return null;
			}
		}
	}
}
