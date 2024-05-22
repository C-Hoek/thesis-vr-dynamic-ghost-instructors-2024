namespace Trial
{
    public record Trial(
        int sessionID,
        int trialNumber
    )
    {
        private int sessionID { get; set; }
        private int trialNumber { get; set; }
    }
}