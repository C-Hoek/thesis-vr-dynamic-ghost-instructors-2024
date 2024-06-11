using System.Collections.Generic;
using System.Linq;
using Sessions;
using UnityEngine;

namespace Routing
{
	public class BezierPath : MonoBehaviour
	{
		[SerializeField] private List<BezierCurve> path;

		/// <summary>
		/// This method returns the point that occurs in the path at this point in time.
		/// </summary>
		/// <param name="t"> The current point in time. </param>
		/// <returns> The position that occurs at the passed time value in the Bezier path. </returns>
		public Vector3 PositionAt(float t)
		{
			var time = SessionController.Session.TimeToCompletePath;
			if (t > time) return path[path.Count - 1].PositionAt(1);
			
			// Find the target curve
			var arcLengths = path.Select(x => x.ArcLength).ToList();
			var totalArcLength = arcLengths.Sum();

			var i = 0;
			var prevEndTime = 0f;
			while (prevEndTime + time * arcLengths[i] / totalArcLength < t)
			{
				// Increment the previous end time by the time limit * the fraction of the total path length that the curve poses.
				prevEndTime += time * arcLengths[i] / totalArcLength;
				i++;
			}
			
			// Obtain the curve duration.
			var curveDuration = time * arcLengths[i] / totalArcLength;
			
			// Calculate the progress in the current curve.
			var progressIntoCurrentCurve = (t - prevEndTime) / curveDuration;
			
			// Return the target position.
			return path[i].PositionAt(progressIntoCurrentCurve);
		}
	}	
}
