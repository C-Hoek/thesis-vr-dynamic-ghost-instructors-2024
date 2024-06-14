using System.Collections;
using UnityEngine;

public class HandAnimationController : MonoBehaviour
{
	[SerializeField] private Animator animator;

	private const float SwitchStep = 0.05f;

	/// <summary>
	/// This method changes the hand animation from 'Default' to 'Point'.
	/// </summary>
	public void ActivatePointAnimation()
	{
		StartCoroutine(ChangeHandAnimation());
	}

	/// <summary>
	/// This method takes 0.33... seconds to change the animation from 'Default' to 'Point'.
	/// </summary>
	/// <returns> N/A. This is a coroutine. </returns>
	private IEnumerator ChangeHandAnimation()
	{
		for (var inp = 0f; inp < 1.0f; inp += 0.05f)
		{
			animator.SetFloat("Point", inp);
			yield return new WaitForSeconds(SwitchStep / 3);
		}
	}
}
