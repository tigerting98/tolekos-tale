using System;
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
    public MusicManager music;
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
            SFXPlayer = GetComponent<AudioSource>();
        }
    }
   

    public void PlaySFX(SFX sfx) {
        try
        {
            if (sfx)
            {
                if (!coolDownDictionary.ContainsKey(sfx.id))
                {
                    SFXPlayer.PlayOneShot(sfx.clip, sfx.volume*GameManager.SFXVolume);
                    if (sfx.cooldownTime > 0)
                    {
                        coolDownDictionary.Add(sfx.id, new CoolDown(sfx.cooldownTime));
                    }
                }
            }
        }
        catch (Exception ex) {
            Debug.Log(ex);
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
