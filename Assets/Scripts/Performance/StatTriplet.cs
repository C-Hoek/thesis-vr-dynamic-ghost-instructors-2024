namespace Performance
{
	public record StatTriplet<T>(T task1, T task2, T combined) where T : struct
	{
		public T task1 { get; init; }
		public T task2 { get; init; }
		public T combined { get; init; }
	}
}