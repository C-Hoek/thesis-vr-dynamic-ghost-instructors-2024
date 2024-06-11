using Sessions;
using UnityEngine;
using Logger = Logging.Logger;

namespace GameEntities
{
	public class Student : MonoBehaviour
	{
		[SerializeField] private GameObject studentAvatar;
		[SerializeField] private HandAnimationController animationController;

		private float? _baselinePerformance;
		public float? BaselinePerformance {
			get => _baselinePerformance;
			set => _baselinePerformance ??= value;
		}
		
		/// <summary>
		/// This method is used to log the position of the student avatar.
		/// </summary>
		public void FixedUpdate()
		{
			if (studentAvatar is null) return;

			// Log the position and transparency of the ghost avatar.
			LogPosition();
		}

		/// <summary>
		/// This method starts the pointing animation of the student.
		/// </summary>
		public void ActivatePointingAnimation()
		{
			animationController.ActivatePointAnimation();
		}

		/// <summary>
		/// This method returns the transform of the student avatar.
		/// </summary>
		/// <returns> The transform of the student avatar. </returns>
		public Transform GetTransform() {
			return studentAvatar.transform;
		}

		/// <summary>
		/// This method logs the position of the object that this script is attached to.
		/// </summary>
		private void LogPosition()
		{
			// TODO: Log relevant stuff.
			var logString = "Student Position" + Logger.Delimiter + this.transform.position;
			SessionController.Logger.Log(logString);
		}
	}
}
