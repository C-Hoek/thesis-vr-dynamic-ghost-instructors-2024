using System.Collections.Generic;

namespace TransparencySettings
{
	public record TransparencyInfo(List<float> MinTransparency, float BaseTransparency, List<float> MaxTransparency, float ErrorThreshold)
	{
		public List<float> MinTransparency { get; } = MinTransparency;
		public float BaseTransparency { get; } = BaseTransparency;
		public List<float> MaxTransparency { get; } = MaxTransparency;
		public float ErrorThreshold { get; } = ErrorThreshold;
	}
}
