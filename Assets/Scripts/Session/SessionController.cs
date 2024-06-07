using System.Collections.Generic;
using Config;
using GameEntities;
using Performance;
using Task;
using TransparencySettings;
using UnityEngine;
using UnityEngine.SceneManagement;
using Logger = Logging.Logger;

namespace Sessions
{
	public class SessionController : MonoBehaviour
	{
		// The static instance used to ensure there is only one active SessionController at any point in time.
		private static SessionController s_instance;
		public static SessionController Instance {
			get => s_instance;
		}

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
		private int _trialIndex = 0;
		private bool _started;
		private bool _infoLogged;

		private List<TrialPerformance> _trialPerformances;

		/// <summary>
		/// This method subscribes to the OnStartNextTrial event.
		/// </summary>
		public void OnEnable()
		{
			SessionEventHandler.Instance.OnStartNextTrial += StartTrial;
			SessionEventHandler.Instance.OnCompleteTrial += CompleteTrial;
		}

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
		public void FixedUpdate()
		{
			if (!_started) return;
			if (!_infoLogged)
			{
				Logger?.LogTrialInfo(_trialIndex, _config);
				_infoLogged = Logger is not null;
			}

			_ghost ??= FindObjectOfType<Ghost>();
			_student ??= FindObjectOfType<Student>();
			
			if (_ghost is null || _student is null) return;

			// Obtain the combined position and rotation error and set the ghost's transparency to the appropriate amount.
			var time = TimeController.CurrentTime;
			var error = ErrorCalculation.CalculateError(_student.GetTransform(), _ghost.GetTransform());
			Logger?.Log(ErrorCalculation.LogError(_student.transform, _ghost.transform, error));
			_ghost.SetTransparency(_transparencySetting.TargetGhostTransparency(error, _trialIndex));

			// Add the error to the trial performance.
			_trialPerformances ??= new List<TrialPerformance>();
			_trialPerformances[_trialIndex].AddTaskError(error, time);
		}

		/// <summary>
		/// This method unsubscribes from the OnStartNextTrial event.
		/// </summary>
		public void OnDisable()
		{
			SessionEventHandler.Instance.OnStartNextTrial -= StartTrial;
			SessionEventHandler.Instance.OnCompleteTrial -= CompleteTrial;
		}

		/// <summary>
		/// This method sets up the full session based on the config object linked through the Unity Editor.
		/// </summary>
		public void Setup()
		{
			// Set up the session through the config object.
			// TODO: set the object from the session controller and not the config!
			s_session = new Session(
				_config.numLearningTrials,
				_config.numTestTrials,
				_config.timeLimit,
				new GestureTask(),
				new TransparencyInfo(_config.minTransparency, _config.baseTransparency, _config.maxTransparency, _config.errorThreshold));

			// Set up the transparency settings through the config object.
			_transparencySetting = ITransparencySetting.SelectTransparencySetting(_config.transparencyType);
		}

		/// <summary>
		/// This method starts the trial and logs that the trial has been started.
		/// </summary>
		public void StartTrial()
		{
			Logger.Log("Trial Started");
			_started = true;
		}

		/// <summary>
		/// This method marks the trial as completed. It also logs the trial performance and loads the next trial.
		/// </summary>
		private void CompleteTrial(bool timeExpired)
		{
			Logger.Log($"Trial Complete Within Time Limit{Logger.Delimiter}{timeExpired}");
			_trialPerformances[_trialIndex].CalculateTrialStatistics(_student.BaselinePerformance, TimeController.CurrentTime, timeExpired);
			Logger.Log(_trialPerformances[_trialIndex].LogPerformance());
			LoadTrial();
		}

		/// <summary>
		/// This method handles all session controller functionality that must be done before completion.
		/// It also logs the full trial performance array.
		/// </summary>
		private void CompleteSession() {
			
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
				CompleteSession();
				SceneManager.LoadScene("Menu");
			}
			
			// Load the next trial of the session.
			_trialIndex += 1;
			_trialPerformances.Add(new TrialPerformance());
			SceneManager.LoadScene("TestEnvironment");
		}
	}
}
