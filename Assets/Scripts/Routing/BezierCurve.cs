using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Routing
{
	public class BezierCurve : MonoBehaviour
	{
		[SerializeField] private GameObject start;
		private Vector3 P1
		{
			get => start.transform.position;
		}

		[SerializeField] private GameObject end;
		private Vector3 P2
		{
			get => end.transform.position;
		}

		[SerializeField] private GameObject control1;
		private Vector3 C1
		{
			get => control1.transform.position;
		}

		[SerializeField] private GameObject control2;
		private Vector3 C2
		{
			get => control2.transform.position;
		}

		private const int NumApproximationPoints = 200;
		private List<float> _segmentFractions;

		private float _arcLength;
		public float ArcLength
		{
			get => _arcLength;
		}

		/// <summary>
		/// This method initialises the approximate segment spacing.
		/// </summary>
		private void Awake()
		{
			ApproximateSegmentLengthFractions();
		}
		
		/// <summary>
		/// This method uses the approximate segment length fractions to smooth the speed approximately evenly over the
		/// entirety of the Bézier curve.
		/// </summary>
		/// <param name="t"> This represents the time parameter and should be scaled between 0 and 1. </param>
		/// <returns> The target position at this point in time. </returns>
		public Vector3 PositionAt(float t)
		{
			// Find the index of the current segment.
			var i = 0;
			var sumFractions = 0f;
			while (sumFractions + _segmentFractions[i] < t && i < NumApproximationPoints)
			{
				sumFractions += _segmentFractions[i];
				i++;
			}
			
			// If the current segment is the start segment (which has length 0); return the starting position.
			if (i == 0) return PosAt(0);
			
			// Find the progress within the section.
			var tInSegment = (t - sumFractions) / _segmentFractions[i];
			
			// Add the resulting tInSegment to the t belonging to the fraction and return the approximate point.
			return PosAt(tInSegment / 100f + sumFractions);
		}
		
		/// <summary>
		/// This method determines the vector at which an object following the curve should be at this point in time.
		/// </summary>
		/// <param name="t"> This represents the time parameter and should be scaled between 0 and 1. </param>
		/// <returns> The position at this point in time. </returns>
		private Vector3 PosAt(float t)
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
				Gizmos.DrawSphere(PosAt(i), .005f);
			}
		}

		/// <summary>
		/// This method approximates segment length fractions. Without using this method, the speed along the curve will vary.
		/// First, the distances between 'NumApproximationPoints' - 1 points are estimated.
		/// These distances are summed to provide the approximate arc length of the Bézier curve.
		/// The segment fractions are defined as the segment length divided by the total arc length.
		/// Lastly, each segment fraction is the sum of its own fraction + that of the previous segment.
		/// </summary>
		private void ApproximateSegmentLengthFractions()
		{
			var distList = new List<float>();
			distList.Add(0);
			for (var i = 1; i < NumApproximationPoints; i++)
			{
				var dist = Vector3.Distance(PosAt((i - 1) / 100f), PosAt(i / 100f));
				distList.Add(dist);
			}

			// Obtain the approximate arc length.
			_arcLength = distList.Sum();

			// Determine the fraction of the total length per segment.
			_segmentFractions = distList.Select(x => x / _arcLength).ToList();

			var j = 1;
			while (j < NumApproximationPoints)
			{
				j++;
			}
		}
	}
}
