using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sanford.Multimedia;

public class initializePiano : MonoBehaviour {

    public List<GameObject> ocataves;

    public Hashtable KeyMap { get; set; }

	// Use this for initialization
	void Start () {
        KeyMap = new Hashtable();

        for(int i = 0; i < ocataves.Count; i++)
        {
            foreach (Transform child in ocataves[i].transform)
            {
                Note note = (Note)Enum.Parse(typeof(Note), child.name);
                int octaveMultiplier = i * 12;

                if (child.name.Contains("Flat"))
                {
                    KeyMap.Add((int)note + octaveMultiplier, child.GetChild(0).gameObject);
                }
                else
                {
                    KeyMap.Add((int)note + octaveMultiplier, child.gameObject);
                }

            }
        }

        foreach (int notenumber in KeyMap.Keys)
        {
            //Debug.Log(notenumber);
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
