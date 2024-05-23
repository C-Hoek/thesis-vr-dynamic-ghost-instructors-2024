using Config;
using Task;
using TransparencySettings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Session
{
    public class SessionController : MonoBehaviour
    {
        [SerializeField] private ScriptableConfigObject config;
        [SerializeField] private GameObject ghost;
        
        private Session session;
        private ITransparencySetting transparencySetting;

        private int trialIndex = 0;

        /// <summary>
        /// This method sets up the full session based on the config object linked through the Unity Editor.
        /// </summary>
        public void Start()
        {
            // Set up the session through the config object.
            session = new Session(
                config.numLearningTrials, 
                config.numTestTrials, 
                config.timeLimit, 
                ITask.SelectTask(config.taskName));
            
            // Set up the transparency settings through the config object.
            transparencySetting = ITransparencySetting.SelectTransparencySetting(config.transparencyType);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void LoadTrial()
        {
            if (session.IsComplete(trialIndex))
            {
                SceneManager.LoadScene("Menu");
            }

        }

        public void IsComplete()
        {

        }
    }
}