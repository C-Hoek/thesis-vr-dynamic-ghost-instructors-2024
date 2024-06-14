using Task;
using TransparencySettings;

namespace Sessions
{
	public record Session(int NumLearningTrials, int NumTestTrials, float TimeToCompletePath, float TimeLimit, TaskHolder Task, TransparencyInfo TransparencyInfo, string SessionID)
	{
		public int NumLearningTrials { get; } = NumLearningTrials;
		public int NumTestTrials { get; } = NumTestTrials;

		public float TimeToCompletePath { get; } = TimeToCompletePath;
		public float TimeLimit { get; } = TimeLimit;
		public TaskHolder Task { get; } = Task;

		public TransparencyInfo TransparencyInfo { get; } = TransparencyInfo;

		public string SessionID { get; } = SessionID;

		/// <summary>
		/// This method tests if the current session has been completed.
		/// </summary>
		/// <param name="trialIndex"> The current trial that is to be loaded. </param>
		/// <returns> True if all trials have been loaded. </returns>
		public bool IsComplete(int trialIndex)
		{
			// The trial index is 0-based while the number is not, therefore we add 1 to the index.
			return trialIndex == NumLearningTrials + NumTestTrials;
		}
	}
}
