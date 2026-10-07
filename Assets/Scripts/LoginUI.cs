using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField nameInput, passInput;
    public TMP_Text feedbackText;

    public static string pendingMessage = "";   // lets Create scene leave a message here

    void Start()
    {
        NetworkClient.Instance.OnServerMessage += HandleServerMessage;
        feedbackText.text = pendingMessage;
        pendingMessage = "";
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
            Signifiers.Login + "|" + nameInput.text + "|" + passInput.text);
    }

    // "Create Account" button
    public void GoToCreateAccount()
    {
        SceneManager.LoadScene("CreateAccountScene");
    }

    void HandleServerMessage(string msg)
    {
        string[] parts = msg.Split('|');
        if (int.Parse(parts[0]) != Signifiers.LoginResult) return;

        if (parts[1] == "1")
            SceneManager.LoadScene("LoginSuccessful");
        else
            feedbackText.text = parts[2];
    }
}