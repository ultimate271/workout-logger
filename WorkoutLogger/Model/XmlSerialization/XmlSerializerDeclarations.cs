using System;

namespace XmlSerializer{
	
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public class XmlSerializableAttribute : Attribute{ }
}