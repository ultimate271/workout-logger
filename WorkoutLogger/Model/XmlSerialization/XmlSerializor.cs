using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace XmlSerializer{
	public static class XmlSerializerExtensions{
		#region MeaninglessExtensions
		public static XElement SerializeToXml(this object o){ return o.SerializeToXml("Root"); }
		#endregion
		
		public static XElement SerializeToXml(this object o, string rootName){
			XElement retVal = new XElement(rootName);

			if (o is int) {
				retVal.Add(new XAttribute("type", "int"));
				retVal.Add(o);
			}//Base Case int
			else if (o is string) {
				retVal.Add(new XAttribute("type", "string"));
				retVal.Add(o);
			}//Base Case string
			else if (o.IsGenericList()) {
				Type genericType = o.GetType().GetGenericArguments().SingleOrDefault(); //Will throw an exception if o is an IList that has multiple generic types somehow
				if (genericType == null) { throw new Exception("XmlSerializerExtensions.SerializeToXml threw exception in case where o should have been a generic list but isn't somehow"); }
				if (genericType.IsXmlSerializable()) {
					string ofString = genericType.ToOfString();
					retVal.Add(new XAttribute("type", "list"), new XAttribute("of", ofString));

					foreach (object listObj in (o as IList)) {
						retVal.Add(listObj.SerializeToXml(ofString));
					}
				}
			}//Recursive Case Generic List
			else {
				if (!o.IsXmlSerializable()) throw new Exception("o is not xml Serializable"); //TODO put a custom exception here
				retVal.Add(new XAttribute("type", o.GetType().Name));
				foreach(PropertyInfo p in o.GetType().GetProperties()){
					if (p.IsXmlSerializable()){
						retVal.Add(p.GetValue(o).SerializeToXml(p.Name));
					}
				}

			}//Recursive Case

			return retVal;
		}//SerializeToXml
		
		public static object DeserializeFromXml(this XElement x){
			return null;
		}//DeserializeFromXml



		public static bool IsGenericList(this object o){
			return o.GetType().IsGenericList();
		}
		public static bool IsGenericList(this Type t){
			return (typeof(IList).IsAssignableFrom(t) && t.IsGenericType);
		}
		public static bool IsXmlSerializable(this object o){
			return IsXmlSerializable(o.GetType());
		}
		public static bool IsXmlSerializable(this Type t){
			if (t == typeof(int) || t == typeof(string) || t.IsGenericList()){
				return true;
			}
			return (from att in t.CustomAttributes where att.AttributeType == typeof(XmlSerializableAttribute) select att).Any();
		}
		public static bool IsXmlSerializable(this PropertyInfo p){
			return p.IsDefined(typeof(XmlSerializableAttribute));
		}

		public static string ToOfString(this Type t){
			return 
				t == typeof(int) ? "int"
				: t == typeof(string) ? "string"
				: t.IsGenericList() ? "list"
				: t.Name;
		}

	}
}