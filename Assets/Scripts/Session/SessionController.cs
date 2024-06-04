using Config;
using GameEntities;
using Task;
using TransparencySettings;
using UnityEngine;
using UnityEngine.SceneManagement;
using Logger = Logging.Logger;

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
		private static Logger s_logger;
		public static Logger Logger
		{
			get
			{
				s_logger ??= FindObjectOfType<Logger>();
				return s_logger;
			}
		}
		private Ghost _ghost;
		private Student _student;

		// Variables used to keep track of the session state.
		[SerializeField] private int _trialIndex = 0;
		private bool _started;
		private bool _infoLogged;

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
		/// This method handles all session controller activities throughout the game.
		/// </summary>
		public void Update()
		{
			if (!_started) return;
			if (!_infoLogged)
			{
				Logger?.LogTrialInfo(_trialIndex, _config);
				_infoLogged = Logger is not null;
			}

			_ghost ??= FindObjectOfType<Ghost>();
			_student ??= FindObjectOfType<Student>();
			
			// TODO: Obtain the error between the ghost and player positions.
			// TODO: Obtain the target transparency.
			// TODO: Replace the _config.baseTransparency below by the obtained target transparency.
			_ghost?.SetTransparency(_config.baseTransparency);
		}

		/// <summary>
		/// This method sets up the full session based on the config object linked through the Unity Editor.
		/// </summary>
		public void Setup()
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
		/// This method loads the next trial if there is any, and returns to the main menu otherwise
		/// </summary>
		public void LoadTrial()
		{
			_started = false;
			
			// Load the menu if the session is complete.
			if (s_session.IsComplete(_trialIndex))
			{
				SceneManager.LoadScene("Menu");
			}
			
			// Load the next trial of the session.
			SceneManager.LoadScene("TestEnvironment");
			_trialIndex += 1;
			_started = true;
		}
	}
}
