using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMove : MovingCharacters
{
    [SerializeField] private Transform player;
    [SerializeField] private float nextMoveTime = 2f;
    [SerializeField] private LayerMask playerMask;
    private float distanceV, distanceH;
    float timer;

    [SerializeField] private Camera camera1;
    private Renderer renderer1;


    // Start is called before the first frame update
    void Start()
    {
        renderer1 = GetComponentInChildren<Renderer>();
        timer = nextMoveTime;
    }
    
    // Update is called once per frame
    void Update()
    {

        gridMovement();

        distanceH = (movePoint.localPosition.x - player.position.x);
        distanceV = (movePoint.localPosition.y - player.position.y);


        //verifica se vc esta na mesma sala que o inimigo
        if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera1), renderer1.bounds))
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
               // Debug.Log("player detectado");

                moveTowards();

                timer = nextMoveTime;
            }

        }
        
    }
    
    void attack()
    {
        Debug.Log("fun de ataque iniciada");
    }
    void moveTowards()
    {
       // Debug.Log("fun iniciada");

        if (Mathf.Abs(distanceH)< Mathf.Abs(distanceV))
        {
            //Debug.Log("mover-se para perto do player (vertical)");
            
            if (distanceV < 0)
            {
                
                 walkU();
            }
            else
            {
                walkD();
            }
            
        }
        else
        {
           // Debug.Log("mover-se para perto do player (horizontal)");
            if (distanceH < 0)
            {
                walkR();
            }
            else
            {
                
                walkL();
            }
        }

    }

    public override void walkU()
    {

        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0, 1, 0), 0f, playerMask))
        {
            attack();
        }
        else
        base.walkU();
    }

    public override void walkD()
    {
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0, -1, 0), 0f, playerMask))
        {
            attack();
        }
        else
            base.walkD();
    }
    public override void walkR()
    {
        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(1, 0, 0), 0f, playerMask))
        {
            attack();
        }
        else
            base.walkR();
    }

    public override void walkL()
    {
        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(-1, 0, 0), 0f, playerMask))
        {
            attack();
        }
        else
            base.walkL();
    }

}
