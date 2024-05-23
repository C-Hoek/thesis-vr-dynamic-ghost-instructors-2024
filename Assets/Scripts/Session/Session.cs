using Task;

namespace Session
{
    public record Session (int numLearningTrials, int numTestTrials, float timeLimit, ITask task)
    {
        public int numLearningTrials { get; } = numLearningTrials;
        public int numTestTrials { get; } = numTestTrials;
        
        public float timeLimit { get; } = timeLimit;
        public ITask task { get; } = task;
        
        /// <summary>
        /// This method tests if the current session has been completed.
        /// </summary>
        /// <param name="trialIndex"> The current trial that is to be loaded. </param>
        /// <returns> True if all trials have been loaded. </returns>
        public bool IsComplete(int trialIndex)
        {
            return trialIndex == numLearningTrials + numTestTrials;
        }
    }
}