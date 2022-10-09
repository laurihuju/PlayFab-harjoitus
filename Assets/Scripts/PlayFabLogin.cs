using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayFabLogin : MonoBehaviour
{
    [SerializeField] private TMP_InputField emailField;
    [SerializeField] private TMP_InputField passwordField;

    [SerializeField] private TextMeshProUGUI errorText;

    public void LogIn()
    {
        if(string.IsNullOrWhiteSpace(emailField.text) || string.IsNullOrWhiteSpace(passwordField.text))
        {
            errorText.text = "Email or password can't be empty!";
            errorText.gameObject.SetActive(true);
            return;
        }

        LoginWithEmailAddressRequest loginRequest = new LoginWithEmailAddressRequest
        {
            Email = emailField.text,
            Password = passwordField.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(loginRequest, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login success");

        SceneManager.LoadScene(1);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        errorText.text = error.ErrorMessage;
        errorText.gameObject.SetActive(true);

        Debug.LogError(error.GenerateErrorReport());
    }
}
