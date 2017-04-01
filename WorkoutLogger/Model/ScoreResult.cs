﻿using System;
using System.Collections.Generic;
using XmlSerializer;

namespace WorkoutLogger {
	namespace Model {
		[WL_ResultCompatability(typeof(WL_MiscWorkout))]
		[XmlSerializable]
		public class WL_ScoreResult : WL_Result {
			[XmlSerializable]
			public int Score { get; set; }
		}
	}
}