using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(HitboxComponent))]
public class InvincibilityComponent : MonoBehaviour
{
    [SerializeField] private float blinkInterval = 0.7f;
    [SerializeField] private Material blinkMaterial;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    public bool isInvincible = false;
    private Coroutine flashRoutine;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    public void Flash()
    {
        if (isInvincible)
        {
            return;
        }
        
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        isInvincible = true;

        spriteRenderer.material = blinkMaterial;
        yield return new WaitForSeconds(blinkInterval);
        spriteRenderer.material = originalMaterial;
        flashRoutine = null;

        isInvincible = false;
    }
}