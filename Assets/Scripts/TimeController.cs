using UnityEngine;

public class TimeController : MonoBehaviour
{
	private static float s_currentTime;
	public static float CurrentTime
	{
		get => s_currentTime;
	}

	/// <summary>
	/// This method sets the current time to 0 at the start of a trial.
	/// </summary>
	public void Start()
	{
		s_currentTime = 0f;
	}

	/// <summary>
	/// This method is used to keep track of the current trial time in milliseconds.
	/// </summary>
	public void Update()
	{
		s_currentTime += Time.deltaTime * 1000f;
	}

}
