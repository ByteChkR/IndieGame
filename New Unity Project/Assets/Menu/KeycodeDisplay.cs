﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeycodeDisplay : MonoBehaviour {

    public Controller.Interactions interaction;
    public Text keycodeText;
    public delegate void OnRefresh(Controller.Interactions inter);
    public static OnRefresh onRefresh;
    private void Awake()
    {
        onRefresh += OnRefreshKeycode;
    }

    private void Start()
    {

        keycodeText.text = GetKeycodeName(Controller.interactions[(int)interaction]);
    }
    void OnRefreshKeycode(Controller.Interactions inter)
    {
        Debug.Log(interaction);
        if(inter == interaction)
        {
            
            keycodeText.text = GetKeycodeName(Controller.interactions[(int)inter]);
        }
    }


    string GetKeycodeName(KeyCode c)
    {
        string s = c.ToString().ToUpper();

        return s;
    }
}
