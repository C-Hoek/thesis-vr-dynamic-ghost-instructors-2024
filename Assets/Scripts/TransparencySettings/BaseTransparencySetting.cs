using Sessions;

namespace TransparencySettings
{
	public class BaseTransparencySetting : ITransparencySetting
	{
		/// <summary>
		/// This method can be called to obtain the target transparency value for the Ghost avatar.
		/// This method always returns the same value. The value will be equal to the `BaseGhostTransparency` value of the provided settings file.
		/// </summary>
		/// <param name="calculatedError"> The error value when comparing the student with the Ghost avatar. </param>
		/// <param name="trialIndex"> The current trial's index. </param>
		/// <returns> A value between 0 and 1. Where 1 denotes completely visible, and 0 denotes completely invisible. This is the same value as the base transparency in the config objects. </returns>
		public float TargetGhostTransparency(float calculatedError, int trialIndex)
		{
			// If the trial index (0-based) indicates a test trial, return 0 as the ghost should not be visible in the test stage.
			return trialIndex + 1 > SessionController.Session.NumLearningTrials ? 0.0f : SessionController.Session.TransparencyInfo.BaseTransparency;
		}
	}
}
