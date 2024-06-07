using Performance;
using Task;

namespace Sessions
{
	public record Trial(TrialPerformance TrialPerformance, GestureTask Task, float TimeLimit, bool IsTraining)
	{
		public TrialPerformance TrialPerformance { get; } = TrialPerformance;
		public GestureTask Task { get; } = Task;

		public float TimeLimit { get; } = TimeLimit;
		public bool IsTraining { get; } = IsTraining;
		
		/// <summary>
		/// This method tests if the current trial has been completed.
		/// </summary>
		/// <param name="currentTime"> The amount of time that has passed since the start of the trial. </param>
		/// <returns> True if the time limit has been reached. </returns>
		public bool IsComplete(float currentTime)
		{
			return Equals(TimeLimit, currentTime);
		}
	}
}
