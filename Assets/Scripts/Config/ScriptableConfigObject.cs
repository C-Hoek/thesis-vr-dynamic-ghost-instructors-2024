using UnityEngine;

namespace Config
{
	[CreateAssetMenu(fileName = "ScriptableConfigObject", menuName = "Scriptable Object/Config")]
	public class ScriptableConfigObject : ScriptableObject
	{
		public string logString;

		public int numLearningTrials;
		public int numTestTrials;

		public float timeLimit;
		public string taskName;

		public string transparencyType;
		public float baseTransparency;
		public float maxTransparency;
		public float minTransparency;
		public float errorThreshold;

		public string LogConfig()
		{
			return logString;
		}
	}
}
