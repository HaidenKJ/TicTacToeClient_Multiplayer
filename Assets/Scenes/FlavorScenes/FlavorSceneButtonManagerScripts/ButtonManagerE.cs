using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonManagerE : MonoBehaviour // This script is purely for the Error404 scene. It's just a button that brings you back to the StartScene.
{
    public Button BackButton;

    void Start()
    {
        SetupButton(BackButton, "BackButton", OnBackClicked);
    }
    private void SetupButton(Button button, string buttonName, UnityEngine.Events.UnityAction onClick)
    {
        if (button == null)
        {
            Debug.Log($"{buttonName} not in scene.");
            return;
        }

        button.onClick.AddListener(onClick);
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

