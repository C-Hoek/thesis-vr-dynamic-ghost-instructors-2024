using Sessions;
using System.IO;
using Config;
using UnityEngine;
using Util;

namespace Logging
{
	/// <summary>
	/// This class will log to: C:\Users\{User}\AppData\LocalLow\DefaultCompany\VR Ghost\Logs
	/// </summary>
	public class Logger : MonoBehaviour
	{
		private StreamWriter _file;

		public static string Delimiter = ",";

		/// <summary>
		/// This function is used to set the Logger up with the correct file.
		/// This should be done at the start of a set of trials.
		/// </summary>
		public void Start()
		{
			var path = Application.persistentDataPath + "/Logs/" + SessionController.TrialID +
					   ".log";
			Directory.CreateDirectory(Path.GetDirectoryName(path));
			_file = new StreamWriter(path, true);
		}

		/// <summary>
		/// Log a string to the file.
		/// </summary>
		/// <param name="infoString"> The string to add to the file. </param>
		public void Log(string infoString)
		{
			_file.Write(TimeController.CurrentTime + Delimiter + infoString + "\n");
		}

		/// <summary>
		/// This method logs the trial index and useful configuration information.
		/// </summary>
		/// <param name="trialIndex"> The current trial index. </param>
		/// <param name="config"> The configuration settings used for the session. </param>
		public void LogTrialInfo(int trialIndex, ScriptableConfigObject config)
		{
			var logString = $"Trial Settings of Trial{Delimiter} {trialIndex}\n" +
				"===============================================================================\n" +
				$"Configuration{Delimiter} {config.logString}\n" +
				$"#LearningTrials{Delimiter} {config.numLearningTrials}\n" +
				$"#TestTrials{Delimiter} {config.numTestTrials}\n" +
				$"Time To Complete Path{Delimiter} {config.timeToCompletePath}\n" +
				$"Time Limit{Delimiter} {config.timeLimit}\n" +
				$"Base Transparency{Delimiter} {config.baseTransparency}\n" +
				$"Min Transparency{Delimiter} {Utils.LogList(config.minTransparency)}\n" +
				$"Max Transparency{Delimiter} {Utils.LogList(config.maxTransparency)}\n" +
				$"Error Threshold{Delimiter} {config.errorThreshold}\n" +
				"===============================================================================";
			Log(logString);
		}

		/// <summary>
		/// Close the logger to avoid any file errors.
		/// </summary>
		public void OnDestroy()
		{
			_file.Close();
		}
	}
}
