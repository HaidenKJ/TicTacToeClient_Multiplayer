using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManagerA : MonoBehaviour // This script is purely for the AccountCreated scene.
{
    public Button ContinueButton;
    public Button BackButton;

    void Start()
    {
        SetupButton(ContinueButton, "ContinueButton", OnContinueClicked);
        SetupButton(BackButton, "BackButton", OnBackClicked);
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

    private void OnContinueClicked()
    {
        Debug.Log("Continue button clicked, but nothing happened");
    }

    private void OnBackClicked()
    {
        LoadScene("StartScene");
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