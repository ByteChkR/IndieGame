using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(CameraContainer))]
public class EditorExtentions : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CameraContainer myScript = (CameraContainer)target;
        if (GUILayout.Button("SmoothenLine"))
        {
            myScript.SmoothLine();
        }
    }
}
