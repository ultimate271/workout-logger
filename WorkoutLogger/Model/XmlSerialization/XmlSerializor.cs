using System;
using System.Xml.Linq;
using System.Linq;

namespace XmlSerializor{
	public static class XmlSerializorExtensions{
		#region MeaninglessExtensions
		public static XElement SerializeToXml(this object o){ return o.SerializeToXml("Root"); }
		#endregion
		public static XElement SerializeToXml(this object o, string rootName){
			return null;
		}
		
		public static object DeserializeFromXml(this XElement x){
			return null;
		}

	}
}