using Task;

namespace Session
{
	public record Session(int NumLearningTrials, int NumTestTrials, float TimeLimit, ITask Task)
	{
		public int NumLearningTrials { get; } = NumLearningTrials;
		public int NumTestTrials { get; } = NumTestTrials;

		public float TimeLimit { get; } = TimeLimit;
		public ITask Task { get; } = Task;

		/// <summary>
		/// This method tests if the current session has been completed.
		/// </summary>
		/// <param name="trialIndex"> The current trial that is to be loaded. </param>
		/// <returns> True if all trials have been loaded. </returns>
		public bool IsComplete(int trialIndex)
		{
			return trialIndex == NumLearningTrials + NumTestTrials;
		}
	}
}
