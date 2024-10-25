using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
  public static Laser instance; // 싱글톤
  public LineRenderer lineRenderer;

  private Transform target;
  private Transform firePoint;

  void Awake()
  {
    if (instance != null && instance != this)
    {
      Destroy(gameObject);
      return;
    };

    instance = this;
  }

  void Update()
  {
    if (instance != null && instance != this)
    {
      Destroy(gameObject);
      return;
    };

    lineRenderer.SetPosition(0, firePoint.position);
    lineRenderer.SetPosition(1, target.position + new Vector3(0, target.position.y, 0));
  }

  public static void GenerateLaser(GameObject laserPrefeb, Transform firePoint)
  {
    if (instance == null)
    {
      // 레이저 컴포넌트 생성
      GameObject laserObject = Instantiate(laserPrefeb, firePoint.position, firePoint.rotation);
      Laser laserComponent = laserObject.GetComponent<Laser>();

      if (laserComponent != null)
      {
        laserComponent.firePoint = firePoint;
        instance = laserComponent;
      }
    }
  }

  public void Seek(Transform _target)
  {
    target = _target;
  }

  // TODO: OnDestroy
  public void DestroyLaser()
  {
    target = null;
    instance = null;

  }
}
