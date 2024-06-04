using UnityEngine;
using UnityEngine.XR;

public class HandAnimator : MonoBehaviour
{
	private InputDevice _inputDevice;
	private Animator _animator;

	private const string TriggerParameterName = "Trigger";

    
	/// <summary>
	/// This method sets the "Trigger" parameter of the hand animator to the value of the controller's trigger button.
	/// </summary>
    void Update()
    {
		if (_inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)) {
			_animator.SetFloat(TriggerParameterName, triggerValue);
		}
		else
		{
			_animator.SetFloat(TriggerParameterName, 0);
		}
    }
}
