using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpell : MonoBehaviour
{
    public Spell defaultSpell;
    public Player player;
    public Animator animator;
    public float animationDelay = 0.5f;
    void Start()
    {
        player.ActivateSpell += CastSpell;
    }
    private void OnDestroy()
    {
        player.ActivateSpell -= CastSpell;
    }

    // Update is called once per frame
    void CastSpell() {
        if (PlayerStats.bombCount > 0) {
            PlayerStats.UseBomb();
            Instantiate(defaultSpell, transform.position, Quaternion.identity);
            StartCoroutine(Invul());
        }
        
    }

    IEnumerator Invul() {
        PlayerStats.SetPlayerInvulnerable();
        animator.SetBool("IsBlinking", true);
        yield return new WaitForSeconds(defaultSpell.invulTimer - animationDelay);
        animator.SetBool("IsBlinking", false);
        yield return new WaitForSeconds(animationDelay);
        PlayerStats.SetPlayerVulnerable();
    }
}
