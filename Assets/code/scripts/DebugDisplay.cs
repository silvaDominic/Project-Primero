using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugDisplay : MonoBehaviour {

    public DebugDisplay() {}

    public static Text UItext;

    public static void Log(string logData) {
        UItext.text = logData;
    }
}