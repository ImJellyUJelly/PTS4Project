using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonClickTest : MonoBehaviour {

    public Button button;
    public ColorBlock colors;

    public int position;
    public midiSequencer sequencer;

    private bool played = false;

    private void Start()
    {
        played = false;
    }

    // Use this for initialization
    public void SetColors () {
        colors = button.colors;
	}

    private void Update()
    {
        if (!played)
        {
            if (sequencer.sequencer.Position == position)
            {
                played = true;
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
        yield return new WaitForSeconds(.5f);
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

}
