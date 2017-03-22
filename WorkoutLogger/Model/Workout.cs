using System;
using System.Linq;
using System.Xml.Linq;

namespace WorkoutLogger {
	namespace Model {
		public abstract class Workout : IXmlSerializable {
			#region Properties
			public string Name {
				get => _Name;
				set => _Name = value ?? "";
			}
			public int Id {
				get => _Id;
				set => _Id = value;
			}
			public string Comment{
				get => _Comment;
				set => _Comment = value;
			}
			public abstract string Scheme { get; }
			#endregion
			#region Constructors

			#region Empty Constructors

			//This constructor intentionally creates a soft copy of "SubstitutionOf", as this is a reference to another workout which is associated, not aggregated, by this workout.
			protected Workout() { }

			protected Workout(
				XElement IncomingXml
			) : this(IncomingXml, null) { }

			#endregion


			#region Meaningful Constructors

			protected Workout(
				Workout clone
			){
				
			}

			protected Workout(
				XElement IncomingXml, 
				XmlSerializableContext XmlContext
			){
				this.XmlContext = XmlContext ?? new XmlSerializableContext(); //The set method ensures that the value is not null, belt and suspenders here
				LoadFromXml(IncomingXml, XmlContext);
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
				XmlContext = XmlContext ?? this.XmlContext;
				if (XmlContext.SerializeMode == XmlSerializableContext.XmlSerializeOptions.LocalFile){
					var info = new {
						Name = (from e in IncomingXml.Elements("Name")
						       select e.Value).SingleOrDefault(),
						Id = XmlContext.IncludeId
							? (from e in IncomingXml.Attributes("Id")
							   select e.Value).SingleOrDefault()
							: null 
						
					};
					this.Name = info.Name;
				}
			}


			public virtual XElement ToXml(XmlSerializableContext XmlContext) {
				XmlContext = XmlContext ?? this.XmlContext;
				XElement retVal = null;

				if (XmlContext.SerializeMode == XmlSerializableContext.XmlSerializeOptions.LocalFile){
					retVal = new XElement("Workout",
						                                      new XAttribute("Scheme", this.Scheme),
						XmlContext.IncludeId ?                new XAttribute("Id", this.Id) : null,
						!string.IsNullOrEmpty(this.Name) ?    new XElement("Name", this.Name) : null,
						!string.IsNullOrEmpty(this.Comment) ? new XElement("Comment", this.Comment) : null
					);
				}

				return retVal;
			}
			#endregion
			#endregion
			#region private
			private string _Name = "";
			private XmlSerializableContext _XmlContext = new XmlSerializableContext();
			private int _Id;
			private string _Comment;
			#endregion
		}
	}
}