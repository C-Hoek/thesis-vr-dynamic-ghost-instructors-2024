using Task;
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
        public float baseGhostTransparency;
        public float maxTransparency;
        public float minTransparency;
        
        public string LogConfig()
        {
            return logString;
        }
    }   
}