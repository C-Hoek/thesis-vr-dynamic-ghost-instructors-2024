using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Routing
{
	public class BezierPath : MonoBehaviour
	{
		[SerializeField] private List<BezierCurve> path;

		/// <summary>
		/// This method adds a curve to the Bezier path.
		/// </summary>
		/// <param name="curve"> The curve that should be added to the path. </param>
		public void AddCurve(BezierCurve curve)
		{
			path ??= new List<BezierCurve>();
			path.Add(curve);
		}

		/// <summary>
		/// This method returns the point that occurs in the path at this point in time.
		/// </summary>
		/// <param name="t"> The current point in time. </param>
		/// <returns> The position that occurs at the passed time value in the Bezier path. </returns>
		public Vector3 PositionAt(float t)
		{
			if (t > 30) return Vector3.zero;
			//TODO: Set 30 to the time limit: SessionController.Session.TimeLimit;
			
			// Find the target curve
			var arcLengths = path.Select(x => x.ArcLength).ToList();
			var totalArcLength = arcLengths.Sum();

			var i = 0;
			var prevEndTime = 0f;
			while (prevEndTime + 30 * arcLengths[i] / totalArcLength < t)
			{
				// Increment the previous end time by the time limit * the fraction of the total path length that the curve poses.
				prevEndTime += 30 * arcLengths[i] / totalArcLength;
				i++;
			}
			
			// Obtain the curve duration.
			var curveDuration = 30 * arcLengths[i] / totalArcLength;
			
			// Calculate the progress in the current curve.
			var progressIntoCurrentCurve = (t - prevEndTime) / curveDuration;
			
			// Return the target position.
			return path[i].PositionAt(progressIntoCurrentCurve);
		}
	}	
}
