namespace TransparencySettings
{
    public interface ITransparencySetting
    {
        /// <summary>
        /// Implementations of this method can be called to obtain the target transparency value for the Ghost avatar.
        /// </summary>
        /// <returns> A value between 0 and 1. Where 1 denotes completely visible, and 0 denotes completely invisible. </returns>
        public float TargetGhostTransparency();
    }
}