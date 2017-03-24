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
					string ofString = genericType.TypeToString();
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
			return x.DeserializeFromXml(typeof(XmlSerializerExtensions).Assembly);
		}

		public static object DeserializeFromXml(this XElement x, Assembly a){
			object retVal = null;

			string typeValue = x.AttributeValue("type");

			if (typeValue == "int") {
				retVal = Int32.Parse(x.Value);
			}//Base Case int

			else if (typeValue == "string") {
				retVal = x.Value;
			}//Base Case string

			else if (typeValue == "list") {
				string ofValue = x.AttributeValue("of");
				Type returnType = typeValue.StringToType(ofValue, a);
				IList list = (IList)Activator.CreateInstance(returnType);

				foreach (XElement desc in x.Descendants()) {
					if (desc.AttributeValue("type") != ofValue) throw new Exception("XmlSerializerExtensions.DeserializeFromXml: List member type mismatch"); //TODO make a custom exception
					list.Add(desc.DeserializeFromXml());
				}
				retVal = list;
			}//Recursive Case List
			else {
				Type returnType = typeValue.StringToType(null, a);
				retVal = Activator.CreateInstance(returnType);
				var query =
					from desc in x.Descendants()
					join pi in returnType.GetProperties()
					on desc.Name equals pi.Name
					select new { property = pi, value = desc.DeserializeFromXml() };

				foreach (var pair in query){
					pair.property.SetValue(retVal, pair.value);
				}
			}//Recursive Case Object


			return retVal;
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

		public static string TypeToString(this Type t){
			return 
				t == typeof(int) ? "int"
				: t == typeof(string) ? "string"
				: t.IsGenericList() ? "list"
				: t.Name;
		}

		public static string AttributeValue(this XElement x, string attributeName){
			return
				(from xatt in x.Attributes()
				 where xatt.Name == attributeName
				 select xatt.Value).SingleOrDefault(); //Throws an exception if there is more than one attribute with Name == attributeName
		}

		//Not so sure how I feel about using this method

		public static Type StringToType(this string s) {
			return s.StringToType("");
		}
		public static Type StringToType(this string s, string ofString){
			return s.StringToType(ofString, typeof(XmlSerializerExtensions).Assembly);
		}
		public static Type StringToType(this string s, string ofString, Assembly a) {
			if (string.IsNullOrEmpty(s)) throw new Exception("XmlSerializerExtensions.StringToType: Cannot convert empty or null string to Type");
			
			Type retVal = null;
			
			if (s == "int") { retVal = typeof(int); }
			else if (s == "string") { retVal = typeof(string); }
			else if (s == "list"){ //TODO List of lists not supported here
				retVal = typeof(List<>).MakeGenericType(ofString.StringToType(null, a));
			}
			else {
				retVal =
					(from ti in a.DefinedTypes
					where ti.Name == s
					select ti as Type).SingleOrDefault();
			}
			return retVal;
		}

	}
}