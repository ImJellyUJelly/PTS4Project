using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour {

    public string message;
    public ButtonType type;

    [SerializeField]
    private GameObject YesNoGroup;
    [SerializeField]
    private GameObject OKGroup;

    private void Start()
    {
        message = message.Replace(';', '\n');
        GetComponentInChildren<Text>().text = message;

        if(type == ButtonType.OK)
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

}

public enum ButtonType
{
    YesNo,
    OK
}
