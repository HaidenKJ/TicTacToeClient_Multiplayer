using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CreateAccountUI : MonoBehaviour
{
    public TMP_InputField nameInput, passInput;
    public TMP_Text feedbackText;

    void Start()
    {
        Application.runInBackground = true;
        NetworkClient.Instance.OnServerMessage += HandleServerMessage;
    }

    void OnDestroy()
    {
        if (NetworkClient.Instance != null)
            NetworkClient.Instance.OnServerMessage -= HandleServerMessage;
    }

    // Continue button
    public void OnContinue()
    {
        if (nameInput.text == "") { feedbackText.text = "Username required"; return; }
        if (passInput.text == "") { feedbackText.text = "Password required"; return; }
        if (nameInput.text.Contains("|") || passInput.text.Contains("|"))
        { feedbackText.text = "The | character isn't allowed"; return; }

        NetworkClient.Instance.SendMessageToServer(
            Signifiers.CreateAccount + "|" + nameInput.text + "|" + passInput.text);
    }

    // Optional Back button
    public void GoToLogin()
    {
        SceneManager.LoadScene("StartScene");
    }

    void HandleServerMessage(string msg)
    {
        string[] parts = msg.Split('|');
        if (int.Parse(parts[0]) != Signifiers.CreateResult) return;

        if (parts[1] == "1")
        {
            LoginUI.pendingMessage = "Account created";
            SceneManager.LoadScene("StartScene");
        }
        else
            feedbackText.text = parts[2];
    }
}