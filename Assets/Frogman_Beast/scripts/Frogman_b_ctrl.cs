using UnityEngine;
using System.Collections;

public class Frogman_b_ctrl : MonoBehaviour
{

  private Animator anim;
  private CharacterController controller;
  private bool battle_state;
  public float speed = 6.0f;
  public float runSpeed = 1.7f;
  public float turnSpeed = 60.0f;
  public float gravity = 20.0f;
  private Vector3 moveDirection = Vector3.zero;

  // Use this for initialization
  void Start()
  {
    anim = GetComponent<Animator>();
    controller = GetComponent<CharacterController>();
  }

  // Update is called once per frame
  void Update()
  {

  }
}

