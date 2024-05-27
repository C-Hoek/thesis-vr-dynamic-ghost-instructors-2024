using Session;
using System;
using Unity;
using UnityEngine;

namespace GameEntities
{
	public class Ghost : MonoBehaviour
	{
		[SerializeField] private GameObject _ghostAvatar;

		/// <summary>
		/// TODO: This method is not particularly doing anything right now.
		/// </summary>
		public void Update()
		{
			if (_ghostAvatar is null) return;

			LogPosition();
        }

		/// <summary>
		/// This method can be used to set the transparency of the ghost avatar.
		/// <param name="alpha"> The value that the transparency should be set to. This should be between 0 and 1. </param>
		/// </summary>
		public void SetTransparency(float alpha)
		{
			if (_ghostAvatar is null || alpha < 0.0f && alpha > 1.0f) return;
			var renderer = _ghostAvatar.GetComponent<Renderer>();
			var colour = renderer.material.color;
			colour.a = 0.5f;
		}

		private void LogPosition()
		{
			var logger = SessionController.Logger;
			var logString = "Position: " + logger.Delimiter + this.transform.position;

			logString += logger.Delimiter;
			SessionController.Logger.Log(logString);
		}
	}
}
