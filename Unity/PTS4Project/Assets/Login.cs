using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{

  public GameObject Username;
  public GameObject Password;

  private string username;
  private string password;
  private string usernamecorrect = "Test";
  private string passwordcorrect = "Test";
  // Use this for initialization
  void Start()
  {

  }

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
    if (username == usernamecorrect && password == passwordcorrect)
    {
      print("Succesful log in. Have fun with DAW");
    }
    else
    {
      Debug.LogWarning("Sorry, try again");
    }
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