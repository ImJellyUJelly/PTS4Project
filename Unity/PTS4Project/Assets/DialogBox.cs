using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour {

    private string message;

    [SerializeField]
    private GameObject YesNoGroup;
    [SerializeField]
    private GameObject OKGroup;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Show("you're mom triple gay;XD", ButtonType.OK);
        }
    }

    public void Show(string msg, ButtonType type)
    {
        message = msg.Replace(';', '\n');
        GetComponentInChildren<Text>().text = message;

        if (type == ButtonType.OK)
        {
            OKGroup.SetActive(true);
            YesNoGroup.SetActive(false);
        }
        else
        {
            OKGroup.SetActive(false);
            YesNoGroup.SetActive(true);
        }

        GetComponent<FadePanel>().visible = true;
    }

    public void DismissOK()
    {
        GetComponent<FadePanel>().visible = false;
        OKGroup.SetActive(false);
        YesNoGroup.SetActive(false);
    }

}

public enum ButtonType
{
    YesNo,
    OK
}
