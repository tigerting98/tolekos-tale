using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        animator = animator == null ? GetComponent<Animator>() : animator;
    }

    public void PlayAnimation() 
    {
        if (animator)
        { animator.SetTrigger("Attack"); }
    }

    IEnumerator PlayAnimationAfter( float interval, float start)
    {
        yield return new WaitForSeconds(start);
        yield return Functions.RepeatAction(PlayAnimation, interval);
    }

    IEnumerator PlayAnimationForTimes(float interval, int times)
    {
        

        return Functions.RepeatActionXTimes(PlayAnimation, interval, times);

    }

    public Coroutine PlayAnimationTimes(float interval, int times)
    {
        return StartCoroutine(PlayAnimationForTimes(interval, times));

    }

    public Coroutine PlayAnimationDuration(float interval, float duration)
    {
        return StartCoroutine(PlayAnimationForTimes(interval, (int)(duration / interval) + 1));

    }

    public Coroutine PlayAnimation(float interval, float start)
    {
        return StartCoroutine(PlayAnimationAfter(interval, start));
    }
}
