namespace Performance
{
	public record Statistics(float AverageDistanceToGhost, float MaxDistanceToGhost, float MinDistanceToGhost, float TimeToCompletion, bool Completed, 
							 float TaskPerformance, float LearningEffect)
	{
		public float AverageDistanceToGhost { get; } = AverageDistanceToGhost;
		public float MaxDistanceToGhost { get; } = MaxDistanceToGhost;
		public float MinDistanceToGhost { get; } = MinDistanceToGhost;

		// Obtain completion statistics.
		public float TimeToCompletion { get; } = TimeToCompletion;
		public bool Completed { get; } = Completed;

		// Calculate the task performance and learning effect.
		public float TaskPerformance { get; } = TaskPerformance;
		public float LearningEffect { get; } = LearningEffect;
	}
}
