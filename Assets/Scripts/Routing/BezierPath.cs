using System.Collections.Generic;
using Sessions;
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
			var curveIndex = Mathf.CeilToInt(t / 30 * path.Count);
			var curveDuration = 30 / path.Count;
			var endTimePreviousCurve = curveDuration * Mathf.FloorToInt(t / 30 * path.Count);

			var progressIntoCurrentCurve = (t - endTimePreviousCurve) / curveDuration;

			return path[curveIndex].PositionAt(progressIntoCurrentCurve);
		}
	}	
}
