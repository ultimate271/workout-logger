using System;
using System.Xml.Linq;

namespace WorkoutLogger{
	namespace Model{
		/// <summary>
		/// Provides a pretty "default" implementation of workout.
		/// Use only when implementation of a whole new scheme of workout is either still in progress
		/// or the workout is so wonky that it doesn't fit into any scheme and its "one of a kind" so to speak
		/// </summary>
		public class MiscWorkout : Workout{
			#region Properties
			public string Description {
				get { return _Description; }
				set { _Description = value; }
			}
			#endregion
			#region Constructors
			#region Empty Constructors
			public MiscWorkout() : base() { }
			//public MiscWorkout() : this (null, null, null, null) {}
			//public MiscWorkout(
			//	string Name
			//) : this(Name, null, null, null) { }
			//public MiscWorkout(
			//	string Name,
			//	Workout SubstitutionOf
			//) : this(Name, SubstitutionOf, null, null) { }
			//public MiscWorkout(
			//	string Name,
			//	Workout SubstitutionOf,
			//	XmlSerializableContext XmlContext
			//) : this(Name, SubstitutionOf, XmlContext, null) { }

			
			//public MiscWorkout(
			//	XElement IncomingXml
			//) : this(IncomingXml, null) { }
			#endregion
			#region Meaningful Constructors

			//public MiscWorkout(
			//	XElement IncomingXml, 
			//	XmlSerializableContext XmlContext
			//) {
			//	base.XmlContext = XmlContext;
			//	this.LoadFromXml(IncomingXml);
			//}
			//public MiscWorkout(
			//	string Name, 
			//    Workout SubstitutionOf, 
			//    XmlSerializableContext XmlContext,
			//    string Description
			//) : base (Name, SubstitutionOf, XmlContext){
			//	this.Description = Description;
			//}

			#endregion
			#endregion
			#region Methods

			#endregion
			#region Implementations
			#region Workout
			public override string Scheme {
				get { return SCHEME; }
			}
			#endregion
			#region XMLSerializable
			//public override void LoadFromXml(XElement incomingXml, XmlSerializableContext XmlContext){
			//	base.LoadFromXml(incomingXml, XmlContext);
			//}

			///// <summary>
			///// 
			///// </summary>
			///// <param name="XmlContext"></param>
			///// <returns>An XElement that contains a Workout node with some metadata nodes</returns>
			//public override XElement ToXml(XmlSerializableContext XmlContext){
			//	XElement retVal = null;
			//	if (XmlContext.SerializeMode == XmlSerializableContext.XmlSerializeOptions.LocalFile){
			//		retVal = base.ToXml(XmlContext);
			//		retVal.Add(new XElement("Description", this.Description));
			//	}
			//	return retVal;
			//}

			#endregion
			#endregion
			#region private
			private const string SCHEME = "Misc";
			private string _Description;
			#endregion

		}
	}
}