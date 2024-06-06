using UnityEngine;
using Sessions;

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
			s_currentTime += Time.deltaTime * 1000f;

			if (s_currentTime > _timeLimit) SessionEventHandler.Instance.CompleteTrial(timeExpired: true);
		}

		/// <summary>
		/// This method starts the timer once the trial starts, and also sets the time limit.
		/// </summary>
		private void StartTimer()
		{
			s_currentTime = 0f;
			_timeLimit = SessionController.Session.TimeLimit;
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
