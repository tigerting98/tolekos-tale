using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Line {
    public bool left;
    public Sprite speaker;
    public bool isPlayer;
    [TextArea(2,5)]
    public string text;
    public string name;

}
[CreateAssetMenu(fileName = "New Conversation")]
public class Dialogue : ScriptableObject
{
    public List<Line> lines;

  

}
