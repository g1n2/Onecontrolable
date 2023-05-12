using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUp : MonoBehaviour
{
    [SerializeField] private float shootSpeed;
    [SerializeField] private GameObject areaCircle;
    [SerializeField] private LayerMask layerbutt;
    public bool attached;
    private bool isOnArea,picked,pressed;
    private float rotSpeed,pressedTimer;
    private Rigidbody2D rb;
    private BoxCollider2D collision;
    private Animator anim;
    private CheckButton CB;
    private SpriteRenderer sprR;

    private bool beingClicked;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collision = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        CB = FindObjectOfType<CheckButton>();
        sprR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) {

           
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero,float.MaxValue,layerbutt);

            //Debug.Log(hit.collider.gameObject.name);
            if (hit.collider != null && hit.collider.gameObject.name == gameObject.name)
            {         
                pressButt();
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            
                unpressButt();
            
        }


        if (pressed && attached)
        {
            pressedTimer += Time.deltaTime;
            beingClicked = true;
        }
        else
        {
            pressedTimer = 0;
            beingClicked = false;
        }
       

        
        if (picked && !attached)
        {                  
            transform.position = mousePos;
        }

        if (!attached) {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -7.5f, 7.5f),
                Mathf.Clamp(transform.position.y, -4.5f, 6f),
                0);
            anim.Play("Idle");
        }

  

        if(attached && !picked)        
        {
  

            transform.localPosition = Vector2.Lerp(
                transform.localPosition,
                new Vector2(areaCircle.transform.localPosition.x, areaCircle.transform.localPosition.y),
                0.05f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,0), 0.1f);

            GetComponent<Rigidbody2D>().gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
            collision.isTrigger = true;
            anim.Play("Idle");
        }

        if (attached && picked)
        {

            transform.position = mousePos;

            transform.position = new Vector3(
           Mathf.Clamp(transform.position.x, areaCircle.transform.position.x-0.15f, areaCircle.transform.position.x+ 0.15f),
           Mathf.Clamp(transform.position.y, areaCircle.transform.position.y- 0.15f, areaCircle.transform.position.y + 0.15f),
           0);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.05f);

            if (Vector2.Distance(transform.position, mousePos) > 2)
            {
                float intensity = Vector2.Distance(transform.position, mousePos)  ;
                StartCoroutine(Shake(intensity));
                
               
            }
           

            if (Vector2.Distance(transform.position, mousePos) > 3)
            {
                Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                picked = false;
                attached = false;
                collision.isTrigger = false;
                CB.occupied = false;
                rb.velocity = direction * shootSpeed;
                sprR.sortingOrder = 0;
                CB.occupant = null;
            }

            if (transform.position.y > areaCircle.transform.position.y+0.1)
            {
                anim.Play("Up");
            }
            else if (transform.position.y < areaCircle.transform.position.y-0.1)
            {
                anim.Play("Down");
            }
            else if (transform.position.x > areaCircle.transform.position.x+0.1)
            {
                anim.Play("Right");
            }
            else if (transform.position.x < areaCircle.transform.position.x-0.1)
            {
                anim.Play("Left");
            }
            else
            {
                anim.Play("Idle");
            }

        }

        if (pressedTimer > 0.3f && attached )
        {         
            picked = true;
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
       


    }

 

    IEnumerator walk()
    {
        //Debug.Log("walk");
        CB.walk();
        yield return new WaitForSeconds(1f);
        beingClicked = false;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("area"))
        {
            isOnArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("area"))
        {
            isOnArea = false;
        }
    }

    
   

    IEnumerator Shake(float intensity)
    {

        transform.Rotate(new Vector3(0, 0, Random.Range(-2*intensity,2*intensity)));
        yield return new WaitForSeconds(0.05f);
        transform.Rotate(new Vector3(0, 0, 0));
      
    }


    private void pressButt()
    {

        pressed = true;
        if (!attached)
        {

            picked = true;
            rotSpeed = Random.Range(-15, 15);
            GetComponent<Rigidbody2D>().gravityScale = 1;
            transform.Rotate(new Vector3(0, 0, rotSpeed));


        }

    }


    private void unpressButt()
    {
        pressed = false;
        if (picked)
        {
            picked = false;
            if (CB.occupied == false)
            {
                if (!isOnArea)
                {
                    rb.velocity = new Vector2(Random.Range(-3, 3), 3);
                }
                else
                {
                    attached = true;
                    CB.occupied = true;
                    sprR.sortingOrder = -1;
                    rb.angularVelocity = 0;
                }
            }
            else
            {
                rb.velocity = new Vector2(Random.Range(-3, 3), 3);
            }
        }

        if (pressedTimer < 0.3f && beingClicked && attached)
        {
            StartCoroutine(walk());
        }

    }


}



