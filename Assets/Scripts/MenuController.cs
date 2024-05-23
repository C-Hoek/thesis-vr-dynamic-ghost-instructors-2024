using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public TMP_Dropdown configDropdown;
    private readonly List<string> dropdownOptions = new List<string>{"static", "dynamic"};
    
    public void StartSession()
    {
        var configType = dropdownOptions[configDropdown.value];
        SceneManager.LoadScene("TestEnvironment");
    }
}
