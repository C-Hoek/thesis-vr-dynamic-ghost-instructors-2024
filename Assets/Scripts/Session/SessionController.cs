using System;
using System.Collections.Generic;
using Config;
using GameEntities;
using Performance;
using Routing;
using Task;
using TransparencySettings;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;
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

		public static string TrialID
		{
			get => $"{s_session.SessionID}_{s_trialIndex}";
		}

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
		private static int s_trialIndex = -1;
		private static bool s_started;
		public static bool Started
		{
			get => s_started;
		}
		
		private bool _completed;
		private bool _infoLogged;

		private List<TrialPerformance> _trialPerformances = new List<TrialPerformance>();
		
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
			_student ??= FindObjectOfType<Student>();
			
			if (!s_started) return;
			if (!_infoLogged)
			{
				Logger?.LogTrialInfo(s_trialIndex, _config);
				_infoLogged = Logger is not null;
			}
			
			if (_ghost is null || _student is null) return;

			// Obtain the combined position and rotation error and set the ghost's transparency to the appropriate amount.
			var time = TimeController.CurrentTime;
			//TODO: Set the transforms to the hands
			var error = ErrorCalculation.CalculateError(_student.GetPointerPosition(), _ghost.GetPointerPosition());
			Logger?.Log(ErrorCalculation.LogError(_student.GetPointerPosition(), _ghost.GetPointerPosition(), error));
			_ghost.SetTransparency(_transparencySetting.TargetGhostTransparency(error, s_trialIndex));
			
			// Set the ghost avatar's hand position to the appropriate task position.
			s_session.Task.SetGhostPosition(_ghost, time);

			// Add the error to the trial performance.
			_trialPerformances ??= new List<TrialPerformance>();
			_trialPerformances.Add(new TrialPerformance());
			_trialPerformances[s_trialIndex].AddTaskError(error, time);
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
			s_session = new Session(
				_config.numLearningTrials,
				_config.numTestTrials,
				_config.timeToCompletePath,
				_config.timeLimit,
				new TaskHolder(),
				new TransparencyInfo(_config.minTransparency, _config.baseTransparency, _config.maxTransparency, _config.errorThreshold),
				Guid.NewGuid().ToString());
			
			// Set up the transparency settings through the config object.
			_transparencySetting = ITransparencySetting.SelectTransparencySetting(_config.transparencyType);
		}

		/// <summary>
		/// This method starts the trial and logs that the trial has been started.
		/// </summary>
		private void StartTrial()
		{
			_student.ActivatePointingAnimation();
			Logger.Log("Trial Started");
			TimeController.Enabled = true;
			s_started = true;
			_ghost = FindObjectOfType<Ghost>();
			_ghost.SetActive();
		}

		/// <summary>
		/// This method marks the trial as completed. It also logs the trial performance and loads the next trial.
		/// </summary>
		private void CompleteTrial(bool timeExpired)
		{
			if (!s_started || _completed) return;
			
			_completed = true;
			Logger.Log($"Trial Complete Within Time Limit{Logger.Delimiter}{!timeExpired}");
			_trialPerformances[s_trialIndex].CalculateTrialStatistics(_student.BaselinePerformance, TimeController.CurrentTime, timeExpired);
			Logger.Log(_trialPerformances[s_trialIndex].LogPerformance());
			
			// Set the student's baseline performance if this was the first trial.
			if (s_trialIndex == 0)
			{
				_student.BaselinePerformance = _trialPerformances[s_trialIndex].TrialStats.TaskPerformance;
			}
			
			LoadTrial();
		}

		/// <summary>
		/// This method handles all session controller functionality that must be done before completion.
		/// It also logs the full trial performance array.
		/// </summary>
		private void CompleteSession() {
			Logger.Log($"Trial Performance{Utils.LogList(_trialPerformances)}");
		}

		/// <summary>
		/// This method loads the next trial if there is any, and returns to the main menu otherwise
		/// </summary>
		public void LoadTrial()
		{
			s_started = false;
			
			// Load the menu if the session is complete.
			if (s_session.IsComplete(s_trialIndex + 1))
			{
				CompleteSession();
				SceneManager.LoadScene("Menu");
				return;
			}
			
			// Load the next trial of the session.
			s_trialIndex += 1;
			_trialPerformances.Add(new TrialPerformance());
			SceneManager.LoadScene("TestEnvironment");

			// Set up the task object.
			var taskObj = Instantiate(s_session.Task.Setup(), transform, false);
			var path = taskObj.GetComponentInChildren<BezierPath>();
			s_session.Task.Path = path;
			// Set the task object to the appropriate place.
			taskObj.transform.position = s_session.Task.TaskPosition;
			
			// Clear the logger, ghost, and student.
			s_logger = null;
			_student = null;
			_ghost = null;
			_completed = false;
		}
	}
}
