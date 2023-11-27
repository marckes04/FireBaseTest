using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class FireBaseController : MonoBehaviour
{
    public GameObject loginPanel, signupPanel, profilePanel, forgetPasswordPanel, notificationPanel;

    public InputField loginEmail, loginPasword, signUpEmail, signUpPasword, signUpConfirmPaswd, signUpUserName, forgetPassEmail;

    public Text notificationTitleText, notificationMessageText, profileUserNameText, profileUserEmailText;

    public Toggle rememberMe;


    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;


    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                InitializeFirebase();

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }


            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                // Ensure the app is properly initialized
                if (app == null)
                    app = FirebaseApp.Create(new AppOptions
                    {
                        DatabaseUrl = new Uri("https://your-project-id.firebaseio.com/")
                    });
            });


        });
    }

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
        if (string.IsNullOrEmpty(loginEmail.text) && string.IsNullOrEmpty(loginPasword.text))
        {
            showNotificationMessage("Error", "Fields empty please fullfil blank spaces");
            return;
        }
        SignInUser(loginEmail.text, loginPasword.text);
    }

    public void SignUpUser()
    {
        if (string.IsNullOrEmpty(signUpEmail.text) && string.IsNullOrEmpty(signUpPasword.text) && string.IsNullOrEmpty(signUpUserName.text)
            && string.IsNullOrEmpty(signUpConfirmPaswd.text))
        {
            showNotificationMessage("Error", "Fields empty please fullfil");
            return;

            CreateUser(signUpEmail.text, signUpPasword.text,signUpPasword.text);
        }
    }

    public void forgetPassword()
    {
        if (string.IsNullOrEmpty(forgetPassEmail.text))
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


    void CreateUser(string email, string password,string username)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
            UpdateUserProfile(username);
        });

    }

    public void SignInUser(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
            profileUserNameText.text = " " + result.User.DisplayName;
            profileUserEmailText.text = " " + result.User.Email;
            OpenProfilePanel();
        });
    }


    void InitializeFirebase() {
  auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
  auth.StateChanged += AuthStateChanged;
  AuthStateChanged(this, null);
}

void AuthStateChanged(object sender, System.EventArgs eventArgs) {
  if (auth.CurrentUser != user) {
    bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null
        && auth.CurrentUser.IsValid();
    if (!signedIn && user != null) {
      Debug.Log("Signed out " + user.UserId);
    }
    user = auth.CurrentUser;
    if (signedIn) {
      Debug.Log("Signed in " + user.UserId);
    }
  }
}

void OnDestroy() {
  auth.StateChanged -= AuthStateChanged;
  auth = null;
}

    void UpdateUserProfile(string username)
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            {
                DisplayName = username,
                PhotoUrl = new System.Uri("https://via.placeholder.com/150c/0%20https://placeholder.com/"),
            };
            user.UpdateUserProfileAsync(profile).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User profile updated successfully.");

                showNotificationMessage("Alert", "Succesfully created");
            });
        }
    }

}
