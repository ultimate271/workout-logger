using System;
using System.Xml.Linq;

namespace WorkoutLogger {
	namespace Model {
		public abstract class Workout : IXmlSerializable {
			#region Properties
			public string Name {
				get => _Name;
				set => _Name = value ?? "";
			}
			public Workout SubstitutionOf {
				get => _SubstitutionOf;
				set => _SubstitutionOf = value;
			}
			public int Id {
				get => _Id;
				set => _Id = value;
			}
			public abstract string Scheme { get; }
			#endregion
			#region Constructors
			
			#region Empty Constructors
			
			protected Workout(
			) : this(null, null, null) { }

			protected Workout(
				string Name
			) : this(Name, null, null) { }

			protected Workout(
				string Name, 
				Workout SubstitutionOf
			) : this(Name, SubstitutionOf, null) { }

			//This constructor intentionally creates a soft copy of "SubstitutionOf", as this is a reference to another workout which is associated, not aggregated, by this workout.
			protected Workout(
				Workout clone
			) : this(clone.Name, clone.SubstitutionOf, new XmlSerializableContext(clone.XmlContext)) { }

			protected Workout(
				XElement IncomingXml
			) : this(IncomingXml, null) { }
			
			#endregion

			
			#region Meaningful Constructors
			
			protected Workout(XElement IncomingXml, XmlSerializableContext XmlContext) {
				this.XmlContext = XmlContext; //The set method ensures that this is not a null value
				LoadFromXml(IncomingXml, XmlContext);
			}
			protected Workout(string Name, Workout SubstitutionOf, XmlSerializableContext XmlContext) {
				this.Name = Name;
				this.SubstitutionOf = SubstitutionOf;
				this.XmlContext = XmlContext; //The set method of XmlContext ensures that this is not a null value
			}
			#endregion
			#endregion
			#region Methods

			#endregion
			#region Implementations
			#region XMLSerializable
			public XmlSerializableContext XmlContext {
				get => _XmlContext;
				set => _XmlContext = value ?? new XmlSerializableContext();
			}
			public virtual void LoadFromXml(XElement IncomingXml, XmlSerializableContext XmlContext) {
				throw new NotImplementedException();
			}


			public virtual XElement ToXml(XmlSerializableContext XmlContext) {
				XmlContext = XmlContext ?? new XmlSerializableContext();
				XElement retVal = null;

				if (XmlContext.SerializeMode == XmlSerializableContext.XmlSerializeOptions.LocalFile){
					retVal = new XElement("Workout");
					retVal.Add(new XAttribute("Scheme", this.Scheme));
					if (XmlContext.IncludeId) retVal.Add(new XAttribute("Id", this.Id));
					if (this.Name != "") retVal.Add(new XElement("Name", this.Name));
					if (this.SubstitutionOf != null) retVal.Add(new XElement("SubstitutionOf", this.SubstitutionOf.Id));
				}

				return retVal;
			}
			#endregion
			#endregion
			#region private
			private string _Name = "";
			private Workout _SubstitutionOf = null;
			private XmlSerializableContext _XmlContext = new XmlSerializableContext();
			private int _Id;
			#endregion
		}
	}
}