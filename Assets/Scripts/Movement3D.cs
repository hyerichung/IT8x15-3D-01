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
    private float accumulatedRotationAngleY = 0f;
    [SerializeField]
    private float groundOffset = 100f;
  
    public string enemyTag = "Enemy";
    public Enemy currentEnemy;
    public List<Enemy> currentEnemies;

    public Transform target;
    public float range = 5.0f;
    void Awake()
  {
    characterController = GetComponent<CharacterController>();
  }

  void Update()
  {
    if (characterController.isGrounded == false)
    {
      // apply gravity to character
      moveDirection.y += gravity * Time.deltaTime;
    }

    characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
  }

  public void MoveTo(Vector3 direction)
  {
    // moveDirection gravity applied for angleY
    Vector3 moveDirectionExceptY = new Vector3(direction.x, 0, direction.z).normalized;
    moveDirection = transform.TransformDirection(new Vector3(moveDirectionExceptY.x, moveDirection.y, moveDirectionExceptY.z));

    // center: 0, left key: -x, right key: +x
    if (Input.GetKey(KeyCode.LeftArrow))
    {
        accumulatedRotationAngleY -= rotationSpeed * Time.deltaTime;
    }
    else if (Input.GetKey(KeyCode.RightArrow))
    {
        accumulatedRotationAngleY += rotationSpeed * Time.deltaTime;
    }

    // keep angle between -360 to 360
    accumulatedRotationAngleY = ClampAngle(accumulatedRotationAngleY, -360, 360);

    // change accumulated angle y to quaternion
    Quaternion targetRotation = Quaternion.Euler(0, accumulatedRotationAngleY, 0);

    // apply angle y for rotate object
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

    // keep angle range min <= angle <= max 
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

            if (distanceToEnemy < shortestDistance)
            {
                currentEnemy = enemy.GetComponent<Enemy>();
                currentEnemy.TakeDamage(10.0f);
            }
        }        
    }
}
