using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Level Description")]
//Used to set up the text on the between stages menu and set up the reference to the next stage
public class LevelDescription : ScriptableObject
{
    [TextArea(3, 5)]
    public string levelDescription;
    public string nextLevelSceneString;
}
