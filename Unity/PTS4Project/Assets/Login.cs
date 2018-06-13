using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Login : MonoBehaviour
{

    public GameObject Username;
    public GameObject Password;
    public GameObject login;
    public GameObject piano;
    private string username;
    private string password;
    private string returnMessage = "hey";
    private string method = "Test";
    // Use this for initialization
    void Start()
    {

    }

    public IEnumerator getRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log(uwr.downloadHandler.text);
            if (uwr.downloadHandler.text == "false")
            {
                Debug.Log("User not registered.");
            }
            else
            {
                returnMessage = uwr.downloadHandler.text;
                Debug.Log(returnMessage);
                StartCoroutine(LoadYourAsyncScene());
                //SceneManager.UnloadSceneAsync("Scene-Login");
                //SceneManager.LoadScene("Piano Test", LoadSceneMode.Additive);
            }

        }
    }

    public string GetReturnMessage()
    {
        return returnMessage;
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the Scene by build //number.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Piano Test");

        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    //post request code voor als we het ooit nodig hebben
    //void Start()
    //{
    //    StartCoroutine(postRequest("localhost:8080/SimpleMavenWebApp/HelloWorld"));
    //}

    //IEnumerator postRequest(string url)
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("Username", usernamecorrect);
    //    form.AddField("Password", passwordcorrect);

    //    UnityWebRequest uwr = UnityWebRequest.Post(url, form);
    //    yield return uwr.SendWebRequest();

    //    if (uwr.isNetworkError)
    //    {
    //        Debug.Log("Error While Sending: " + uwr.error);
    //    }
    //    else
    //    {
    //        Debug.Log("Received: " + uwr.downloadHandler.text);
    //    }
    //}



    public void Loginbutton()
    {
        if (username == "")
        {
            print("Please fill in a Username");
        }
        else if (password == "")
        {
            print("Please fill in a Password");
        }
        else
        {
            Authenticate();
        }
    }

    public void Authenticate()
    {
        StartCoroutine(getRequest("http://localhost:8080/SimpleMavenWebApp/HelloWorld?method=" + method + "&user=" + username + "&pass=" + password + ""));
    }
    // Update is called once per frame
    void Update()
    {
        username = Username.GetComponent<InputField>().text;
        password = Password.GetComponent<InputField>().text;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Username.GetComponent<InputField>().isFocused)
            {
                Password.GetComponent<InputField>().Select();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (password != "")
                {
                    Authenticate();
                }
            }
        }
    }
}