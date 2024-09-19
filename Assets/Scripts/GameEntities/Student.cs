using Sessions;
using UnityEngine;
using Logger = Logging.Logger;

namespace GameEntities
{
	public class Student : MonoBehaviour
	{
		[SerializeField] private HandAnimationController animationController;
		[SerializeField] private Transform handBone;
		[SerializeField] private Transform handTarget;
		[SerializeField] private Transform headTarget;

		[SerializeField] private Transform pointerBone;

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
			if (!SessionController.Started) return;
			// Log the position and transparency of the student avatar.
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
		/// This method returns the position of the pointer (end of the index finger).
		/// </summary>
		/// <returns> The position of the student avatar's pointer. </returns>
		public Vector3 GetPointerPosition() {
			return pointerBone.transform.position;
		}

		/// <summary>
		/// This method logs the position of the object that this script is attached to.
		/// </summary>
		private void LogPosition()
		{
			var logString = $"Student Hand Position{Logger.Delimiter}{handBone.transform.position}{Logger.Delimiter}" +
				$"Student Hand Rotation{Logger.Delimiter}{handBone.transform.rotation.eulerAngles}{Logger.Delimiter}" +
				$"Student Hand Pointer Position{Logger.Delimiter}{pointerBone.transform.position}{Logger.Delimiter}" +
				$"Student Hand Target Position{Logger.Delimiter}{handTarget.transform.position}{Logger.Delimiter}" +
				$"Student Hand Target Rotation{Logger.Delimiter}{handTarget.transform.rotation.eulerAngles}{Logger.Delimiter}" +
				$"Student Head Position{Logger.Delimiter}{headTarget.transform.position}{Logger.Delimiter}" +
				$"Student Head Rotation{Logger.Delimiter}{headTarget.transform.rotation.eulerAngles}";
			SessionController.Logger.Log(logString);
		}
	}
}
