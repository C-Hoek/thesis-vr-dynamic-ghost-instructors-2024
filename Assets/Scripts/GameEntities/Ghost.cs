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
		
		public Vector3 characterCameraOffset;
		private readonly Vector3 _handToPointerOffset = new Vector3(0.0155999996f, 0.172999993f, 0.0188999996f);
		
		[SerializeField] private Material ghostMaterial;

		/// <summary>
		/// This method sets the character's offset to take into account head-bone-to-eye differences among others.
		/// </summary>
		public void Awake()
		{
			transform.position += characterCameraOffset;
		}
		
		/// <summary>
		/// This method is used to log the position and transparency of the ghost avatar.
		/// </summary>
		public void FixedUpdate()
		{
			if (ghostAvatar is null) return;

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
			handTarget.transform.position = position - _handToPointerOffset;
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
		/// This method returns the transform of the ghost avatar.
		/// </summary>
		/// <returns> The transform of the ghost avatar. </returns>
		public Transform GetTransform() {
			return handBone.transform;
		}

		/// <summary>
		/// This method logs the position of the ghost's hand pointer that this script is attached to.
		/// </summary>
		private void LogPosition()
		{
			var logString = $"Ghost Hand Position{Logger.Delimiter}{handBone.transform.position}{Logger.Delimiter}" +
				$"Ghost Hand Pointer Position{Logger.Delimiter}{handBone.transform.position + _handToPointerOffset}{Logger.Delimiter}" +
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
