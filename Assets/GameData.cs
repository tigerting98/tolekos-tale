using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public SpellCardUI spellcardUI;
    private void Awake()
    {
        if (GameManager.gameData == null)
        {
            GameManager.gameData = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else {
            Destroy(this.gameObject);
        }
    }
}
