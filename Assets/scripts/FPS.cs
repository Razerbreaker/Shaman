//using UnityEngine;
//using System.Collections;

//public class FPS : MonoBehaviour
//{
//    float deltaTime = 0.0f;

//    void Update()
//    {
//        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
//    }

//    void OnGUI()
//    {
//        int w = Screen.width, h = Screen.height;

//        GUIStyle style = new GUIStyle();

//        Rect rect = new Rect(0, 0, w, h * 2 / 100);
//        style.alignment = TextAnchor.UpperLeft;
//        style.fontSize = h * 3 / 100;
//        style.normal.textColor = new Color(1f, 0.0f, 0.0f, 1.0f);
//        float msec = deltaTime * 1000.0f;
//        float fps = 1.0f / deltaTime;
//        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
//        GUI.Label(rect, text, style);
//    }
//}
using UnityEngine;

public class FPS : MonoBehaviour
{
    GUIStyle style = new GUIStyle();
    int accumulator = 0;
    int counter = 0;
    float timer = 0f;

    void Start()
    {
        style.normal.textColor = Color.white;
        style.fontSize = 32;
        style.fontStyle = FontStyle.Bold;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 34), "FPS: " + counter, style);
    }

    void Update()
    {
        accumulator++;
        timer += Time.deltaTime;

        if (timer >= 1)
        {
            timer = 0;
            counter = accumulator;
            accumulator = 0;
        }
    }
}