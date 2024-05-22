namespace Performance
{
    public record Statistics (StatTriplet<float> averageDistanceToGhost, StatTriplet<float> maxDistanceToGhost, StatTriplet<float> minDistanceToGhost,
        StatTriplet<float> timeToCompletion, StatTriplet<bool> completed, StatTriplet<float> taskPerformance, StatTriplet<float> learningEffect)
    {
        public StatTriplet<float> averageDistanceToGhost { get; init; };
        public StatTriplet<float> maxDistanceToGhost { get; init; }
        public StatTriplet<float> minDistanceToGhost { get; init; };

        // Obtain completion statistics.
        public StatTriplet<float> timeToCompletion { get; init; };
        public StatTriplet<bool> completed { get; init; };

        // Calculate the task performance and learning effect.
        public StatTriplet<float> taskPerformance { get; init; };
        public StatTriplet<float> learningEffect { get; init; };
    }
}