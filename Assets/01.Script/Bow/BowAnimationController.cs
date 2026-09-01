using System;
using UnityEngine;

public class BowAnimationController : MonoBehaviour
{
    [SerializeField] BowShooter _shooter;
    [SerializeField] Animator Animator;
    private void Awake()
    {
        _shooter.OnArrowShot += PlayShootAnimation;
    }

    private void PlayShootAnimation()
    {
        Animator.SetTrigger("Shoot");
    }
}
