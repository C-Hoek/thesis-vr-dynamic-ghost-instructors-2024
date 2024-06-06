using UnityEngine;

namespace Sessions
{
	public class TrialInitiatorObject : MonoBehaviour
	{
		[SerializeField] private TrialInitiator trialInitiator;

		/// <summary>
		/// This method is triggered when a collider enters the trial initiator object.
		/// </summary>
		/// <param name="other"> The collider that entered the trial initiator object. </param>
		private void OnTriggerEnter(Collider other)
		{
			// Check which object collided with the trial initiator object.
			// TODO: Set the hand's tags to Right and Left.
			var collider = 0;
			if (other.gameObject.tag == "Right")
			{
				collider = 1;
			}
			else if (other.gameObject.tag != "Left")
			{
				collider = 2;
			}

			trialInitiator.SetFlag(true, this, collider);
		}

		/// <summary>
		/// This method is triggered when a collider exits the trial initiator object.
		/// </summary>
		/// <param name="other"> The collider that exits the trial initiator object. </param>
		private void OnTriggerExit(Collider other)
		{
			// Check which object collided with the trial initiator object.
			var collider = 0;
			if (other.gameObject.tag == "Right")
			{
				collider = 1;
			}
			else if (other.gameObject.tag != "Left")
			{
				collider = 2;
			}

			trialInitiator.SetFlag(false, this, collider);
		}

	}
}
