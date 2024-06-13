using Sessions;
using Logger = Logging.Logger;
using UnityEngine;

namespace GameEntities
{
	public class Ghost : MonoBehaviour
	{
		[SerializeField] private GameObject ghostAvatar;
		[SerializeField] private GameObject handTarget;
		[SerializeField] private GameObject handBone;
		[SerializeField] private GameObject armBone;
		
		public Vector3 characterCameraOffset;
		[SerializeField] private Transform pointerBone;
		
		[SerializeField] private Material ghostMaterial;

		/// <summary>
		/// This method sets the character's offset to take into account head-bone-to-eye differences among others.
		/// </summary>
		public void Awake()
		{
			transform.position += characterCameraOffset;
		}

		// This method ensures that the ghost instructor's hand will not be rotated in a disturbing way.
		public void Update()
		{
			handTarget.transform.rotation = armBone.transform.rotation * Quaternion.Euler(0, 45.0f, 0);
		}
		
		/// <summary>
		/// This method is used to log the position and transparency of the ghost avatar.
		/// </summary>
		public void FixedUpdate()
		{
			if (ghostAvatar is null || !SessionController.Started) return;

			// Log the position and transparency of the ghost avatar.
			LogPosition();
			LogTransparency();
		}

		/// <summary>
		/// This method shows the ghost avatar.
		/// </summary>
		public void SetActive()
		{
			ghostAvatar.SetActive(true);
		}

		/// <summary>
		/// This method sets the ghost's hand position
		/// </summary>
		/// <param name="position"> The target position of the ghost. </param>
		public void SetPosition(Vector3 position)
		{
			// 1. Get the position from the Bézier curve
			// 2. Get the distance between the finger bone & the target
			// 3. Move the target by this offset
			var indexHandOffset = pointerBone.transform.position - handBone.transform.position;
			var target = position - indexHandOffset;
			handTarget.transform.position = target;
		}

		/// <summary>
		/// This method can be used to set the transparency of the ghost avatar.
		/// The cube is fairly obviously visible up until 0.001.
		/// The cube is essentially invisible from 0.0001 onwards.
		/// <param name="alpha"> The value that the transparency should be set to. This should be between 0 and 1. </param>
		/// </summary>
		public void SetTransparency(float alpha)
		{
			if (ghostAvatar is null || alpha < 0.0f && alpha > 1.0f) return;
			
			// Adjust the transparency.
			var colour = ghostMaterial.color;
			colour.a = alpha;
			ghostMaterial.color = colour;
		}

		/// <summary>
		/// This method returns the position of the pointer (end of the index finger).
		/// </summary>
		/// <returns> The position of the ghost avatar's pointer. </returns>
		public Vector3 GetPointerPosition() {
			return pointerBone.transform.position;
		}

		/// <summary>
		/// This method logs the position of the ghost's hand pointer that this script is attached to.
		/// </summary>
		private void LogPosition()
		{
			var logString = $"Ghost Hand Position{Logger.Delimiter}{handBone.transform.position}{Logger.Delimiter}" +
				$"Ghost Hand Pointer Position{Logger.Delimiter}{pointerBone.transform.position}{Logger.Delimiter}" +
				$"Ghost Hand Target Position{Logger.Delimiter}{handTarget.transform.position}";
			SessionController.Logger.Log(logString);
		}
		
		/// <summary>
		/// This method is used to log the transparency of the ghost avatar.
		/// </summary>
		private void LogTransparency()
		{
			var logString = "Ghost Transparency" + Logger.Delimiter + ghostMaterial.color.a;
			SessionController.Logger.Log(logString);
		}
	}
}
