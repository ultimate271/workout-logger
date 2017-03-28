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
					retVal.Add(new XAttribute("type", o.GetType().TypeToString()));

					//string ofString = genericType.TypeToString();
					foreach (object listObj in (o as IList)) {
						string nodeName = listObj.GetType().IsGenericList() ? "list" : listObj.GetType().TypeToString();
						retVal.Add(listObj.SerializeToXml(nodeName));
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
		public static object DeserializeFromXml(this XElement x, Assembly a){
			object retVal = null;

			string typeValue = x.AttributeValue("type");
			Type returnType = typeValue.StringToType(a);

			if (returnType == typeof(int)) {
				retVal = Int32.Parse(x.Value);
			}//Base Case int

			else if (returnType == typeof(string)) {
				retVal = x.Value;
			}//Base Case string

			else if (returnType.IsGenericList()) {
				//string ofValue = x.AttributeValue("of");
				IList tempRetVal = (IList)Activator.CreateInstance(returnType);
				Type ofType = returnType.GetGenericArguments().SingleOrDefault();

				foreach (XElement child in x.Elements()) {
					Type descType = child.AttributeValue("type").StringToType();
					if (!ofType.IsAssignableFrom(descType)) throw new Exception("XmlSerializerExtensions.DeserializeFromXml: List member type mismatch"); //TODO make a custom exception
					tempRetVal.Add(child.DeserializeFromXml(a));
				}
				retVal = tempRetVal;
			}//Recursive Case List
			else {
				retVal = Activator.CreateInstance(returnType);
				var query =
					from child in x.Elements()
					join pi in returnType.GetProperties()
					on child.Name equals pi.Name
					where pi.IsXmlSerializable()
					select new { Property = pi, Value = child.DeserializeFromXml(a) };

				foreach (var pair in query){
					pair.Property.SetValue(retVal, pair.Value);
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
			if (t == typeof(int) || t == typeof(string) || t.IsGenericList()){
				return true;
			}
			return t.IsDefined(typeof(XmlSerializableAttribute));
		}
		public static bool IsXmlSerializable(this PropertyInfo p){
			return p.IsDefined(typeof(XmlSerializableAttribute));
		}

		public static string TypeToString(this Type t){
			return 
				t == typeof(int) ? "int"
				: t == typeof(string) ? "string"
				: t.IsGenericList() ? string.Format("list({0})", t.GetGenericArguments().SingleOrDefault().TypeToString())
				: t.Name;
		}

		/// <summary>
		/// Returns the single value of the attribute with value == attributeName.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="attributeName"></param>
		/// <returns>The value of the attribute with attributeName, or null. Throws an exception if there is more than one attribute with Name == attribtueName</returns>
		public static string AttributeValue(this XElement x, string attributeName){
			return
				(from xatt in x.Attributes()
				 where xatt.Name == attributeName
				 select xatt.Value).SingleOrDefault(); //Throws an exception if there is more than one attribute with Name == attributeName
		}


		public static Type StringToType(this string s){
			return s.StringToType(typeof(XmlSerializerExtensions).Assembly);
		}
		public static Type StringToType(this string s, Assembly a) {
			if (string.IsNullOrEmpty(s)) throw new Exception("XmlSerializerExtensions.StringToType: Cannot convert empty or null string to Type");
			
			Type retVal = null;

			Match m = Regex.Match(s, @"^list\((.*)\)");
			if (s == "int") { retVal = typeof(int); }
			else if (s == "string") { retVal = typeof(string); }
			else if (m.Success){
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