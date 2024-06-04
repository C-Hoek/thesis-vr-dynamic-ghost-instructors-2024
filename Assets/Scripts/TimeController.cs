using UnityEngine;
using Logger = Logging.Logger;

namespace Session
{
	public class TimeController : MonoBehaviour
	{
		private static float s_currentTime;
		public static float CurrentTime
		{
			get => s_currentTime;
		}

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
		}

		/// <summary>
		/// 
		/// </summary>
		private void StartTimer()
		{
			s_currentTime = 0f;
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
