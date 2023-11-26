using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBaseController : MonoBehaviour
{
    public GameObject loginPanel, signupPanel, profilePanel,forgetPasswordPanel;

    public InputField loginEmail, loginPasword,signUpEmail,signUpPasword,signUpConfirmPaswd, signUpUserName,forgetPassEmail;

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
            return;
        }
    }

    public void SignUpUser()
    {
        if (string.IsNullOrEmpty(signUpEmail.text) && string.IsNullOrEmpty(signUpPasword.text) && string.IsNullOrEmpty(signUpUserName.text)
            && string.IsNullOrEmpty(signUpConfirmPaswd.text))
        {
            return;
        }
    }

    public void forgetPassword()
    {
      if(string.IsNullOrEmpty(forgetPassEmail.text))
        {
            return;
        }
    }

}
