using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Movement3D : MonoBehaviour
{
  private Vector3 moveDirection;

  [SerializeField]
  private CharacterController characterController;

  [SerializeField]
  private float moveSpeed = 5.0f;
  private float rotationSpeed = 100f;

  [SerializeField]
  private float jumpForce = 10f;
  private float gravity = -9.81f;
  // Accumulated rotation angle on the Y-axis
  private float accumulatedRotationAngleY = 0f;

  public string enemyTag = "Enemy";
  public Enemy currentEnemy;
  public List<Enemy> currentEnemies;

  public Transform target;
  // Range within which enemies can be targeted
  public float range = 5.0f;

  void Awake()
  {
    characterController = GetComponent<CharacterController>();
  }

  void Update()
  {
    // Apply gravity if the character is not grounded
    if (characterController.isGrounded == false)
    {
      moveDirection.y += gravity * Time.deltaTime;
    }

    // Move the character based on the direction and speed
    characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
  }

  public void MoveTo(Vector3 direction)
  {
    // Set moveDirection to apply gravity while keeping Y-axis movement
    Vector3 moveDirectionExceptY = new Vector3(direction.x, 0, direction.z).normalized;
    moveDirection = transform.TransformDirection(new Vector3(moveDirectionExceptY.x, moveDirection.y, moveDirectionExceptY.z));

    // Adjust accumulated rotation angle based on left or right arrow key input
    if (Input.GetKey(KeyCode.LeftArrow))
    {
      accumulatedRotationAngleY -= rotationSpeed * Time.deltaTime;
    }
    else if (Input.GetKey(KeyCode.RightArrow))
    {
      accumulatedRotationAngleY += rotationSpeed * Time.deltaTime;
    }

    // Keep angle within the range -360 to 360 degrees
    accumulatedRotationAngleY = ClampAngle(accumulatedRotationAngleY, -360, 360);

    // Convert accumulated Y rotation angle to a quaternion
    Quaternion targetRotation = Quaternion.Euler(0, accumulatedRotationAngleY, 0);

    // Apply rotation to the character smoothly towards the target angle
    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    if (Input.GetKeyDown(KeyCode.Z))
    {
      UpdateTarget();
    }
  }

  private float ClampAngle(float angle, float min, float max)
  {
    if (angle > 360) angle -= 360;
    if (angle < -360) angle += 360;

    return Mathf.Clamp(angle, min, max);
  }

  public void JumpTo()
  {
    if (characterController.isGrounded)
    {
      moveDirection.y = jumpForce;
    }
  }

  void UpdateTarget()
  {
    GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

    float shortestDistance = Mathf.Infinity;

    foreach (GameObject enemy in enemies)
    {
      float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

      // Set the current enemy if it is closer than the previous closest
      if (distanceToEnemy < shortestDistance)
      {
        currentEnemy = enemy.GetComponent<Enemy>();
        currentEnemy.TakeDamage(10.0f);
      }
    }
  }
}
