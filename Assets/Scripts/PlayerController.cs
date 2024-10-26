using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField]
  private KeyCode jumpKeyCode = KeyCode.Space;

  private Movement3D movement3D;

  void Awake()
  {
    movement3D = GetComponent<Movement3D>();
  }

  void Update()
  {
    // x, z movement
    float x = Input.GetAxisRaw("Horizontal"); // left/right arrow
    float z = Input.GetAxisRaw("Vertical"); // up/down arrow

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
