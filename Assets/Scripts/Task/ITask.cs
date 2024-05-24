using System.Collections.Generic;
using UnityEngine;

namespace Task
{
	public interface ITask
	{
		/// <summary>
		/// Implementations of this method should show the vertices of the task to the student.
		/// Edges should not be shown to the student.
		/// </summary>
		public void VisualiseTask();

		/// <summary>
		/// Implementations of this method should show the vertices, edges, and helpful instructions to the Ghost.
		/// </summary>
		public void VisualiseTaskGhost();

		/// <summary>
		/// Implementations of this method should return the correct order in which vertices should be pressed.
		/// </summary>
		public List<GameObject> GetCorrectVertexOrder();

		/// <summary>
		/// Implementations of this method should return a boolean that represents if the pressed vertex is the correct next vertex.
		/// </summary>
		/// <param name="v"> The pressed vertex. </param>
		/// <param name="previous"> The vertex that the user pressed previously. </param>
		/// <returns></returns>
		public bool IsVertexCorrect(GameObject v, GameObject previous);

		/// <summary>
		/// Implementations of this method should mark a vertex as pressed.
		/// Correct vertices should be marked visually differently from incorrect vertices.
		/// </summary>
		/// <param name="v"> The pressed vertex. </param>
		public void MarkVertexPressed(GameObject v);

		/// <summary>
		/// Implementations of this method should mark a vertex as unpressed.
		/// This should be called to remove incorrectly pressed vertices after visual feedback has shown the student that it is wrong.
		/// </summary>
		/// <param name="v"> The vertex that should be marked as unpressed. </param>
		public void MarkVertexUnpressed(GameObject v);

		/// <summary>
		/// Implementations of this method should check whether the task has been fully and correctly completed.
		/// </summary>
		/// <returns> True if the task has been completed. False otherwise. </returns>
		public bool IsComplete();

		/// <summary>
		/// This method returns an ITask class based on a string representation of the task type.
		/// </summary>
		/// <param name="taskType"> The string representation of a task type. This should be "cygnus" or "lyra". </param>
		/// <returns> An ITask class with the </returns>
		public static ITask SelectTask(string taskType)
		{
			return taskType switch
			{
				"cygnus" => new CygnusTask(),
				"lyra" => new LyraTask(),
				_ => new LyraTask()
			};
		}
	}
}
