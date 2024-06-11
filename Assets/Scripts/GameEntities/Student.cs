using Sessions;
using UnityEngine;
using Logger = Logging.Logger;

namespace GameEntities
{
	public class Student : MonoBehaviour
	{
		[SerializeField] private HandAnimationController animationController;
		[SerializeField] private Transform handBone;
		[SerializeField] private Transform headTarget;

		private Vector3 _handToPointerOffset = new Vector3(0.0155999996f, 0.172999993f, 0.0188999996f);

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
		/// This method returns the transform of the student avatar's pointer hand.
		/// </summary>
		/// <returns> The transform of the student avatar's pointer hand. </returns>
		public Transform GetTransform() {
			return handBone.transform;
		}

		/// <summary>
		/// This method logs the position of the object that this script is attached to.
		/// </summary>
		private void LogPosition()
		{
			var logString = $"Student Hand Position{Logger.Delimiter}{handBone.transform.position}{Logger.Delimiter}" +
				$"Student Hand Pointer Position{Logger.Delimiter}{handBone.transform.position + _handToPointerOffset}" +
				$"Student Head Position{Logger.Delimiter}{headTarget.transform.position}{Logger.Delimiter}Student Head Rotation{Logger.Delimiter}{headTarget.transform.rotation}";
			SessionController.Logger.Log(logString);
		}
	}
}
