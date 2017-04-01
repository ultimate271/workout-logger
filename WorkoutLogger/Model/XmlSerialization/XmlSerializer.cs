using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace XmlSerializer{
	public static class XmlSerializerExtensions{
		#region MeaninglessExtensions
		public static XElement SerializeToXml(this object o){ return o.SerializeToXml("Root"); }
		public static object DeserializeFromXml(this XElement x) { return x.DeserializeFromXml(typeof(XmlSerializerExtensions).Assembly); }
		#endregion
		
		//TODO Add "DateTime" and "TimeSpan" as a potential base case
		public static XElement SerializeToXml(this object o, string rootName){
			XElement retVal = new XElement(rootName);
			retVal.Add(new XAttribute("type", o.GetType().TypeToString()));
			
			if (o is int || o is string) {
				retVal.Add(o);
			}//Base Case int or string
			else if (o is DateTime){
				retVal = (new XmlSerializableDateTime((DateTime)o)).SerializeToXml(rootName);
				retVal.Attribute("type").SetValue("DateTime");
			}
			else if (o is TimeSpan){
				retVal = (new XmlSerializableTimeSpan((TimeSpan)o)).SerializeToXml(rootName);
				retVal.Attribute("type").SetValue("TimeSpan");
			}
			else if (o.IsGenericList()) {
				Type genericType = o.GetType().GetGenericArguments().SingleOrDefault(); //Will throw an exception if o is an IList that has multiple generic types somehow
				if (genericType == null) { throw new XmlSerializerException("XmlSerializerExtensions.SerializeToXml threw exception in case where o should have been a generic list but isn't somehow"); }
				if (genericType.IsXmlSerializable()) {
					//string ofString = genericType.TypeToString();
					foreach (object listObj in (o as IList)) {
						string nodeName = listObj.GetType().IsGenericList() ? "list" : listObj.GetType().TypeToString();
						retVal.Add(listObj.SerializeToXml(nodeName));
					}
				}
			}//Recursive Case Generic List
			else {
				if (!o.IsXmlSerializable()) throw new XmlSerializerException("o is not xml Serializable");
				foreach(PropertyInfo p in o.GetType().GetProperties()){
					if (p.IsXmlSerializable()){
						if (p.GetXmlAttribute().AsXAttribute) {
							if (!typeof(int).IsAssignableFrom(p.PropertyType) && !typeof(string).IsAssignableFrom(p.PropertyType)) throw new XmlSerializerException($"Cannot serialize non integer or non string type {p.PropertyType} to an XAttribute");
							retVal.Add(new XAttribute(p.Name, p.GetValue(o)));
						}
						else {
							retVal.Add(p.GetValue(o).SerializeToXml(p.Name));
						}
					}
				}

			}//Recursive Case

			return retVal;
		}//SerializeToXml
		public static object DeserializeFromXml(this XElement x, Assembly a){
			object retVal = null;

			//string typeValue = x.AttributeValue("type");
			Type returnType = x.Attribute("type").Value.StringToType(a) ?? throw new XmlSerializerException($"{x.Attribute("type").Value} is not a type in Assembly {a.ToString()}");

			if (returnType == typeof(int)) {
				retVal = Int32.Parse(x.Value);
			}//Base Case int

			else if (returnType == typeof(string)) {
				retVal = x.Value;
			}//Base Case string

			else if (returnType == typeof(DateTime)){
				x.Attribute("type").SetValue(typeof(XmlSerializableDateTime).TypeToString());
				retVal = (x.DeserializeFromXml(a) as XmlSerializableDateTime).ToDateTime();
			}
			else if (returnType == typeof(TimeSpan)){
				x.Attribute("type").SetValue(typeof(XmlSerializableTimeSpan).TypeToString());
				retVal = (x.DeserializeFromXml(a) as XmlSerializableTimeSpan).ToTimeSpan();
			}
			else if (returnType.IsGenericList()) {
				//string ofValue = x.AttributeValue("of");
				IList tempRetVal = (IList)Activator.CreateInstance(returnType);
				Type ofType = returnType.GetGenericArguments().SingleOrDefault();

				foreach (XElement child in x.Elements()) {
					Type descType = child.Attribute("type").Value.StringToType();
					if (!ofType.IsAssignableFrom(descType)) throw new XmlSerializerException("XmlSerializerExtensions.DeserializeFromXml: List member type mismatch");
					tempRetVal.Add(child.DeserializeFromXml(a));
				}
				retVal = tempRetVal;
			}//Recursive Case List
			else {
				retVal = Activator.CreateInstance(returnType);

				//This query checks all of the children of the current XElement and matches them to properties, then assigns those properties in the foreach loop
				var query =
					from child in x.Elements()
					join pi in returnType.GetProperties()
					on child.Name equals pi.Name
					where pi.IsXmlSerializable()
					select new { Property = pi, Value = child.DeserializeFromXml(a) };
				
				foreach (var pair in query){
					if (!pair.Value.GetType().IsXmlSerializable()) throw new XmlSerializerException($"{pair.Value.GetType().Name} is not xml serializable");
					pair.Property.SetValue(retVal, pair.Value);
				}

				//The following code does the same thing but for xattributes
				var attQuery =
					from att in x.Attributes()
					join pi in returnType.GetProperties()
					on att.Name equals pi.Name
					where pi.IsXmlSerializable() && att.Name != "type"
					select new { Property = pi, Value = att.Value };

				foreach (var pair in attQuery){
					if (typeof(int).IsAssignableFrom(pair.Property.PropertyType)){
						pair.Property.SetValue(retVal, Int32.Parse(pair.Value));
					}
					else if (typeof(string).IsAssignableFrom(pair.Property.PropertyType)){
						pair.Property.SetValue(retVal, pair.Value);
					}
					else{
						throw new XmlSerializerException($"Cannot assign property of type {pair.Property.PropertyType} from an XAttribute Value");
					}
				}
			}//Recursive Case Object


			return retVal;
		}//DeserializeFromXml


		#region Helper Functions
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
			if (t == typeof(int) || t == typeof(string) || t.IsGenericList() || t == typeof(DateTime) || t == typeof(TimeSpan)){
				return true;
			}
			return t.IsDefined(typeof(XmlSerializableAttribute));
		}
		public static bool IsXmlSerializable(this PropertyInfo p){
			return p.IsDefined(typeof(XmlSerializableAttribute));
		}
		public static XmlSerializableAttribute GetXmlAttribute(this PropertyInfo p){
			return p.IsXmlSerializable() ? p.GetCustomAttribute(typeof(XmlSerializableAttribute)) as XmlSerializableAttribute : null;
		}

		public static string TypeToString(this Type t){
			return 
				t == typeof(int) ? "int"
				: t == typeof(string) ? "string"
				: t.IsGenericList() ? $"list({t.GetGenericArguments().SingleOrDefault().TypeToString()})"
				: t.Name;
		}

		/// <summary>
		/// Returns the single value of the attribute with value == attributeName.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="attributeName"></param>
		/// <returns>The value of the attribute with attributeName, or null. Throws an exception if there is more than one attribute with Name == attribtueName</returns>
		//public static string AttributeValue(this XElement x, string attributeName){
		//	return
		//		(from xatt in x.Attributes()
		//		 where xatt.Name == attributeName
		//		 select xatt.Value).SingleOrDefault(); //Throws an exception if there is more than one attribute with Name == attributeName
		//}


		public static Type StringToType(this string s){
			return s.StringToType(typeof(XmlSerializerExtensions).Assembly);
		}
		public static Type StringToType(this string s, Assembly a) {
			if (string.IsNullOrEmpty(s)) throw new XmlSerializerException("XmlSerializerExtensions.StringToType: Cannot convert empty or null string to Type");
			
			Type retVal = null;

			Match m = Regex.Match(s, @"^list\((.*)\)");
			if (s == "int") { retVal = typeof(int); }
			else if (s == "string") { retVal = typeof(string); }
			else if (s == "DateTime") { retVal = typeof(DateTime); }
			else if (s == "TimeSpan") { retVal = typeof(TimeSpan); }
			else if (m.Success) {
				retVal = typeof(List<>).MakeGenericType(m.Groups[1].Value.StringToType(a));
			}
			else {
				retVal =
					(from ti in a.DefinedTypes
					 where ti.Name == s
					 select ti as Type).SingleOrDefault();
			}
			return retVal;
		}
		#endregion
	}
}