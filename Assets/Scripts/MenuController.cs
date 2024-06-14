using System;
using System.Collections.Generic;
using Config;
using Sessions;
using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
	[SerializeField] private TMP_Dropdown configDropdown;
	[SerializeField] private TMP_InputField trialIndexInputField;
	[SerializeField] private ScriptableConfigObject staticConfig;
	[SerializeField] private ScriptableConfigObject dynamicConfig;
	[SerializeField] private TextMeshProUGUI versionNumber;

	[SerializeField] private SessionController sessionController;

	private readonly List<string> _dropdownOptions = new List<string> { "static", "dynamic" };

	/// <summary>
	/// This method displays the version number on the main menu.
	/// </summary>
	public void Awake()
	{
		versionNumber.text = Application.version;
	}

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
		
		// Set the trial index.
		SessionController.TrialIndex = Int32.Parse(trialIndexInputField.text);
		
		sessionController.Setup();

		// Load the test environment.
		sessionController.LoadTrial();
	}
}
