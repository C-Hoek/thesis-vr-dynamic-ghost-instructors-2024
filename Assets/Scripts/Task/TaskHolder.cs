using GameEntities;
using Routing;
using UnityEngine;

namespace Task
{
	public class TaskHolder
	{
		private GameObject _taskObject;
		private BezierPath _path;
		public BezierPath Path
		{
			set => _path = value;
		}
		
		private bool _setupDone;

		private Vector3 _taskPosition = new Vector3(0f, 0.161000013f + 0.602f, -93.4499969f + 90);
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
	}
}
