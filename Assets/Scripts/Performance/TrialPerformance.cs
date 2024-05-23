namespace Performance
{
	public class TrialPerformance
	{
		/// <summary>
		/// This method calculates and compiles all recorded statistics throughout the trial.
		/// All calculated sub-statistics are ordered as: task1, task2, combined.
		/// </summary>
		/// <returns> It returns a statistics object containing the minimum, average, and maximum distance 
		/// between the student and the Ghost, as well as completion statistics and the observed learning effect.</returns>
		public Statistics CalculateTrialStatistics()
		{
			// Calculate distance values.
			StatTriplet<float> averageDistanceToGhost = new StatTriplet<float> (0f, 0f, 0f);
			StatTriplet<float> maxDistanceToGhost = new StatTriplet<float> (float.MaxValue, float.MaxValue, float.MaxValue);
			StatTriplet<float> minDistanceToGhost = new StatTriplet<float> (float.MinValue, float.MinValue, float.MinValue);

			// Obtain completion statistics.
			StatTriplet<float> timeToCompletion = new StatTriplet<float> (0f, 0f, 0f);
			StatTriplet<bool> completed = new StatTriplet<bool> (false, false, false);

			// Calculate the task performance and learning effect.
			StatTriplet<float> taskPerformance = new StatTriplet<float> (0f, 0f, 0f);
			StatTriplet<float> learningEffect = CalculateLearningEffect(new Statistics(
				new StatTriplet<float>(0f, 0f, 0f),
				new StatTriplet<float>(0f, 0f, 0f),
				new StatTriplet<float>(0f, 0f, 0f),
				new StatTriplet<float>(0f, 0f, 0f),
				new StatTriplet<bool>(false, false, false),
				new StatTriplet<float>(0f, 0f, 0f),
				new StatTriplet<float>(0f, 0f, 0f)));

			// Combine all sub-statistics into a statistics object.
			return new Statistics(minDistanceToGhost, averageDistanceToGhost, maxDistanceToGhost, timeToCompletion,
				completed, taskPerformance, learningEffect);
		}

		/// <summary>
		/// This method calculates the learning effect observed between the baseline and this trial based on student performance.
		/// </summary>
		/// <param name="baselinePerformance"> The baseline performance of the student. </param>
		/// <returns> A triplet containing the learning effect for task 1, for task 2, and for both tasks combined, in that order. </returns>
		private StatTriplet<float> CalculateLearningEffect(Statistics baselinePerformance)
		{
			return new StatTriplet<float>(0f, 0f, 0f);
		}
	}
}