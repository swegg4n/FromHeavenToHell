using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    /// <summary>
    /// Animationslägen
    /// </summary>
    private enum AnimationSelector
    {
        Idle,
        Right,
        Left,
        Up,
        Down,
    }

    private Animator animator;

    private Vector2 normalizedDirection;    //riktningen objektet ska vändas mot
    private float koeficient;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Räknar ut vilket håll objektet ska vändas
    /// </summary>
    void Update()
    {
        if (GameManager.instance.Paused == false)
        {
            CheckIfMoving();

            normalizedDirection = GetComponentInChildren<AimIndicator>().Direction.normalized;

            koeficient = normalizedDirection.y / normalizedDirection.x;


            if ((koeficient < 1 && koeficient > -1) && normalizedDirection.x > 0)
            {
                animator.SetInteger("AnimationSelector", (int)AnimationSelector.Right);
            }
            else if ((koeficient < 1 && koeficient > -1) && normalizedDirection.x < 0)
            {
                animator.SetInteger("AnimationSelector", (int)AnimationSelector.Left);
            }
            else if ((koeficient > 1 || koeficient < -1) && normalizedDirection.y > 0)
            {
                animator.SetInteger("AnimationSelector", (int)AnimationSelector.Up);
            }
            else if ((koeficient > 1 || koeficient < -1) && normalizedDirection.y < 0)
            {
                animator.SetInteger("AnimationSelector", (int)AnimationSelector.Down);
            }
            else
            {
                animator.SetInteger("AnimationSelector", (int)AnimationSelector.Idle);
            }
        }
    }

    public void CheckIfMoving()
    {
        if (GetComponent<PlayerMovement>().velocityX != 0 || GetComponent<PlayerMovement>().velocityY != 0)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }
}
