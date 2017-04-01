using System;
using System.Linq;
using System.Xml.Linq;
using XmlSerializer;

namespace WorkoutLogger {
	namespace Model {
		[XmlSerializable]
		public abstract class WL_Workout {
			#region Properties
			[XmlSerializable]
			public string Name {
				get => _Name;
				set => _Name = value ?? "";
			}
			//[XmlSerializable(AsXAttribute = true)]
			//public int Id {
			//	get => _Id;
			//	set => _Id = value;
			//}
			[XmlSerializable]
			public string Comment{
				get => _Comment;
				set => _Comment = value;
			}
			#endregion
			#region Constructors

			#region Empty Constructors

			protected WL_Workout() { }

			//protected Workout(
			//	XElement IncomingXml
			//) : this(IncomingXml, null) { }

			#endregion


			#region Meaningful Constructors

			protected WL_Workout(
				WL_Workout clone
			){
				
			}

			//protected Workout(
			//	XElement IncomingXml, 
			//	XmlSerializableContext XmlContext
			//){
			//	this.XmlContext = XmlContext ?? new XmlSerializableContext(); //The set method ensures that the value is not null, belt and suspenders here
			//	LoadFromXml(IncomingXml, XmlContext);
			//}
			#endregion
			#endregion
			#region Methods

			#endregion
			#region Implementations
			#region XMLSerializable
			//public XmlSerializableContext XmlContext {
			//	get => _XmlContext;
			//	set => _XmlContext = value ?? new XmlSerializableContext();
			//}


			//public virtual void LoadFromXml(XElement IncomingXml, XmlSerializableContext XmlContext) {
			//	XmlContext = XmlContext ?? this.XmlContext;
			//	if (XmlContext.SerializeMode == XmlSerializableContext.XmlSerializeOptions.LocalFile){
			//		var info = new {
			//			Name = (from e in IncomingXml.Elements("Name")
			//			       select e.Value).SingleOrDefault(),
			//			Id = XmlContext.IncludeId
			//				? (from e in IncomingXml.Attributes("Id")
			//				   select e.Value).SingleOrDefault()
			//				: null 

			//		};
			//		this.Name = info.Name;
			//	}
			//}


			//public virtual XElement ToXml(XmlSerializableContext XmlContext) {
			//	XmlContext = XmlContext ?? this.XmlContext;
			//	XElement retVal = null;

			//	if (XmlContext.SerializeMode == XmlSerializableContext.XmlSerializeOptions.LocalFile){
			//		retVal = new XElement("Workout",
			//			                                      new XAttribute("Scheme", this.Scheme),
			//			XmlContext.IncludeId ?                new XAttribute("Id", this.Id) : null,
			//			!string.IsNullOrEmpty(this.Name) ?    new XElement("Name", this.Name) : null,
			//			!string.IsNullOrEmpty(this.Comment) ? new XElement("Comment", this.Comment) : null
			//		);
			//	}

			//	return retVal;
			//}
			#endregion
			#region object
			public override string ToString() {
				return $"Name: {this.Name}\nComment: {this.Comment}";
			}
			public override int GetHashCode() {
				return ("" + this.Comment + this.Name).GetHashCode();
			}
			public override bool Equals(object obj) {
				bool retVal = true;

				//Check to make sure the types match and cast the parameter as a workout if they do
				if (obj == null || GetType() != obj.GetType()) {
					return false;
				}
				WL_Workout incomingWorkout = obj as WL_Workout;

				retVal &= this.Comment == incomingWorkout.Comment;
				retVal &= this.Name == incomingWorkout.Name;


				return retVal;
				
			}

			#endregion
			#endregion
			#region private
			private string _Name = "";
			private string _Comment;
			#endregion
		}
	}
}