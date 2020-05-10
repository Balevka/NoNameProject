using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTools
{
    public static TextMesh CreateText(string textValue, float fontSize, Vector3 positon, Color color, TextAnchor textAnchor)
    {
        GameObject text = new GameObject("Text");
        text.transform.position = positon;
        TextMesh textMesh = text.AddComponent<TextMesh>();
        textMesh.text = textValue;
        textMesh.fontSize = 40;
        textMesh.anchor = textAnchor;
        textMesh.color = color;
        text.transform.localScale = new Vector3(fontSize, fontSize, 0);




        return text.GetComponent<TextMesh>();
    }
}
