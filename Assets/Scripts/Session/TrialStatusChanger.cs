using TMPro;
using UnityEngine;

namespace Sessions
{
	public class TrialStatusChanger : MonoBehaviour
	{
		[SerializeField] private TrialStatusChangerObject leftCube;
		[SerializeField] private TrialStatusChangerObject rightCube;
		[SerializeField] private TrialStatusChangerObject endCube;
		[SerializeField] private TextMeshProUGUI initiationText;

		private const int HoldTime = 2;
		private const float EndHoldTime = 0.2f;
		private float _heldTimer;

		private bool _leftFilled;
		private bool _rightFilled;

		private bool _trialStarted;
		private bool _trialFinished;
		
		/// <summary>
		/// This method subscribes to the OnPathComplete event.
		/// </summary>
		private void OnEnable()
		{
			SessionEventHandler.Instance.OnPathComplete += ActivateEndCube;
		}
		
		/// <summary>
		/// This method unsubscribes from the OnPathComplete event.
		/// </summary>
		private void OnDisable()
		{
			SessionEventHandler.Instance.OnPathComplete -= ActivateEndCube;
		}

		/// <summary>
		/// Activate the end cube.
		/// </summary>
		private void ActivateEndCube()
		{
			endCube.gameObject.SetActive(true);
		}

		/// <summary>
		/// This method keeps track of how long the user has held their hands in the right point in space.
		/// Once this time exceeds the HoldTime, the trial is started.
		/// </summary>
		private void Update()
		{
			if (_trialFinished) return;
			
			// Once the user holds for longer or equal to the hold time, start the trial.
			if (_heldTimer >= HoldTime && !_trialStarted) RemoveCubes();
			else if (_heldTimer >= EndHoldTime && _trialStarted)
			{
				SessionEventHandler.Instance.CompleteTrial();
				_trialFinished = true;
			}
			

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
		public void SetFlag(bool isFilled, TrialStatusChangerObject cube, int collider)
		{
			if (cube == endCube)
			{
				_leftFilled = true;
				_rightFilled = true;
			}
			
			// If the trial has started, the start cubes have been destroyed. Return if this is the case.
			if (_trialStarted) return;
			if (cube == leftCube && collider == 0) _leftFilled = isFilled;
			else if (cube == rightCube && collider == 1) _rightFilled = isFilled;
		}

		/// <summary>
		/// This method destroys the trial initiator cubes and should be called when the initiation conditions are met.
		/// </summary>
		private void RemoveCubes()
		{
			initiationText.text = "";

			SessionEventHandler.Instance.StartNextTrial();

			Destroy(leftCube.gameObject);
			Destroy(rightCube.gameObject);
			_heldTimer = 0;
			_trialStarted = true;
			_leftFilled = false;
			_rightFilled = false;
		}
	}

}
