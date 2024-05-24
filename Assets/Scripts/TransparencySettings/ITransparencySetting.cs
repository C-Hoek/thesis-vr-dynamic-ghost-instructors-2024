namespace TransparencySettings
{
	public interface ITransparencySetting
	{
		/// <summary>
		/// Implementations of this method can be called to obtain the target transparency value for the Ghost avatar.
		/// </summary>
		/// <param name="calculatedError"> The error value when comparing the student with the Ghost avatar. </param>
		/// <returns> A value between 0 and 1. Where 1 denotes completely visible, and 0 denotes completely invisible. </returns>
		public float TargetGhostTransparency(float calculatedError);

		/// <summary>
		/// This method returns an ITransparencySetting class based on a string representation of the transparency type.
		/// </summary>
		/// <param name="transparencyType"> A string that is expected to say "static" or "dynamic". </param>
		/// <returns> An ITransparencySetting class with the appropriate method implementations. </returns>
		public static ITransparencySetting SelectTransparencySetting(string transparencyType)
		{
			return transparencyType switch
			{
				"static" => new BaseTransparencySetting(),
				"dynamic" => new DynamicTransparencySetting(),
				_ => new BaseTransparencySetting()
			};
		}
	}
}
