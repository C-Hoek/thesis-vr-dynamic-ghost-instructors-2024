namespace Task
{
    public interface ITask
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns> True if the task has been completed. False otherwise. </returns>
        public bool IsComplete();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TaskDetails TaskDetails();



    }
}