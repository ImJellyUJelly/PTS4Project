using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MidiNote : MonoBehaviour {

    public Button button;
    public ColorBlock colors;
    public Color noteColor;
    public int NoteIndex;
    public int NoteOffIndex;
    public int NoteTrack;

    public int position;
    public float duration;
    public midiSequencer sequencer;

   // private bool played = false;
    private Camera mainCam;

    private void Start()
    {
        InvokeRepeating("MakeVisible", 0.0f, 2.5f);

        //played = false;
        mainCam = Camera.main;
    }

    // Use this for initialization
    public void SetColors () {
        colors = button.colors;
	}

    private void Update()
    {
        if (true)
        {
            if (sequencer.sequencer.Position > position - 12 && sequencer.sequencer.Position < position + 12)
            {
                //played = true;
                Click();
            }
        }
    }

    void Click()
    {
        StartCoroutine(changeColorPressed());
    }

    IEnumerator changeColorPressed()
    {
        while (button.colors.normalColor.r != button.colors.pressedColor.r) {
            yield return new WaitForEndOfFrame();
            colors.normalColor = new Color(colors.pressedColor.r, colors.pressedColor.g, colors.pressedColor.b, colors.pressedColor.a);
            button.colors = colors;
        }
        yield return new WaitForSeconds(.1f);
        StartCoroutine(changeColorUnpressed());
    }

    IEnumerator changeColorUnpressed()
    {
        while (button.colors.normalColor.r != button.colors.highlightedColor.r)
        {
            yield return new WaitForEndOfFrame();
            colors.normalColor = new Color(colors.highlightedColor.r, colors.highlightedColor.g, colors.highlightedColor.b, colors.highlightedColor.a);
            button.colors = colors;
        }
    }


    void MakeVisible()
    {
        if (gameObject.activeSelf == false)
        {
            Vector3 pos = mainCam.WorldToViewportPoint(transform.position);
            if (pos.z > 0 && pos.x >= 0.0f && pos.x <= 1.0f && pos.y >= 0.0f && pos.y <= 1.0f)
            {
                gameObject.SetActive(true);
            }
        }
        if (gameObject.activeSelf == true)
        {
            Vector3 pos = mainCam.WorldToViewportPoint(transform.position);
            if ((pos.z > 0 && pos.x >= 0.0f && pos.x <= 1.0f && pos.y >= 0.0f && pos.y <= 1.0f) == false)
            {
                gameObject.SetActive(false);
            }
        }
    }

}
