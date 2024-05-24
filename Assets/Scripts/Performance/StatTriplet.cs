namespace Performance
{
	public record StatTriplet<T>(T Task1, T Task2, T Combined)
		where T : struct
	{
		public T Task1 { get; } = Task1;
		public T Task2 { get; } = Task2;
		public T Combined { get; } = Combined;
	}
}
