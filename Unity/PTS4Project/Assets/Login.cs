using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{

    public GameObject Username;
    public GameObject Password;
    private string username;
    private string password;
    private string method = "login";
    // Use this for initialization
    void Start()
    {

    }

    IEnumerator getRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            if (uwr.downloadHandler.text == "false")
            {
                Debug.Log("User not registered.");
            }
            else
            {
                //Debug.Log(uwr.downloadHandler.text);
                SceneManager.LoadScene("Piano Test", LoadSceneMode.Additive);
            }
            
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
        StartCoroutine(getRequest("http://localhost:8080/SimpleMavenWebApp/HelloWorld?method="+ method +"&user="+ username +"&pass="+ password +""));

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