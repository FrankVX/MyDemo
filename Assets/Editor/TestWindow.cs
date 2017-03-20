using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.UI;

public class TestWindow : EditorWindow
{
    public static Texture icon;
    static TestWindow window;
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;


    AnimBool m_ShowExtraFields;
    string m_String;
    Color m_Color = Color.white;
    int m_Number = 0;



    [MenuItem("TestWindow/test")]
    public static void Init()
    {
        window = GetWindow<TestWindow>();
        window.maxSize = new Vector2(500, 500);
        window.maximized = true;
        window.position = new Rect(0, 0, 1500, 1300);
        window.ShowPopup();
    }

    void OnEnable()
    {
        m_ShowExtraFields = new AnimBool(true);
        m_ShowExtraFields.valueChanged.AddListener(Repaint);
        //window.titleContent = new GUIContent("TestWindow"); 
    }

    public Rect windowRect = new Rect(200, 200, 320, 350);
    private void OnGUI()
    {
        windowRect = position;
        //Debug.Log(position);
        //GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        //myString = EditorGUILayout.TextField("Text Field", myString);

        //groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        //myBool = EditorGUILayout.Toggle("Toggle", myBool);
        //myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        //EditorGUILayout.EndToggleGroup();

        //m_ShowExtraFields.target = EditorGUILayout.ToggleLeft("Show extra fields", m_ShowExtraFields.target);

        ////Extra block that can be toggled on and off.
        //if (EditorGUILayout.BeginFadeGroup(m_ShowExtraFields.faded))
        //{
        //    EditorGUI.indentLevel++;
        //    EditorGUILayout.PrefixLabel("Color");
        //    m_Color = EditorGUILayout.ColorField(m_Color);
        //    EditorGUILayout.PrefixLabel("Text");
        //    m_String = EditorGUILayout.TextField(m_String);
        //    EditorGUILayout.PrefixLabel("Number");
        //    m_Number = EditorGUILayout.IntSlider(m_Number, 0, 10);
        //    EditorGUI.indentLevel--;
        //}

        //EditorGUILayout.EndFadeGroup();
        //BeginWindows();
        //windowRect = GUILayout.Window(1, windowRect, DoWindow, new GUIContent("subWindow"));
        //EndWindows();
        DrawRightGrid();
    }
    void DoWindow(int unusedWindowID)
    {
        GUILayout.Button("Hi");
        GUI.DragWindow();
    }

    private void DrawRightGrid()
    {
        if (!icon) icon = Resources.Load<Texture>("icon");
        Rect cood = new Rect(0, 0, 10, 10);
        GUI.color = new Color(1f, 1f, 1f, 0.1f);
        GUI.DrawTextureWithTexCoords(new Rect(0, 0, windowRect.width, windowRect.height), icon, cood);
        GUI.color = Color.white;
    }

}
