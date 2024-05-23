namespace Performance
{
	public record StatTriplet<T>(T task1, T task2, T combined)
		where T : struct
	{
		public T task1 { get; } = task1;
		public T task2 { get; } = task2;
		public T combined { get; } = combined;
	}
}