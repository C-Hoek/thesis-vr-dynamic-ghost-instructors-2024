using UnityEngine;

namespace Sessions
{
	public class TimeController : MonoBehaviour
	{
		private static float s_currentTime;
		public static float CurrentTime
		{
			get => s_currentTime;
		}
		private float? _timeLimit;

		private static bool s_enabled;
		public static bool Enabled
		{
			set => s_enabled = value;
		}

		private bool _trialFinished;

		/// <summary>
		/// This method subscribes to the OnStartNextTrial event.
		/// </summary>
		public void OnEnable()
		{
			SessionEventHandler.Instance.OnStartNextTrial += StartTimer;
		}

		/// <summary>
		/// This method sets the current time to 0 at the start of a trial.
		/// </summary>
		public void Start()
		{
			s_currentTime = 0f;
		}

		/// <summary>
		/// This method is used to keep track of the current trial time in milliseconds.
		/// </summary>
		public void Update()
		{
			if (!s_enabled || _trialFinished) return;
			
			s_currentTime += Time.deltaTime * 1000f;

			if (s_currentTime > _timeLimit)
			{
				SessionEventHandler.Instance.CompleteTrial(timeExpired: true);
				_trialFinished = true;
			}
			
		}

		/// <summary>
		/// This method starts the timer once the trial starts, and also sets the time limit.
		/// </summary>
		private void StartTimer()
		{
			s_currentTime = 0f;
			// Adjust the time limit to take into account the millisecond counter of the time controller.
			_timeLimit = SessionController.Session.TimeLimit * 1000f;
		}

		/// <summary>
		/// This method unsubscribes from the OnStartNextTrial event.
		/// </summary>
		public void OnDisable()
		{
			SessionEventHandler.Instance.OnStartNextTrial -= StartTimer;
		}
	}
}
