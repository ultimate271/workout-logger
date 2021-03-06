﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using XmlSerializer;

using WLCore;

namespace WLModel{
	
	/// <summary>
	/// Lets start with some requirements for Model
	/// The "top level" element in the model will be the List of Logs.
	/// In every log is a reference to the workout of that log.
	/// This reference must be a reference in the List of Workouts
	/// Also, the type of result in a log must match the type of workout in the Workout_Result type dictionary
	/// Each workout in the list of workouts must be unique
	/// </summary>
	public class Model {
		private List<WL_Log> Logs { get; set; } = new List<WL_Log>();
		private List<WL_Workout> Workouts { get; set; } = new List<WL_Workout>();

		#region Public Methods
		public void AddLogs(IEnumerable<WL_Log> Logs){
			foreach (WL_Log log in Logs){
				AddLog(log);
			}
		}
		public void AddWorkout(WL_Workout Workout) {
			if (GetWorkout(Workout) != null) this.Workouts.Add(Workout);
		}

		public void AddWorkout(XElement xWorkout){
			WL_Workout workout = xWorkout.DeserializeFromXml() as WL_Workout;
			AddWorkout(workout);
		}

		/// <summary>
		/// Checks the database for a workout that equals the current workout
		/// If it doesn't exist in the database, return null
		/// </summary>
		/// <param name="workout"></param>
		/// <returns></returns>
		public WL_Workout GetWorkout(WL_Workout workout){
			//If an equivelent workout already exists in the list, return that
			foreach (WL_Workout existingWorkout in Workouts){
				if (existingWorkout.Equals(workout)){
					return existingWorkout;
				}
			}
				
			return null;
		}
		/// <summary>
		/// Adds a log to the local model
		/// </summary>
		/// <param name="log">The log to add</param>
		/// <param name="SupressTypeMismatch">If set to true, this method will not throw an exception if the log result does not match the workout type</param>
		public void AddLog(WL_Log log, bool SupressTypeMismatch = false) {
			log.Workout = this.GetWorkout(log.Workout);
			if (!IsWellTyped(log) && !SupressTypeMismatch) throw new WL_Exception($"Workout type {log.Workout.GetType().Name} is not compatible with result type {log.Result.GetType().Name}");
			Logs.Add(log);
		}

		#endregion

		/// <summary>
		/// Returns true if the result of the log is a valid type for the workout of the log
		/// </summary>
		/// <param name="log"></param>
		/// <returns></returns>
		private bool IsWellTyped(WL_Log log){
			//Get the result type of the log
			Type resultType = log.Result.GetType();
			//Check to make sure the result type has a compatibilty attribute
			if (!resultType.IsDefined(typeof(WL_ResultCompatabilityAttribute))) return false;
			//Extract the attribute
			WL_ResultCompatabilityAttribute compatibiiltyAtt = resultType.GetCustomAttribute<WL_ResultCompatabilityAttribute>();
			//check the workout type of the log against all the possible compatible types of the result
			foreach (Type compatibleType in compatibiiltyAtt.WorkoutTypes){
				if (compatibleType.IsAssignableFrom(log.Workout.GetType())) return true;
			}
			//None of the compatible types matched, so return false
			return false;
		}
	}
	
}
