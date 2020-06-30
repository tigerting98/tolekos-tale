using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public class CoolDown {
        public float timer;
        public CoolDown(float time) {
            this.timer = time;
        }
    }
    public static AudioManager current;
    public AudioSource SFXPlayer;
    public Dictionary<string, CoolDown> coolDownDictionary = new Dictionary<string, CoolDown>();
    private void Awake()
    {
        if (current) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
            current = this;
        }
        if (!SFXPlayer) {
            GameObject obj = new GameObject();
            SFXPlayer = obj.AddComponent<AudioSource>();
            DontDestroyOnLoad(obj);
        }
    }

    public void PlaySFX(SFX sfx) {
        if (!coolDownDictionary.ContainsKey(sfx.id))
        { 
            SFXPlayer.PlayOneShot(sfx.clip, sfx.volume);
            if (sfx.cooldownTime > 0)
            { 
                coolDownDictionary.Add(sfx.id, new CoolDown(sfx.cooldownTime)); 
            }
        }
    }
    private void Update()
    {
        List<string> tobeRemoved = new List<string>();
        foreach (string id in coolDownDictionary.Keys) {
            coolDownDictionary[id].timer -= Time.deltaTime;
            if (coolDownDictionary[id].timer <= 0) {
                tobeRemoved.Add(id);
            }
        }
        foreach (string id in tobeRemoved) {
            coolDownDictionary.Remove(id);
        }

        
    }
}
