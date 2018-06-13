using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;



public class LoginTest
{

    private GameObject loginScript = new GameObject();

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator ConnectionWebserviceAndBack()
    {
        string username = "Test";
        string password = "Test";
        string method = "Test";
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
        loginScript.AddComponent<Login>();
        //loginScript = GameObject.Find("Login Button").GetComponent<Login>();
        loginScript.GetComponent<Login>().StartCoroutine(loginScript.GetComponent<Login>().getRequest("http://localhost:8080/SimpleMavenWebApp/HelloWorld?method=" + method + "&user=" + username + "&pass=" + password + ""));
        Assert.AreEqual("True", loginScript.GetComponent<Login>().GetReturnMessage());
    }
}
