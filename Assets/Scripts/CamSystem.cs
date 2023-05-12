using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSystem : MonoBehaviour
{
    [SerializeField] [Range(0,1)] private float lerpSpeed;
    [SerializeField] private Transform player;
    [SerializeField] private PlayerMove PM;
    private Vector3 toGo;
    private bool canLerp;
    

    // Start is called before the first frame update
    void Start()
    {
        toGo = new Vector3(transform.position.x, transform.position.y, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if(canLerp)
        transform.position = Vector3.Lerp(transform.position, toGo, lerpSpeed);


        if (player.position.x > toGo.x + 5.1f)
        {   
            StartCoroutine(move());
            moveRight();
        }
        if (player.position.x < toGo.x - 5)
        {
            StartCoroutine(move());
            moveLeft();
        }
        if (player.position.y > toGo.y + 5.1)
        {
            StartCoroutine(move());
            moveUp();
        }
        if (player.position.y < toGo.y - 5)
        {
            StartCoroutine(move());
            moveDown();
        }

        
        if (Input.GetKeyUp(KeyCode.I))
        {
            moveUp();
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            moveLeft();
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            moveDown();
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            moveRight();
        }
        
    }


    IEnumerator move()
    {
       
        canLerp = false;
        PM.canMove = false;
        yield return new WaitForSeconds(lerpSpeed+0.2f);
        canLerp = true;
        yield return new WaitForSeconds(lerpSpeed+0.2f);
        PM.canMove = true;
    }


    public void moveRight()
    {
        toGo = new Vector3 (transform.position.x + 9.6f,transform.position.y,-9.6f);      
    }
    void moveLeft()
    {
        toGo = new Vector3(transform.position.x - 9.6f, transform.position.y, -9.6f);
    }
    void moveUp()
    {
        toGo = new Vector3(transform.position.x, transform.position.y+ 9.6f, -9.6f);
    }
    void moveDown()
    {
        toGo = new Vector3(transform.position.x, transform.position.y- 9.6f, -9.6f);
    }

}
