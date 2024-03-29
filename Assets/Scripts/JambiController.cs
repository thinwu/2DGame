﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JambiController : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    float timerDisplay;
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void disableDialog()
    {
        dialogBox.SetActive(false);
    }
    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
        Invoke("disableDialog", displayTime);
    }
}
