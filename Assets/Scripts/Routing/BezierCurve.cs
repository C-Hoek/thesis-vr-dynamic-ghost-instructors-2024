using System;
using UnityEngine;

namespace Routing
{
	public class BezierCurve : MonoBehaviour
	{
		[SerializeField] private GameObject start;
		private Vector3 P1 => start.transform.position;
		
		[SerializeField] private GameObject end;
		private Vector3 P2 => end.transform.position;
		
		[SerializeField] private GameObject control1;
		private Vector3 C1 => control1.transform.position;
		
		[SerializeField] private GameObject control2;
		private Vector3 C2 => control2.transform.position;

		/// <summary>
		/// This method determines the vector at which an object following the curve should be at this point in time.
		/// </summary>
		/// <param name="t"> This represents the time parameter and should be scaled between 0 and 1. </param>
		/// <returns> The position at this point in time. </returns>
		public Vector3 PositionAt(float t)
		{
			return Mathf.Pow(1 - t, 3) * P1 + 3 * Mathf.Pow(1 - t, 2) * t * C1 +
			       3 * (1 - t) * Mathf.Pow(t, 2) * C2 + Mathf.Pow(t, 3) * P2;
		}

		/// <summary>
		/// This method enables easy editing of the Bézier curve through visualisation.
		/// </summary>
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(P1, 0.05f);
			Gizmos.DrawWireSphere(P2, 0.05f);

			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(C1, 0.05f);
			Gizmos.DrawWireSphere(C2, 0.05f);
			
			Gizmos.color = Color.cyan;
			
			for (var i = 0.0f; i < 1.0f; i += 0.01f)
			{
				Gizmos.DrawSphere(PositionAt(i), .005f);
			}
		}
	}
}
