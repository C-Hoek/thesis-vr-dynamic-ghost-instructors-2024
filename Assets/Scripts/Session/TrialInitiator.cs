using TMPro;
using UnityEngine;

namespace Sessions
{
	public class TrialInitiator : MonoBehaviour
	{
		[SerializeField] private TrialInitiatorObject leftCube;
		[SerializeField] private TrialInitiatorObject rightCube;
		[SerializeField] private TextMeshPro initiationText;

		private const int HoldTime = 2;
		private float _heldTimer;

		private bool _leftFilled;
		private bool _rightFilled;


		/// <summary>
		/// This method keeps track of how long the user has held their hands in the right point in space.
		/// Once this time exceeds the HoldTime, the trial is started.
		/// </summary>
		private void Update()
		{
			// Once the user holds for longer or equal to the hold time, start the trial.
			if (_heldTimer >= HoldTime) RemoveCubes();

			// Increment the timer.
			if (_leftFilled && _rightFilled)
			{
				_heldTimer += Time.deltaTime;
			}
			else
			{
				_heldTimer = 0f;
			}
		}

		/// <summary>
		/// This method sets the appropriate flag to the passed boolean.
		/// It is used to keep track of which hand is filling which cube.
		/// The flags are only set if the left hand is filling the left cube or the right hand is filling the right cube.
		/// </summary>
		/// <param name="isFilled"> A boolean that determines if a collider is within the trial initiator object. </param>
		/// <param name="cube"> The trial initiator object in question. </param>
		/// <param name="collider"> An integer that determines what collided with the trial initiator object. This is 0 if it was the left hand, 
		/// 1 if it was the right hand, and 2 if it was any other object. </param>
		public void SetFlag(bool isFilled, TrialInitiatorObject cube, int collider)
		{
			if (cube == leftCube && collider == 0) _leftFilled = isFilled;
			else if (cube == rightCube && collider == 1) _rightFilled = isFilled;
		}

		/// <summary>
		/// This method destroys the trial initiator cubes and should be called when the initiation conditions are met.
		/// </summary>
		private void RemoveCubes()
		{
			initiationText.text = "";
			Destroy(leftCube.gameObject);
			Destroy(rightCube.gameObject);

			// Start the next trial.
			SessionEventHandler.Instance.StartNextTrial();
		}
	}

}
