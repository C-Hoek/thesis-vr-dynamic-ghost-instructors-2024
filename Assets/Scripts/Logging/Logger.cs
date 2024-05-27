using System;
using System.IO;
using UnityEngine;

namespace Logging
{
	/// <summary>
	/// This class will log to: C:\Users\{User}\AppData\LocalLow\DefaultCompany\VR Ghost\Logs
	/// </summary>
	public class Logger : MonoBehaviour
	{
		private StreamWriter _file;

		public const string Delimiter = ",";

		/// <summary>
		/// This function is used to set the Logger up with the correct file.
		/// This should be done at the start of a set of trials.
		/// </summary>
		public void Start()
		{
			var path = Application.persistentDataPath + "/Logs/" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") +
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
			_file.Write(infoString + "\n");
		}

		/// <summary>
		/// Close the logger to avoid any file errors.
		/// </summary>
		public void Close()
		{
			_file.Close();
		}
	}
}
