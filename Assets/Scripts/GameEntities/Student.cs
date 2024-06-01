using Session;
using UnityEngine;
using Logger = Logging.Logger;

namespace GameEntities
{
	public class Student : MonoBehaviour
	{
		[SerializeField] private GameObject studentAvatar;
		
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
