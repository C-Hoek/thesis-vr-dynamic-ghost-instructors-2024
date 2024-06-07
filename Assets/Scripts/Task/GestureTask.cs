using GameEntities;
using Routing;
using UnityEngine;

namespace Task
{
	// TODO: Revert this to a non-monobehaviour.
	public class GestureTask : MonoBehaviour
	{
		private GameObject _taskObject;
		[SerializeField] private BezierPath _path;
		[SerializeField] private Ghost _ghost;
		
		// TODO: Remove this component.
		private float _time = 0f;
		private bool _setupDone;
		
		/// <summary>
		/// This method sets up the task by retrieving its path.
		/// </summary>
		public void Setup(Ghost ghost)
		{
			//TODO: actually add a proper reference here : )
			_taskObject = Resources.Load<GameObject>("");
			_path = _taskObject.GetComponent<BezierPath>();
			_ghost = ghost;
			_setupDone = true;
		}
		
		public void Update()
		{
			_time += Time.deltaTime;
			SetGhostPosition(_time);
		}

		/// <summary>
		/// This method sets the hand position of the ghost.
		/// </summary>
		/// <param name="t"> The current point in time. </param>
		public void SetGhostPosition(float t)
		{
			_ghost.SetPosition(_path.PositionAt(t));
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
