using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

using System.IO;

using iTextSharp.text.pdf;
using iTextSharp.text;

public class midiImageConverter : MonoBehaviour {

    private const string converterPath = "sheet.exe";
    private ProcessStartInfo pInfo;

    public MidiManager mm;
    

	// Use this for initialization
	void Start () {
        pInfo =  new ProcessStartInfo();
        pInfo.CreateNoWindow = true;
        pInfo.UseShellExecute = false;
        pInfo.FileName = converterPath;
        pInfo.WindowStyle = ProcessWindowStyle.Hidden;
        pInfo.Arguments = "";
        

    }
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S))
        {
            UnityEngine.Debug.Log("Trying to print midi sheet.");

            if (mm.path == null)
            {
                UnityEngine.Debug.Log("No song to print");
                return;
            }

            try
            {
                using (Process exe = Process.Start(pInfo))
                {
                    pInfo.Arguments = mm.path + " " + "midi" + "-sheet";
                    exe.WaitForExit();

                    
                    var document = new Document(PageSize.A2);
                    using (var stream = new FileStream(mm.path + "pdf", FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        PdfWriter.GetInstance(document, stream);
                        document.OpenDocument();
                        using (var imageStream = new FileStream(Directory.GetCurrentDirectory() + "\\midi" + "-sheet_1.png", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            var image = Image.GetInstance(imageStream);
                            document.Add(image);
                        }
                        document.Close();
                    }
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
