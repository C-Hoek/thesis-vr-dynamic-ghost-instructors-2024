using Task;

namespace Session
{
    public class Session
    {
        private int numLearningTrials = 0;
        private int numTestTrials = 0;
        private int numRetentionTrials = 0;

        private float timeLimit = 30;

        private ITask task;

        
        /// <summary>
        /// T
        /// </summary>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        public bool IsComplete(float currentTime)
        {
            return false;
        }
    }
}