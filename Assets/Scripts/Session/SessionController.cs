using Config;
using Task;
using TransparencySettings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Session
{
	public class SessionController : MonoBehaviour
	{
		private static SessionController s_instance;
		
		private ScriptableConfigObject _config;
		public ScriptableConfigObject Config
		{
			set
			{
				_config = value;
				Setup();
			}
		}

		private GameObject _ghost;

		private Session _session;
		private ITransparencySetting _transparencySetting;

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
			_session = new Session(
				_config.numLearningTrials,
				_config.numTestTrials,
				_config.timeLimit,
				ITask.SelectTask(_config.taskName));

			// Set up the transparency settings through the config object.
			_transparencySetting = ITransparencySetting.SelectTransparencySetting(_config.transparencyType);
		}

		/// <summary>
		/// 
		/// </summary>
		public void LoadTrial()
		{
			if (_session.IsComplete(_trialIndex))
			{
				SceneManager.LoadScene("Menu");
			}

		}

		public void IsComplete()
		{

		}
	}
}
