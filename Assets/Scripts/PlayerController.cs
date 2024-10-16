using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField]
  private KeyCode jumpKeyCode = KeyCode.Space;

  [SerializeField]
  private CameraController cameraController;

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

    movement3D.MoveTo(new Vector3(x, 0, z));

    // space key -> jump
    if (Input.GetKeyDown(jumpKeyCode))
    {
      movement3D.JumpTo();
    }

    // mouse movement -> camera angle changes
    float mouseX = Input.GetAxis("Mouse X"); // left/right mouse (left: -1, wait: 0, right: 1)
    float mouseY = Input.GetAxis("Mouse Y"); // up/down mouse (down: -1, wait: 0, up: 1)

    cameraController.RotateTo(mouseX, mouseY);
  }
}
