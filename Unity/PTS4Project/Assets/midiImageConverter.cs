using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class midiImageConverter : MonoBehaviour {

    private const string converterPath = "sheet.exe";
    private ProcessStartInfo pInfo;
    

	// Use this for initialization
	void Start () {
        pInfo =  new ProcessStartInfo();
        pInfo.CreateNoWindow = false;
        pInfo.UseShellExecute = false;
        pInfo.FileName = converterPath;
        pInfo.WindowStyle = ProcessWindowStyle.Hidden;
        pInfo.Arguments = "";

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S))
        {
            UnityEngine.Debug.Log("Trying to print midi sheet");

            try
            {
                using (Process exe = Process.Start(pInfo))
                {
                    pInfo.Arguments = "akachan.mid" + " " + "test";
                    exe.WaitForExit();
                }
            }
            catch (System.Exception e)
            {
                print("Error occured in midiImageConvert::Update\n");
                print(e);
            }
        }
	}


}
