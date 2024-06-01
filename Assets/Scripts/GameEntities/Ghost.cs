using Session;
using Logger = Logging.Logger;
using UnityEngine;

namespace GameEntities
{
	public class Ghost : MonoBehaviour
	{
		[SerializeField] private GameObject ghostAvatar;

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
		/// This method can be used to set the transparency of the ghost avatar.
		/// The cube is fairly obviously visible up until 0.001.
		/// The cube is essentially invisible from 0.0001 onwards.
		/// <param name="alpha"> The value that the transparency should be set to. This should be between 0 and 1. </param>
		/// </summary>
		public void SetTransparency(float alpha)
		{
			if (ghostAvatar is null || alpha < 0.0f && alpha > 1.0f) return;
			
			// Adjust the transparency.
			var renderer1 = ghostAvatar.GetComponent<Renderer>();
			var colour = renderer1.material.color;
			colour.a = alpha;
			renderer1.material.color = colour;
		}

		/// <summary>
		/// This method logs the position of the object that this script is attached to.
		/// </summary>
		private void LogPosition()
		{
			// TODO: Log relevant stuff.
			var logString = "Ghost Position" + Logger.Delimiter + this.transform.position;
			SessionController.Logger.Log(logString);
		}
		
		/// <summary>
		/// This method is used to log the transparency of the ghost avatar.
		/// </summary>
		private void LogTransparency()
		{
			var logString = "Ghost Transparency" + Logger.Delimiter + ghostAvatar.GetComponent<Renderer>().material.color.a;
			SessionController.Logger.Log(logString);
		}
	}
}
