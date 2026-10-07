using UnityEngine;
using TMPro;

public enum GameState { Login, CreateAccount, LoggedIn }

public class UIManager : MonoBehaviour
{
    public NetworkClient networkClient;

    public GameObject loginPanel, createPanel, loggedInPanel;
    public TMP_InputField loginName, loginPass, createName, createPass;
    public TMP_Text feedbackText;

    void Start()
    {
        ChangeState(GameState.Login);
    }

    public void ChangeState(GameState state)
    {
        loginPanel.SetActive(state == GameState.Login);
        createPanel.SetActive(state == GameState.CreateAccount);
        loggedInPanel.SetActive(state == GameState.LoggedIn);
        feedbackText.text = "";
    }

    // ---- Button functions ----
    public void ShowLogin() { ChangeState(GameState.Login); }
    public void ShowCreate() { ChangeState(GameState.CreateAccount); }

    public void OnLoginContinue()
    {
        if (!IsValid(loginName.text, loginPass.text)) return;
        networkClient.SendMessageToServer(Signifiers.Login + "|" + loginName.text + "|" + loginPass.text);
    }

    public void OnCreateContinue()
    {
        if (!IsValid(createName.text, createPass.text)) return;
        networkClient.SendMessageToServer(Signifiers.CreateAccount + "|" + createName.text + "|" + createPass.text);
    }

    bool IsValid(string name, string pass)
    {
        if (name == "") { feedbackText.text = "Username required"; return false; }
        if (pass == "") { feedbackText.text = "Password required"; return false; }
        if (name.Contains("|") || pass.Contains("|")) { feedbackText.text = "The | character isn't allowed"; return false; }
        return true;
    }

    // ---- Called by NetworkClient when the server replies ----
    public void HandleServerMessage(string msg)
    {
        string[] parts = msg.Split('|');
        int type = int.Parse(parts[0]);
        bool success = parts[1] == "1";
        string text = parts[2];

        if (type == Signifiers.CreateResult)
        {
            if (success)
            {
                ChangeState(GameState.Login);
                feedbackText.text = text;   // set AFTER ChangeState, which clears it
            }
            else feedbackText.text = text;
        }
        else if (type == Signifiers.LoginResult)
        {
            if (success) ChangeState(GameState.LoggedIn);
            else feedbackText.text = text;
        }
    }
}