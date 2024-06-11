using UnityEngine;
using System.Collections.Generic;

namespace Config
{
	[CreateAssetMenu(fileName = "ScriptableConfigObject", menuName = "Scriptable Object/Config")]
	public class ScriptableConfigObject : ScriptableObject
	{
		public string logString;

		public int numLearningTrials;
		public int numTestTrials;

		public float timeLimit;

		public string transparencyType;
		public List<float> minTransparency;
		public float baseTransparency;
		public List<float> maxTransparency;
		public float errorThreshold;

		/// <summary>
		/// This method returns the log string.
		/// </summary>
		/// <returns> A string containing config information. </returns>
		public string LogConfig()
		{
			return logString;
		}
	}
}
