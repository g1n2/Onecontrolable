using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerHealth : charsHP
{

    [SerializeField] public GameObject sword;
    private Reset2d reset;
    [HideInInspector] public PlayerMove PM;
    [HideInInspector] public PlayerATK PA;
    [HideInInspector] public CheckButton CB;
    [HideInInspector] public bool once = true;
    private new void Start()
    {
        base.Start();
        reset = FindAnyObjectByType<Reset2d>();
        PM = FindAnyObjectByType<PlayerMove>();
        PA = FindAnyObjectByType<PlayerATK>();
        CB = FindAnyObjectByType<CheckButton>();
    }

    private void FixedUpdate()
    {
        die();
    }

    void die()
    {

       
        if (health <= 0 && once)
        {
            once = false;
            if(anim != null)
            {
                sword.SetActive(false);
                anim.Play("Die");
               // PM.enabled = false;
               // PA.enabled = false;
                CB.isEnabled = false;
            }

        }
    }

    public void destroyChar()
    {
        // GameObject.Destroy(gameObject);
        
        sprRender.enabled = false;
        StartCoroutine(reset.restart2d());
    }

    

}
