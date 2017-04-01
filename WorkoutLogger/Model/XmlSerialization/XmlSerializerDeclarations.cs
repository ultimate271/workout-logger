using System;
using System.Runtime.Serialization;

namespace XmlSerializer{
	
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public class XmlSerializableAttribute : Attribute{
		public bool AsXAttribute { get; set; }
		public XmlSerializableAttribute(){
			this.AsXAttribute = false;
		}
	}

	[XmlSerializable]
	public class XmlSerializableDateTime{
		[XmlSerializable]
		public int Year { get; set; }
		[XmlSerializable]
		public int Month { get; set; }
		[XmlSerializable]
		public int Day { get; set; }
		[XmlSerializable]
		public int Hour { get; set; }
		[XmlSerializable]
		public int Minute { get; set; }
		[XmlSerializable]
		public int Second { get; set; }
		[XmlSerializable]
		public int Millisecond { get; set; }

		public XmlSerializableDateTime (){}
		public XmlSerializableDateTime (DateTime t){
			this.Year = t.Year;
			this.Month = t.Month;
			this.Day = t.Day;
			this.Hour = t.Hour;
			this.Minute = t.Minute;
			this.Second = t.Second;
			this.Millisecond = t.Millisecond;
		}
		public DateTime ToDateTime(){
			return new DateTime(
				this.Year,
				this.Month,
				this.Day,
				this.Hour,
				this.Minute,
				this.Second,
				this.Millisecond
			);
		}
	}
	[XmlSerializable]
	public class XmlSerializableTimeSpan{
		[XmlSerializable]
		public int Days { get; set; }
		[XmlSerializable]
		public int Hours { get; set; }
		[XmlSerializable]
		public int Minutes { get; set; }
		[XmlSerializable]
		public int Seconds { get; set; }
		[XmlSerializable]
		public int Milliseconds { get; set; }

		public XmlSerializableTimeSpan() { }
		public XmlSerializableTimeSpan(TimeSpan t){
			this.Days = t.Days;
			this.Hours = t.Hours;
			this.Minutes = t.Minutes;
			this.Seconds = t.Seconds;
			this.Milliseconds = t.Milliseconds;
		}
		public TimeSpan ToTimeSpan(){
			return new TimeSpan(
				this.Days,
				this.Hours,
				this.Minutes,
				this.Seconds,
				this.Milliseconds
			);
		}
	}

	public class XmlSerializeContext{
		
	}
	public class XmlSerializerException : Exception {
		public XmlSerializerException() {
		}

		public XmlSerializerException(string message) : base(message) {
		}

		public XmlSerializerException(string message, Exception innerException) : base(message, innerException) {
		}

		protected XmlSerializerException(SerializationInfo info, StreamingContext context) : base(info, context) {
		}
	}
}