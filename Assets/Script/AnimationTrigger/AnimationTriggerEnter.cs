using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerEnter : MonoBehaviour
{
    Player player;
    Enemy enemy;
    public void Awake()
    {
        player = GetComponentInParent<Player>();
        enemy = GetComponentInParent<Enemy>();
    }
    public void AnimationFinishTrigger()
    {
        player?.animator.SetBool("Eated", false);
        enemy?.animator.SetBool("IsEaten", false);
    }
}
