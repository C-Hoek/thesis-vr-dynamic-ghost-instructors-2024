namespace Task
{
    public class CygnusTask : ITask
    {
        // TODO: Implement the task.

        /// <summary>
        /// This method shows the vertices of the task to the student.
        /// Edges are not shown to the student.
        /// </summary>
        public void VisualiseTask()
        {

        }

        /// <summary>
        /// This method shows the vertices, edges, and helpful instructions to the Ghost.
        /// </summary>
        public void VisualiseTaskGhost()
        {

        }

        /// <summary>
        /// This method returns a boolean that represents if the pressed vertex is the correct next vertex.
        /// </summary>
        /// <param name="v"> The pressed vertex. </param>
        /// <param name="previous"> The vertex that the user pressed previously. </param>
        /// <returns></returns>
        public bool IsVertexCorrect(GameObject v, GameObject previous)
        {
            return false;
        }

        // TODO: Describe how correct vertices and incorrect vertices are marked differently.
        /// <summary>
        /// This method marks a vertex as pressed.
        /// Correct vertices should be marked visually differently from incorrect vertices.
        /// </summary>
        /// <param name="v"> The pressed vertex. </param>
        public void MarkVertexPressed(GameObject v)
        {

        }

        /// <summary>
        /// This method marks a vertex as unpressed.
        /// This should be called to remove incorrectly pressed vertices after visual feedback has shown the student that it is wrong.
        /// </summary>
        /// <param name="v"> The vertex that should be marked as unpressed. </param>
        public void MarkVertexUnpressed(GameObject v)
        {

        }

        /// <summary>
        /// This method checks whether the task has been fully and correctly completed.
        /// </summary>
        /// <returns> True if the task has been completed. False otherwise. </returns>
        public bool IsComplete()
        {
            return false;
        }
    }
}