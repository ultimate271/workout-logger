using System;
using System.Xml.Linq;

namespace WorkoutLogger {
	namespace Model {
		public abstract class Workout : XmlSerializable {
			#region Defaults
			#endregion
			#region Properties
			public string Name {
				get { return _Name; }
				set { _Name = value == null ? "" : value; }
			}
			public Workout SubstitutionOf {
				get { return _SubstitutionOf; }
				set { _SubstitutionOf = value; }
			}
			#endregion
			#region Constructors
			
			#region Empty Constructors
			
			protected Workout() : this(null, null, null) { }
			protected Workout(string Name) : this(Name, null, null) { }
			protected Workout(string Name, Workout SubstitutionOf) : this(Name, SubstitutionOf, null) { }

			protected Workout(Workout clone) : this(clone.Name, clone.SubstitutionOf, new XmlSerializableContext(clone.XmlContext)) { }
			protected Workout(XElement xml) : this(xml, null) { }
			
			#endregion

			
			#region Meaningful Constructors
			
			protected Workout(XElement xml, XmlSerializableContext XmlContext) {
				this.XmlContext = XmlContext != null ? XmlContext : this.XmlContext;
				LoadFromXml(xml);
			}
			protected Workout(string Name, Workout SubstitutionOf, XmlSerializableContext XmlContext) {
				this.Name = Name;
				this.SubstitutionOf = SubstitutionOf;
				this.XmlContext = XmlContext;
			}
			#endregion
			#endregion
			#region Methods

			#endregion
			#region Implementations
			#region XMLSerializable
			public XmlSerializableContext XmlContext {
				get { return _XmlContext; }
				set { _XmlContext = value == null ? new XmlSerializableContext() : value; }
			}
			public virtual void LoadFromXml(XElement incomingXml) {
				throw new NotImplementedException();
			}
			public virtual XElement ToXml() {
				throw new NotImplementedException();
			}
			#endregion
			#endregion
			#region private
			private string _Name = "";
			private Workout _SubstitutionOf = null;
			private XmlSerializableContext _XmlContext = new XmlSerializableContext();
			#endregion
		}
	}
}