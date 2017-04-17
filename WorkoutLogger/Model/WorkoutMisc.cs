using System;
using System.Xml.Linq;
using XmlSerializer;

namespace WorkoutLogger{

	/// <summary>
	/// Provides a pretty "default" implementation of workout.
	/// Use only when implementation of a whole new scheme of workout is either still in progress
	/// or the workout is so wonky that it doesn't fit into any scheme and its "one of a kind" so to speak
	/// </summary>
	[XmlSerializable]
	public class WL_WorkoutMisc : WL_Workout{
		#region Properties
		[XmlSerializable]
		public string Description {
			get { return _Description; }
			set { _Description = value; }
		}
		#endregion
		#region Constructors
		#region Empty Constructors
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
		#region object
		public override string ToString() {
			return $"{base.ToString()}\nDescription: {this.Description}";
		}
			
		public override bool Equals(object obj) {
			if (obj == null || GetType() != obj.GetType()) {
				return false;
			}

			WL_WorkoutMisc inWorkout = (WL_WorkoutMisc)obj;
			return
				this.MetadataEquals(inWorkout) &&
				this.Description != null ? this.Description.Equals(inWorkout.Description) : false;
		}


		public override int GetHashCode() {

			return this.MetadataGetHashCode() * this.Description.GetHashCode();
		}
			
		#endregion
		#endregion
		#region private
		private string _Description;
		#endregion

	}

}