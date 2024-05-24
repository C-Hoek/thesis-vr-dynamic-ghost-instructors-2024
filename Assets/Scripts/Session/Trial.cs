using Performance;
using Task;

namespace Session
{
	public record Trial(TaskPerformance TaskPerformance, ITask Task1, ITask Task2, float TimeLimit, bool IsTraining)
	{
		public TaskPerformance TaskPerformance { get; } = TaskPerformance;
		public ITask Task1 { get; } = Task1;
		public ITask Task2 { get; } = Task2;
		
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
