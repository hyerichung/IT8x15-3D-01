using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{

  [SerializeField]
  private float moveSpeed = 5.0f;
  [SerializeField]
  private float jumpForce = 3.0f;

  private float gravity = -9.81f;

  private Vector3 moveDirection;
  private CharacterController characterController;

  void Awake()
  {
    characterController = GetComponent<CharacterController>();
  }

  void Update()
  {
    // apply gravity to character
    if (characterController.isGrounded == false)
    {
      moveDirection.y += gravity * Time.deltaTime;
    }

    characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
  }

  public void MoveTo(Vector3 direction)
  {
    moveDirection = new Vector3(direction.x, moveDirection.y, direction.z);
  }

  public void JumpTo()
  {
    if (characterController.isGrounded)
    {
      moveDirection.y = jumpForce;
    }
  }
}
