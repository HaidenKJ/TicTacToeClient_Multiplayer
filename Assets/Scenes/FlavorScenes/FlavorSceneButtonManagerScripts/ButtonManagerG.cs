using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManagerG : MonoBehaviour // This script is purely for the succesfully created account scene
{
    public Button QuitApplicationButton;

    void Start()
    {
        SetupButton(QuitApplicationButton, "QuitApplicationButton", OnQuitClicked);
    }

    // Checks the button exists, logs a message if not, and hooks up the click handler if it does.
    // The game keeps running either way.
    private void SetupButton(Button button, string buttonName, UnityEngine.Events.UnityAction onClick)
    {
        if (button == null)
        {
            Debug.Log($"{buttonName} not in scene.");
            return;
        }

        button.onClick.AddListener(onClick);
    }

    private void OnQuitClicked()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    private void LoadScene(string sceneName)
    {
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.Log($"Scene '{sceneName}' not found. Check the name and Build Settings.");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}