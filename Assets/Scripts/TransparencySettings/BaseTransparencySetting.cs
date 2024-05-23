namespace TransparencySettings
{
    public class BaseTransparencySetting : ITransparencySetting
    {
        /// <summary>
        /// This method can be called to obtain the target transparency value for the Ghost avatar.
        /// This method always returns the same value. The value will be equal to the `BaseGhostTransparency` value of the provided settings file.
        /// </summary>
        /// <returns> A value between 0 and 1. Where 1 denotes completely visible, and 0 denotes completely invisible. </returns>
        public float TargetGhostTransparency()
        {
            //TODO: Obtain the BaseTransparency from the settings.
            return 0.5f;
        }
    }
}