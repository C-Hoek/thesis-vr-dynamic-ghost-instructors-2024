namespace Performance
{
	public record Statistics(float AverageError, float MaxError, float MinError, float TimeToCompletion, bool Completed, 
							 float TaskPerformance, float LearningEffect)
	{
		public float AverageError { get; } = AverageError;
		public float MaxError { get; } = MaxError;
		public float MinError { get; } = MinError;

		// Obtain completion statistics.
		public float TimeToCompletion { get; } = TimeToCompletion;
		public bool Completed { get; } = Completed;

		// Calculate the task performance and learning effect.
		public float TaskPerformance { get; } = TaskPerformance;
		public float LearningEffect { get; } = LearningEffect;
	}
}
