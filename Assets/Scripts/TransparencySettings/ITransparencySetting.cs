namespace TransparencySettings
{
    public interface ITransparencySetting
    {
        /// <summary>
        /// Implementations of this method can be called to obtain the target transparency value for the Ghost avatar.
        /// </summary>
        /// <returns> A value between 0 and 1. Where 1 denotes completely visible, and 0 denotes completely invisible. </returns>
        public float TargetGhostTransparency();
        
        /// <summary>
        /// This method returns an ITransparencySetting class based on a string representation of the transparency type.
        /// </summary>
        /// <param name="transparencyType"> A string that is expected to say "base" or "dynamic". </param>
        /// <returns> An ITransparencySetting class with the appropriate method implementations. </returns>
        public static ITransparencySetting SelectTransparencySetting(string transparencyType)
        {
            return transparencyType switch
            {
                "dynamic" => new DynamicTransparencySetting(),
                _ => new BaseTransparencySetting()
            };
        }
    }
}