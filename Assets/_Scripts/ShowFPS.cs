using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    public static float fps;
    GUIStyle style = new GUIStyle();

    private void Start()
    {
        style.normal.textColor = Color.white;
        style.fontSize = 32;
        style.fontStyle = FontStyle.Bold;

        Application.targetFrameRate = 1000;
    }

    void OnGUI()
    {
        fps = 1.0f / Time.deltaTime;
        GUI.Label(new Rect(10, 10, 100, 34), "FPS: " + (int)fps, style);
    }
}
