using UnityEngine;

namespace GameEntities
{
	public class IKTargetToCharacterMapper : MonoBehaviour
	{
		public Vector3 characterCameraOffset;
		
		public Transform cameraHeadTarget;

		private const float RotationSpeed = 10f;
		
		public Transform leftControllerTarget;
		public Transform ikLeftArmTarget;
		
		public Transform rightControllerTarget;
		public Transform ikRightArmTarget;

		/// <summary>
		/// This method sets the character's offset to take into account head-bone-to-eye differences among others.
		/// </summary>
		public void Awake()
		{
			transform.position += characterCameraOffset;
		}
		
		/// <summary>
		/// This method aligns the IK targets with the controllers.
		/// </summary>
		public void LateUpdate()
		{
			var yRotation = cameraHeadTarget.eulerAngles.y;
			// Don't rotate beyond 90 degrees as the camera will show disturbing imagery of necks bending in a way they are not supposed to be bending.
			// Also don't move the arms beyond this for a similar reason.
			if (yRotation is > 90 and < 270f)
			{
				return;
			}
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, yRotation, transform.eulerAngles.z), Time.deltaTime * RotationSpeed);
			
			// Align the arms with their controllers.
			SetArmTransform();
		}

		/// <summary>
		/// This method aligns the arms/hands with the controllers.
		/// </summary>
		private void SetArmTransform()
		{
			// Align the left arm with the left controller.
			ikLeftArmTarget.position = leftControllerTarget.position;
			ikLeftArmTarget.rotation = leftControllerTarget.rotation;

			// Align the right arm with the right controller.
			ikRightArmTarget.position = rightControllerTarget.position;
			ikRightArmTarget.rotation = rightControllerTarget.rotation;
		}
	}
}
