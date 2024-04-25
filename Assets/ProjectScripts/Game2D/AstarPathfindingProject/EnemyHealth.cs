using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : charsHP
{
    private CloseDoors2d cd;

    private void Start()
    {
        base.Start();
        cd = FindAnyObjectByType<CloseDoors2d>();

    }

    private void FixedUpdate()
    {

        death();

    }
    void death()
    {
        if (health <= 0)
        {
            if (anim != null)
            {
                anim.Play("Die");
            }

        }
    }

    public void delete()
    {
        sprRender.enabled = false;
        BoxCollider2D box = gameObject.GetComponent<BoxCollider2D>();
        box.enabled = false;
        //Destroy(gameObject);
    }
    
}
