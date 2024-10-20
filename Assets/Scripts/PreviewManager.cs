using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class PreviewManager : MonoBehaviour
{
  [SerializeField]
  private float previewYOffset = 0.05f; // ensure preview object to be placed above Ground and GridPlane

  private GameObject previewObject;

  [SerializeField]
  private Material previewMaterialPrefab; // transparent shader
  private Material previewMaterialInstance;

  private void Start()
  {
    previewMaterialInstance = new Material(previewMaterialPrefab);
  }

  public void StartShowingPreview(GameObject prefab)
  {
    // get preview prefab and instantiate
    previewObject = Instantiate(prefab);
    PreparePreview();
  }

  private void PreparePreview()
  {
    // get preview object's renderer and apply preview material prefeb to all of them
    Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();

    foreach (Renderer renderer in renderers)
    {
      Material[] materials = renderer.materials;

      for (int i = 0; i < materials.Length; i++)
      {
        materials[i] = previewMaterialInstance;
      }

      renderer.materials = materials;
    }
  }

  public void UpdatePosition(Vector3 position, bool validity)
  {
    MovePreview(position);
    ChangePreviewColor(validity);
  }

  private void MovePreview(Vector3 position)
  {
    previewObject.transform.position = new Vector3(position.x, position.y + previewYOffset, position.z);
  }

  private void ChangePreviewColor(bool validity)
  {
    Color color = validity ? Color.white : Color.red;
    color.a = 0.5f;

    previewMaterialInstance.color = color;
  }

  public void StopShowingPreview()
  {
    Destroy(previewObject);
  }
}
