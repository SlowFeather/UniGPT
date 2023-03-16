using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static Unity.VisualScripting.Member;
using AIHelper;
using System.Linq;

public class AIWindow : EditorWindow
{
    [MenuItem("AI/UniGPT")]
    static void window()
    {
        AIWindow aiWindow = GetWindow<AIWindow>();
        aiWindow.Show();
    }

    //Input
    [TextArea] private string userInput = "Hello";
    [TextArea] private string feedback = "";

    //Scroll
    private Vector2 userInputScroll = Vector2.zero;
    private Vector2 feedbackScroll = Vector2.zero;


    void OnGUI()
    {
        GUILayout.Space(8);

        #region  GUILayout.FlexibleSpace（布局之间左右对齐）
        EditorGUILayout.BeginHorizontal();//开始最外层横向布局
        GUILayout.FlexibleSpace();//布局之间左右对齐
        GUILayout.Label("UniGPT");
        GUILayout.FlexibleSpace();//布局之间左右对齐
        EditorGUILayout.EndHorizontal();
        #endregion

        if (AIHelperSettings.instance.apiKey==null || AIHelperSettings.instance.apiKey == "")
        {
            EditorGUILayout.HelpBox("Error:API Key hasn't been set. Please check the project settings \n (Edit > Project Settings > ChatAI > API Key).", MessageType.Error);//红色错误号
        }

        GUILayout.Label("Ask Your Question:");

        userInputScroll = EditorGUILayout.BeginScrollView(userInputScroll, GUILayout.Height(88));
        userInput=EditorGUILayout.TextArea(userInput, GUILayout.ExpandHeight(true));
        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("Submit"))
        {
            feedback=OpenAIUtil.InvokeChat(userInput.ToString());
            feedback = feedback.Substring(2);
        }

        // Create a label to display program feedback
        GUILayout.Label("Here Is The Answer:");
        //EditorGUILayout.SelectableLabel(feedback, GUILayout.Height(100));

        feedbackScroll = EditorGUILayout.BeginScrollView(feedbackScroll, GUILayout.Height(188));
        feedback = EditorGUILayout.TextArea(feedback, GUILayout.ExpandHeight(true));
        EditorGUILayout.EndScrollView();
    }
}