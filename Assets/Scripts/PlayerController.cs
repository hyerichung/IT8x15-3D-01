using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField]
  private KeyCode jumpKeyCode = KeyCode.Space;
  private KeyCode attackKeyCode = KeyCode.Z;
  private KeyCode gettingHitKeyCode = KeyCode.X;
  private KeyCode dieKeyCode = KeyCode.C;

  private Movement3D movement3D;
  private Animator animator;

  void Awake()
  {
    movement3D = GetComponent<Movement3D>();
    animator = GetComponent<Animator>();
  }

  void Update()
  {
    // x, z movement
    float x = Input.GetAxisRaw("Horizontal"); // left/right arrow
    float z = Input.GetAxisRaw("Vertical"); // up/down arrow

    if (z == 0)
    {
      animator.SetBool("isRunning", false);
    }
    else
    {
      animator.SetBool("isRunning", true);
    }

    // Z key -> attack
    if (Input.GetKeyDown(attackKeyCode))
    {
      animator.SetTrigger("isAttacking");
    }

    // X key -> attack
    if (Input.GetKeyDown(gettingHitKeyCode))
    {
      animator.SetTrigger("isGettingHit");
    }

    // C key -> die
    if (Input.GetKeyDown(dieKeyCode))
    {
      animator.SetTrigger("isDied");
    }

    // vector with x, z direction
    Vector3 moveDirection = new Vector3(x, 0, z);

    movement3D.MoveTo(moveDirection);

    // space key -> jump
    if (Input.GetKeyDown(jumpKeyCode))
    {
      movement3D.JumpTo();
    }
  }
}
