using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float restoreDefaultMatTime = 0.2f;

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // Initialize spriteRenderer first
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is missing on " + gameObject.name);
            return;
        }

        defaultMat = spriteRenderer.material;
    }

    public float GetRestoreMatTime()
    {
        return restoreDefaultMatTime;
    }

    public IEnumerator FlashRoutine()
    {
        if (spriteRenderer != null && whiteFlashMat != null)
        {
            spriteRenderer.material = whiteFlashMat;
            yield return new WaitForSeconds(restoreDefaultMatTime);
            spriteRenderer.material = defaultMat;
        }
    }
}