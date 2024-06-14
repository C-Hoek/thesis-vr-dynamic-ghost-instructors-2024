using System.Collections.Generic;
using System.Linq;
using Sessions;
using UnityEngine;

namespace Routing
{
	public class BezierPath : MonoBehaviour
	{
		[SerializeField] private List<BezierCurve> path;
		private bool _finished;

		/// <summary>
		/// This method returns the point that occurs in the path at this point in time.
		/// </summary>
		/// <param name="t"> The current point in time. </param>
		/// <returns> The position that occurs at the passed time value in the Bezier path. </returns>
		public Vector3 PositionAt(float t)
		{
			// If the path has been completed, register this and return the last position of the path.
			var time = SessionController.Session.TimeToCompletePath * 1000;
			if (t >= time)
			{
				if (!_finished)
				{
					SessionEventHandler.Instance.PathComplete();
					_finished = true;
				}
				return path[^1].PositionAt(1.0f);
			}
			
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
