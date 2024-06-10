using Task;
using TransparencySettings;

namespace Sessions
{
	public record Session(int NumLearningTrials, int NumTestTrials, float TimeLimit, TaskHolder Task, TransparencyInfo TransparencyInfo)
	{
		public int NumLearningTrials { get; } = NumLearningTrials;
		public int NumTestTrials { get; } = NumTestTrials;

		public float TimeLimit { get; } = TimeLimit;
		public TaskHolder Task { get; } = Task;

		public TransparencyInfo TransparencyInfo { get; } = TransparencyInfo;

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
