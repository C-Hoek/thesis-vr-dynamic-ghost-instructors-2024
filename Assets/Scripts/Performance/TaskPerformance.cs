using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Performance
{
	public class TaskPerformance
	{
		private List<float> _taskErrors;

		private List<GameObject> _pickedVertexOrder;


		// TODO: Implement the performance calculation.

		/// <summary>
		/// This method calculates how well the student performed while completing the task.
		/// </summary>
		/// <returns> A score between 0 and 100. </returns>
		public float CalculatePerformance()
		{
			return _taskErrors.Average();
		}

		/// <summary>
		/// This method adds a single time-point error to the list of task errors.
		/// </summary>
		/// <param name="error"> The single time-point error to add to the list of task errors. </param>
		public void AddTaskError(float error)
		{
			_taskErrors ??= new List<float>();
			_taskErrors.Add(error);
		}

		/// <summary>
		/// This method returns the list of task errors for logging and testing purposes.
		/// </summary>
		/// <returns> The list of single time-point task errors. </returns>
		public List<float> GetTaskErrors()
		{
			return _taskErrors;
		}
	}
}
