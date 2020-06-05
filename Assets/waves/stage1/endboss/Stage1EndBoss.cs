using System;
using System.Collections;

using UnityEngine;


public class Stage1EndBoss : EnemyWave
{
    [SerializeField] Movement background;
    public GameObject bossImage;
    [SerializeField] Dialogue preBossFight;
    [SerializeField] Boss theBoss;
    int predialogueNumber = 0;
    public event Action OnNext;


    
    
    public event Action OnDefeat;
    private void Start()
    {
      
    }
    public void Defeated() {
        OnDefeat?.Invoke();
        GameManager.DestoryAllEnemyBullets();
        DestroyAfter(5);
    }

    public override void SpawnWave() {

        StartCoroutine(PreFight());
    }

    IEnumerator PreFight() {
        Movement bg = Instantiate(background, new Vector3(0, 8, 0.9f), Quaternion.identity);
        bossImage = bg.transform.Find("bossImage").gameObject;
        bg.SetDestroyWhenOut(false);
        bg.SetSpeed(new Vector2(0, -0.8f));
        bg.StopMovingAfter(10);
        yield return new WaitForSeconds(12);
        StartCoroutine(StartPreBossFightDialogue());
    }
    IEnumerator StartPreBossFightDialogue() {
        GameManager.dialogueUI.SetActive();
        GetNext();
        yield return new WaitForSeconds(0.5f);
        GameManager.gameInput.OnPressZ += GetNext;
        GameManager.gameInput.OnPressEnter += GetNext;
    }

    void GetNext() {
        if (predialogueNumber < preBossFight.lines.Count)
        {
            GameManager.dialogueUI.ReceiveNewLine(preBossFight.lines[predialogueNumber]);
            predialogueNumber++;
        }
        else { 
        
            GameManager.dialogueUI.SetInactive();
            GameManager.gameInput.OnPressZ -= GetNext;
            GameManager.gameInput.OnPressEnter -= GetNext;
            StartBossFight();
          
        }
        
    }

    void StartBossFight() {
        Vector2 initialPosition = new Vector2(bossImage.transform.position.x, bossImage.transform.position.y);

        Boss boss = Instantiate(theBoss, initialPosition, Quaternion.identity);
        bossImage.SetActive(false);
        boss.movement.SetSpeed(new Vector2(0, -1));
    
    }
    

}
