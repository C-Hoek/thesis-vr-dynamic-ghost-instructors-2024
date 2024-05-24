using System;
using Session;

namespace TransparencySettings
{
	public class DynamicTransparencySetting : ITransparencySetting
	{
		/// <summary>
		/// This method can be called to obtain the target transparency value for the Ghost avatar.
		/// This method will call `ComputeTransparency` to dynamically calculate the transparency based on how well the student is performing in comparison with the Ghost.
		/// </summary>
		/// <param name="calculatedError"> The error value when comparing the student with the Ghost avatar. </param>
		/// <returns> A value between 0 and 1. Where 1 denotes completely visible, and 0 denotes completely invisible. </returns>
		public float TargetGhostTransparency(float calculatedError)
		{
			var transparencyInfo = SessionController.Session.TransparencyInfo;
			var transparencyModifier = ComputeTransparencyModifier(calculatedError, transparencyInfo.ErrorThreshold);

			return transparencyInfo.MinTransparency + transparencyModifier * (transparencyInfo.MaxTransparency - transparencyInfo.MinTransparency);
		}

		/// <summary>
		/// This method calculates the transparency of the Ghost avatar based on an exponential evaluation of how well the student is performing.
		/// </summary>
		/// <param name="calculatedError"> The error value when comparing the student with the Ghost avatar. This should be between 0.0f and 1.0f. </param>
		/// <param name="errorThreshold"> The error threshold above which the Ghost avatar should be visible to some extent. </param>
		/// <returns> A value between 0 and 1. Where 1 denotes completely visible, and 0 denotes completely invisible. </returns>
		public float ComputeTransparencyModifier(float calculatedError, float errorThreshold)
		{
			if (calculatedError <= errorThreshold) return 0.0f;

			// When the student is making a small mistake, the Ghost should stay largely invisible.
			// For larger mistakes, the Ghost should become more and more visible.
			var exponentialError = Math.Exp(calculatedError);

			// The modifier should remain capped at 1.0f.
			return (float) exponentialError / (float) Math.E;
		}
	}
}
