using UnityEngine;

namespace Sessions
{
	public class TrialStatusChangerObject : MonoBehaviour
	{
		[SerializeField] private TrialStatusChanger trialStatusChanger;

		/// <summary>
		/// This method is triggered when a collider enters the trial initiator object.
		/// </summary>
		/// <param name="other"> The collider that entered the trial initiator object. </param>
		private void OnTriggerEnter(Collider other)
		{
			// Check which object collided with the trial initiator object.
			var colliderIndex = 0;
			if (other.gameObject.CompareTag("Right"))
			{
				colliderIndex = 1;
			}
			else if (!other.gameObject.CompareTag("Left"))
			{
				colliderIndex = 2;
			}

			trialStatusChanger.SetFlag(true, this, colliderIndex);
		}

		/// <summary>
		/// This method is triggered when a collider exits the trial initiator object.
		/// </summary>
		/// <param name="other"> The collider that exits the trial initiator object. </param>
		private void OnTriggerExit(Collider other)
		{
			// Check which object collided with the trial initiator object.
			var colliderIndex = 0;
			if (other.gameObject.CompareTag("Right"))
			{
				colliderIndex = 1;
			}
			else if (!other.gameObject.CompareTag("Left"))
			{
				colliderIndex = 2;
			}

			trialStatusChanger.SetFlag(false, this, colliderIndex);
		}
	}
}
