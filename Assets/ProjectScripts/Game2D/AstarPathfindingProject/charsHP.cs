using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charsHP : MonoBehaviour
{

    [SerializeField] public float health = 3;
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration;
    [HideInInspector] public Animator anim;
    [HideInInspector]public SpriteRenderer sprRender;

    private Material originalMat;
    private Coroutine flashCoroutine;

    public void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        sprRender = gameObject.GetComponentInChildren<SpriteRenderer>();
        originalMat = sprRender.material;
    }

    public void loseHP(float damage)
    {
        if(flashCoroutine != null)
        {
            StopCoroutine(flashRoutine());
        }

        flashCoroutine = StartCoroutine(flashRoutine());
        health -= damage;
       // Debug.Log("vida: " + health);


    }

    public void gainHP(float heal)
    {
        health += heal;
        //Debug.Log("vida: " + health);
    }

    IEnumerator flashRoutine()
    {
        sprRender.material = flashMaterial;
        yield return new WaitForSeconds(duration);
        sprRender.material = originalMat;
        flashCoroutine = null;
    }
    
}
