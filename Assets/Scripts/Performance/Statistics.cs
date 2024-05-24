namespace Performance
{
	public record Statistics(StatTriplet<float> AverageDistanceToGhost, StatTriplet<float> MaxDistanceToGhost, StatTriplet<float> MinDistanceToGhost,
		StatTriplet<float> TimeToCompletion, StatTriplet<bool> Completed, StatTriplet<float> TaskPerformance, StatTriplet<float> LearningEffect)
	{
		public StatTriplet<float> AverageDistanceToGhost { get; } = AverageDistanceToGhost;
		public StatTriplet<float> MaxDistanceToGhost { get; } = MaxDistanceToGhost;
		public StatTriplet<float> MinDistanceToGhost { get; } = MinDistanceToGhost;

		// Obtain completion statistics.
		public StatTriplet<float> TimeToCompletion { get; } = TimeToCompletion;
		public StatTriplet<bool> Completed { get; } = Completed;

		// Calculate the task performance and learning effect.
		public StatTriplet<float> TaskPerformance { get; } = TaskPerformance;
		public StatTriplet<float> LearningEffect { get; } = LearningEffect;
	}
}
