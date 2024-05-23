namespace Performance
{
    public record Statistics (StatTriplet<float> averageDistanceToGhost, StatTriplet<float> maxDistanceToGhost, StatTriplet<float> minDistanceToGhost,
        StatTriplet<float> timeToCompletion, StatTriplet<bool> completed, StatTriplet<float> taskPerformance, StatTriplet<float> learningEffect)
    {
        public StatTriplet<float> averageDistanceToGhost { get; } = averageDistanceToGhost;
        public StatTriplet<float> maxDistanceToGhost { get; } = maxDistanceToGhost;
        public StatTriplet<float> minDistanceToGhost { get; } = minDistanceToGhost;

        // Obtain completion statistics.
        public StatTriplet<float> timeToCompletion { get; } = timeToCompletion;
        public StatTriplet<bool> completed { get; } = completed;

        // Calculate the task performance and learning effect.
        public StatTriplet<float> taskPerformance { get; } = taskPerformance;
        public StatTriplet<float> learningEffect { get; } = learningEffect;
    }
}