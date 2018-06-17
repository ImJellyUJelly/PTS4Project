using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine.Networking;
using System.Collections;



public class LoginTest
{

    private GameObject loginScript = new GameObject();
    string returnMessage;

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator ConnectionWebserviceAndBack()
    {
        string username = "Michael";
        string password = "Michael";
        string method = "login";
        yield return null;
        Assert.AreEqual("Welcome Michael.", getRequest("http://localhost:8080/SimpleMavenWebApp/HelloWorld?method=" + method + "&user=" + username + "&pass=" + password));
    }

    public string getRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        uwr.SendWebRequest();
        System.Threading.Thread.Sleep(1000);
        return uwr.downloadHandler.text;
    }
}
