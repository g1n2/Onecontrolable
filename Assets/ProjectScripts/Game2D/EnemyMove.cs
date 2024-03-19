using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMove : MovingCharacters 
{
    [SerializeField] private Transform player;
    [SerializeField] private float nextMoveTimeMAX, nextMoveTimeMIN;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Animator anim;
    [SerializeField] private WalkListener WL;
    private float distanceV, distanceH;
    float timer,nextMoveTime;

    [SerializeField] private Camera camera1;
    private Renderer renderer1;
    private SpriteRenderer spriteSkeleton;
    private charsHP playerHP;

    // Start is called before the first frame update
    void Start()
    {
        playerHP = FindObjectOfType<PlayerHealth>();
        renderer1 = GetComponentInChildren<Renderer>();
        spriteSkeleton= GetComponentInChildren<SpriteRenderer>();
        nextMoveTime = Random.Range(nextMoveTimeMIN, nextMoveTimeMAX);
        timer = nextMoveTime;
        player = GameObject.Find("Player").transform;
        camera1 = GameObject.Find("CamGame").GetComponent<Camera>();
        
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

                checkDir();

                
            }

        }
        
    }

    void attack()
    {
        anim.ResetTrigger("walk");
        timer = nextMoveTimeMAX+1;
        anim.SetTrigger("atk");
    }

    public void damage()
    {
        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0, 1, 0), 0.1f, playerMask)
            || Physics2D.OverlapCircle(movePoint.position + new Vector3(0, -1, 0), 0.1f, playerMask)
            || Physics2D.OverlapCircle(movePoint.position + new Vector3(1, 0, 0), 0.1f, playerMask)
            || Physics2D.OverlapCircle(movePoint.position + new Vector3(-1, 0, 0), 0.1f, playerMask)
            )
        {
           // Debug.Log("i hit :]");
            playerHP.loseHP(1);
        }
        else
            Debug.Log("i missed :[");

    }

    public void checkDir()
    {
 


        if (Mathf.Abs(distanceH)< Mathf.Abs(distanceV))
        {
            //Debug.Log("mover-se para perto do player (vertical)");
            
            if (distanceV < 0)
            {
                
                 checkU();
            }
            else
            {
                checkD();
            }
            
        }
        else
        {
           // Debug.Log("mover-se para perto do player (horizontal)");
            if (distanceH < 0)
            {
                checkR();
            }
            else
            {
                
                checkL();
            }
        }
        

    }


    void checkU()
    {
        //checa acima dele para ver se o player esta na celula
        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0, 1, 0), 0f, playerMask))
        {
            attack();
        }
        //checa acima para ver se outro inimigo esta na celula, caso esteja se movimenta para a esquerda ou direita (dependendo do player)
        else if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0, 1, 0), 0f, stopMove))
        {
            if (distanceH < 0)
            {
                checkR();
            }
            else
            {

                checkL();
            }
        }

        else
        {
            WL.U = true;
            anim.SetTrigger("walk");
            timer = nextMoveTime;
        }

    }

    void checkD()
    {

        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0, -1, 0), 0f, playerMask))
        {
            attack();
        }
        else if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0, -1, 0), 0f, stopMove))
        {
            if (distanceH < 0)
            {
                checkR();
            }
            else
            {

                checkL();
            }
        }
        else
        {
            WL.D = true;
            anim.SetTrigger("walk");
            timer = nextMoveTime;
        }
    }
    void checkR()
    {
        spriteSkeleton.flipX = true;
        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(1, 0, 0), 0f, playerMask))
        {
            attack();
        }
        else if (Physics2D.OverlapCircle(movePoint.position + new Vector3(1, 0, 0), 0f, stopMove))
        {
            if (distanceV < 0)
            {

                checkU();
            }
            else
            {
                checkD();
            }
        }
        else
        {
            WL.R = true;
            anim.SetTrigger("walk");
            timer = nextMoveTime;
        }
    }

    void checkL()
    {
        spriteSkeleton.flipX = false;
        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(-1, 0, 0), 0f, playerMask))
        {
            attack();
        }
        else if (Physics2D.OverlapCircle(movePoint.position + new Vector3(1, 0, 0), 0f, stopMove))
        {
            Debug.Log("check");
            if (distanceV < 0)
            {

                checkU();
            }
            else
            {
                checkD();
            }
        }
        else
        {
            WL.L = true;
            anim.SetTrigger("walk");
            timer = nextMoveTime;
        }
    }

}
