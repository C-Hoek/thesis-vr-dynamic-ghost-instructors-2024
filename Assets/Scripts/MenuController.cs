using System.Collections.Generic;
using Config;
using Session;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	public TMP_Dropdown configDropdown;
	public ScriptableConfigObject staticConfig;
	public ScriptableConfigObject dynamicConfig;

	[SerializeField] private SessionController sessionController;

	private readonly List<string> _dropdownOptions = new List<string> { "static", "dynamic" };

	/// <summary>
	/// This method loads the selected configuration type (static or dynamic) and then loads the test environment.
	/// </summary>
	public void StartSession()
	{
		// Set the configuration of the session controller.
		var configType = _dropdownOptions[configDropdown.value];
		var config = configType switch
		{
			"static" => staticConfig,
			"dynamic" => dynamicConfig,
			_ => null
		};
		
		if (config is null) return;
		sessionController.Config = config;
		
		// Load the test environment.
		SceneManager.LoadScene("TestEnvironment");
	}
}
