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

		public float timeToCompletePath;
		public float timeLimit;

		public string transparencyType;
		public List<float> minTransparency;
		public float baseTransparency;
		public List<float> maxTransparency;
		public float errorThreshold;
	}
}
