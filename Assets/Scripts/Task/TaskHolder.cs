using GameEntities;
using Routing;
using UnityEngine;

namespace Task
{
	public class TaskHolder
	{
		private GameObject _taskObject;
		private BezierPath _path;
		
		private bool _setupDone;

		private Vector3 _taskPosition = new Vector3(0, 0.160999998f + 1, -95.2139969f + 90);
		public Vector3 TaskPosition
		{
			get => _taskPosition;
		}
		
		/// <summary>
		/// This method sets up the task by retrieving its path.
		/// </summary>
		/// <returns> The test object containing the path information. </returns>
		public GameObject Setup()
		{
			_taskObject = Resources.Load<GameObject>("TaskPrefabs/TestObject");
			_path = _taskObject.GetComponent<BezierPath>();
			_setupDone = true;
			return _taskObject;
		}

		/// <summary>
		/// This method sets the hand position of the ghost.
		/// </summary>
		/// <param name="ghost"> The ghost avatar. </param>
		/// <param name="t"> The current point in time. </param>
		public void SetGhostPosition(Ghost ghost, float t)
		{
			if (!_setupDone) return;
			
			ghost.SetPosition(_path.PositionAt(t));
		}
		
		/// <summary>
		/// Implementations of this method should show the task curves.
		/// </summary>
		public void VisualiseTask()
		{
			
		}

		/// <summary>
		/// Implementations of this method should check whether the task has been fully and correctly completed.
		/// </summary>
		/// <returns> True if the task has been completed. False otherwise. </returns>
		public bool IsComplete()
		{
			return false;
		}
	}
}
