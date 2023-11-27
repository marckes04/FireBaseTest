using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class FireBaseController : MonoBehaviour
{
    public GameObject loginPanel, signupPanel, profilePanel,forgetPasswordPanel,notificationPanel;

    public InputField loginEmail, loginPasword,signUpEmail,signUpPasword,signUpConfirmPaswd, signUpUserName,forgetPassEmail;

    public Text notificationTitleText, notificationMessageText, profileUserNameText, profileUserEmailText;


    public Toggle rememberMe;

    public void OpenLogInPanel()
    {
        loginPanel.SetActive(true);
        signupPanel.SetActive(false);
        profilePanel.SetActive(false);
    }

    public void OpenSignUpPanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(true);
        profilePanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
    }

    public void OpenProfilePanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        profilePanel.SetActive(true);
        forgetPasswordPanel.SetActive(false);
    }

    public void OpenForgetPasswordPanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        profilePanel.SetActive(true);
        forgetPasswordPanel.SetActive(true);
    }

    public void LoginUser()
    {
        if(string.IsNullOrEmpty(loginEmail.text) && string.IsNullOrEmpty(loginPasword.text))
        {
            showNotificationMessage("Error", "Fields empty please fullfil blank spaces");
            return;
        }
    }

    public void SignUpUser()
    {
        if (string.IsNullOrEmpty(signUpEmail.text) && string.IsNullOrEmpty(signUpPasword.text) && string.IsNullOrEmpty(signUpUserName.text)
            && string.IsNullOrEmpty(signUpConfirmPaswd.text))
        {
            showNotificationMessage("Error", "Fields empty please fullfil");
            return;
        }
    }

    public void forgetPassword()
    {
      if(string.IsNullOrEmpty(forgetPassEmail.text))
        {
            showNotificationMessage("Error", "Fields empty please fullfil");
            return;
        }
    }

    private void showNotificationMessage(string title, string message)
    {
        notificationTitleText.text = " " + title;
        notificationMessageText.text = " " + message;

        notificationPanel.SetActive(true);
    }

    public void CloseNotificationPanel()
    {
        notificationTitleText.text = " ";
        notificationMessageText.text = " ";

        notificationPanel.SetActive(false);
    }

    public void LogOut()
    {
        profileUserEmailText.text = " ";
        profileUserNameText.text = " ";
        OpenLogInPanel();
    }

}
