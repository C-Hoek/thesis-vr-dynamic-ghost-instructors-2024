using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "ScriptableConfigObject", menuName = "Scriptable Object/Config")]
    public class ScriptableConfigObject : ScriptableObject
    {
        public string logString;
        
        public string LogConfig()
        {
            return logString;
        }
    }   
}