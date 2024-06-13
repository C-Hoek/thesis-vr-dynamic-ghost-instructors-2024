using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using Util;
using Logger = Logging.Logger;

namespace Performance
{
	public class TrialPerformance
	{
		// This list stores (errorValue, time).
		private List<Tuple<float, float>> _taskErrors;
		
		private Statistics _trialStatistics;
		public Statistics TrialStats
		{
			get => _trialStatistics;
		}
		
		private float? _completionTime;
		
		/// <summary>
		/// This method adds a single time-point error to the list of task errors.
		/// </summary>
		/// <param name="error"> The single time-point error to add to the list of task errors. </param>
		/// <param name="time"> The time at which the error was calculated. </param>
		public void AddTaskError(float error, float time)
		{
			_taskErrors ??= new List<Tuple<float, float>>();
			_taskErrors.Add(new Tuple<float, float>(error, time));
		}

		/// <summary>
		/// This method calculates and compiles all recorded statistics throughout the trial.
		/// All calculated sub-statistics are ordered as: task1, task2, combined.
		/// </summary>
		/// <param name="baselinePerformance"> The baseline performance of the student. </param>
		/// <param name="completionTime"> The time it took for the student to complete the trial. </param>
		/// <param name="timeExpired"> True if the trial was not completed within the time limit. False otherwise. </param>
		public void CalculateTrialStatistics(float? baselinePerformance, float completionTime, bool timeExpired)
		{
			var errors = _taskErrors.Where(x => x.Item2 < completionTime).Select(x => x.Item1).ToList();

			// Calculate distance values.
			var averageError = errors.Average();
			var maxError = errors.Max();
			var minError = errors.Min();

			// Obtain completion statistics.
			var timeToCompletion = completionTime;

			var completed = !timeExpired;

			// Calculate the task performance and learning effect.
			var taskPerformance = CalculateTaskPerformance(errors);
			var learningEffect = CalculateLearningEffect(baselinePerformance, taskPerformance);

			_completionTime = completionTime;

			// Combine all sub-statistics into a statistics object.
			_trialStatistics = new Statistics(averageError, maxError, minError, timeToCompletion, completed, taskPerformance, learningEffect);
		}

		/// <summary>
		/// This method calculates the learning effect observed between the baseline and this trial based on student performance.
		/// </summary>
		/// <param name="baselinePerformance"> The baseline performance of the student. </param>
		/// <param name="taskPerformance"> The performance of the student during the most recently completed trial. </param>
		/// <returns> A float representing the learning effect; defined as the difference between the baseline performance and current performance of the student. </returns>
		private float CalculateLearningEffect(float? baselinePerformance, float taskPerformance) 
		{
			return baselinePerformance.HasValue ? taskPerformance - baselinePerformance.Value : 0f;
		}

		/// <summary>
		/// This method calculates the performance of the student.
		/// Based on a time step of 0.02, Fixed Update will be executed 50 times per second.
		/// With a time limit of 30 seconds, there will be 1500 errors.
		/// To scale a possible sum of 1*1500 to 1-100, we divide the sum by 15.
		/// </summary>
		/// <returns> A float between 0 and 100 determining how well the student performed. </returns>
		private float CalculateTaskPerformance(List<float> errors)
		{
			var sum = errors.Sum();
			var scaler = 100 / errors.Count;
			Debug.Log($"{errors.Sum()}, {scaler}");
			return 100 - (sum * scaler);
		}

		/// <summary>
		/// This method returns a string containing all performance information.
		/// </summary>
		/// <returns> The performance log string. </returns>
		public string LogPerformance()
		{
			var errors = _taskErrors.Where(x => x.Item2 < _completionTime).ToList();
			
			var logString = $"Statistics{Logger.Delimiter}{_trialStatistics}{Logger.Delimiter}Obtained From{Logger.Delimiter}{Utils.LogList(errors)}";
			return logString;
		}
	}
}
