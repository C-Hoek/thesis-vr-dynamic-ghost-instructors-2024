using UnityEngine;
using Util;
using Logger = Logging.Logger;

namespace Performance
{
	public class ErrorCalculation
	{
		// Attributes that determine how important the axis of the position is in the context of the full error calculation.
		private const float XPosW = 1f;
		private const float YPosW = 1f;
		private const float ZPosW = 0.5f;

		// Attribute that determines what the maximum allowed distance error is in each direction.
		private const float XMaxPosError = 0.1f;
		private const float YMaxPosError = 0.1f;
		private const float ZMaxPosError = 0.15f;

		/// <summary>
		/// This method calculates the error between the movements of the student and those of the Ghost.
		/// </summary>
		/// <param name="studentPointer"> The position of the student's pointer. </param>
		/// <param name="ghostPointer"> The position of the ghost's pointer. </param>
		/// <returns> A number between 0 and 1. </returns>
		public static float CalculateError(Vector3 studentPointer, Vector3 ghostPointer)
		{
			var posError = Utils.Vector3Abs(ghostPointer - studentPointer);

			// If the distance between the student and the teacher exceeds the maximum distance in any direction, return the maximum error.
			if (DistanceOrRotExceedsMaxErrors(posError)) return 1.0f;	

			// Add weight scaling and max error scaling to ensure that the maximum error will be equal to 1.0f, and a minimum of 0.0f.
			var scaledPosError = new Vector3 (posError.x / XMaxPosError, posError.y / YMaxPosError, posError.z / ZMaxPosError);			
			var distanceError =  (XPosW * scaledPosError.x + YPosW * scaledPosError.y + ZPosW * scaledPosError.z) / (XPosW + YPosW + ZPosW);
			var error = distanceError;

			// Log and return the error.
			return error;
		}

		/// <summary>
		/// This method determines if any of the calculated distances exceed their maximum allowed distance, 
		/// and if any of the calculated rotation errors exceed their maximum allowed error.
		/// </summary>
		/// <param name="posError"> The calculated distances between the student and the ghost's hands. </param>
		/// <returns> True if any of the distances exceeds the maximum allowed distance. False otherwise. </returns>
		private static bool DistanceOrRotExceedsMaxErrors(Vector3 posError)
		{
			return posError.x > XMaxPosError || posError.y > YMaxPosError || posError.z > ZMaxPosError;
		}

		/// <summary>
		/// This method logs the error to the log file.
		/// </summary>
		/// <param name="student"> The student's pointer location. </param>
		/// <param name="ghost"> The ghost avatar's pointer location. </param>
		/// <param name="error"> The calculated error. </param>
		/// <returns> Returns a string containing the useful properties of the two transforms, and the calculated error. </returns>
		public static string LogError(Vector3 student, Vector3 ghost, float error) {
			var logString = $"Error{Logger.Delimiter}{error}{Logger.Delimiter}" +
							$"Student Position{Logger.Delimiter}{student}{Logger.Delimiter}" +
							$"Ghost Position{Logger.Delimiter}{ghost}";
			return logString;
		}
	}
}
