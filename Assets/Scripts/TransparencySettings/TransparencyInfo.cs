namespace TransparencySettings
{
	public record TransparencyInfo(float MinTransparency, float BaseTransparency, float MaxTransparency, float ErrorThreshold)
	{
		public float MinTransparency { get; } = MinTransparency;
		public float BaseTransparency { get; } = BaseTransparency;
		public float MaxTransparency { get; } = MaxTransparency;
		public float ErrorThreshold { get; } = ErrorThreshold;
	}
}
