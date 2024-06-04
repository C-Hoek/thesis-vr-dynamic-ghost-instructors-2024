using System;

namespace Session
{
	public class SessionEventHandler
	{
		private static SessionEventHandler s_instance = null;
		public static SessionEventHandler Instance => s_instance ??= new SessionEventHandler();

		/// <summary>
		/// This event is called when a trial starts.
		/// </summary>
		public event Action OnStartNextTrial;

		/// <summary>
		/// This method invokes the OnStartNextTrial event.
		/// </summary>
		public void StartNextTrial()
		{
			OnStartNextTrial?.Invoke();
		}
	}
}
