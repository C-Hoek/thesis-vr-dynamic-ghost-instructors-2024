using Config;
using Task;
using TransparencySettings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Session
{
	public class SessionController : MonoBehaviour
	{
		// The static instance used to ensure there is only one active SessionController at any point in time.
		private static SessionController s_instance;

		// The non-extracted session configuration.
		private ScriptableConfigObject _config;
		public ScriptableConfigObject Config
		{
			set
			{
				_config = value;
				Setup();
			}
		}

		// The session details.
		private static Session s_session;
		public static Session Session
		{
			get => s_session;
		}
		private ITransparencySetting _transparencySetting;

		// Session-related Game Objects.
		private GameObject _ghost;

		// Variables used to keep track of the session state.
		private int _trialIndex = 0;

		/// <summary>
		/// This method makes sure that the session controller can be passed between scenes to allow for information passing.
		/// It also ensures only one SessionController is active at a time.
		/// </summary>
		public void Awake()
		{
			if (s_instance is not null)
			{
				Destroy(s_instance);
			}

			s_instance = this;
			DontDestroyOnLoad(this);
		}

		/// <summary>
		/// This method sets up the full session based on the config object linked through the Unity Editor.
		/// </summary>
		private void Setup()
		{
			// Set up the session through the config object.
			s_session = new Session(
				_config.numLearningTrials,
				_config.numTestTrials,
				_config.timeLimit,
				ITask.SelectTask(_config.taskName),
				new TransparencyInfo(_config.minTransparency, _config.baseTransparency, _config.maxTransparency, _config.errorThreshold));

			// Set up the transparency settings through the config object.
			_transparencySetting = ITransparencySetting.SelectTransparencySetting(_config.transparencyType);
		}

		/// <summary>
		/// 
		/// </summary>
		public void LoadTrial()
		{
			if (s_session.IsComplete(_trialIndex))
			{
				SceneManager.LoadScene("Menu");
			}

		}

		public void IsComplete()
		{

		}
	}
}
