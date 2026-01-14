using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Material onDamageVfxMaterial;
    [SerializeField] private float OnDamageVfxDuration = 0.15f;

    private Material originalMaterial;
    private Coroutine onDamageVfxCoroutine;

    private void Awake() {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
    }

    public void PlayOnDamageVFX() {
        if(onDamageVfxCoroutine != null) 
            StopCoroutine(onDamageVfxCoroutine);

        onDamageVfxCoroutine = StartCoroutine(OnDamageVFXCo());
    }

    private IEnumerator OnDamageVFXCo() {
        sr.material = onDamageVfxMaterial;
        
        yield return new WaitForSeconds(OnDamageVfxDuration);

        sr.material = originalMaterial;
    }
}
