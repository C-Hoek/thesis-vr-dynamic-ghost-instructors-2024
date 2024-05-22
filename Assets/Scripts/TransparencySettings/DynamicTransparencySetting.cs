namespace TransparencySettings
{
    class DynamicTransparencySetting
    {
        /// <summary>
        /// This method can be called to obtain the target transparency value for the Ghost avatar.
        /// This method will call `ComputeTransparency` to dynamically calculate the transparency based on how well the student is performing in comparison with the Ghost.
        /// </summary>
        /// <returns> A value between 0 and 1. Where 1 denotes completely visible, and 0 denotes completely invisible. </returns>
        public float TargetGhostTransparency()
        {
            // TODO: Call `ComputeTransparency` with the calculated error.
            // TODO: Obtain the Min and Max Ghost Transparency from the Settings.
            return 0.5f;
        }

        // TODO: Add what values the error can take and what this means for the computation in the following method comment.
        /// <summary>
        /// This method calculates the transparency of the Ghost avatar based on how well the student is performing.
        /// </summary>
        /// <param name="calculatedError"> The error value when comparing the student with the Ghost avatar. </param>
        /// <returns> A value between 0 and 1. Where 1 denotes completely visible, and 0 denotes completely invisible. </returns>
        public float ComputeTransparency(float calculatedError)
        {
            //TODO: Add the method body.
        }
    }
}