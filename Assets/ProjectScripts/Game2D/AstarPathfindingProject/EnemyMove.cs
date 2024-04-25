using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemyMove : MovingCharacters 
{

    [SerializeField] private Transform player;
    [SerializeField] private float nextWaypointDist = 3;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndPath = false;

    private Seeker seeker;


    [SerializeField] private float nextMoveTimeMAX, nextMoveTimeMIN;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Animator anim;
    [SerializeField] private WalkListener WL;
    float timer,nextMoveTime;

    [SerializeField] private Camera camera1;
    private Renderer renderer1;
    private SpriteRenderer spriteSkeleton;
    private PlayerHealth playerHP;
    private EnemyHealth enemyHP;
    private CloseDoors2d cd;

    // Start is called before the first frame update
    void Start()
    {
        enemyHP = GetComponent<EnemyHealth>();
        playerHP = FindObjectOfType<PlayerHealth>();
        cd = FindObjectOfType<CloseDoors2d>();
        renderer1 = GetComponentInChildren<Renderer>();
        spriteSkeleton= GetComponentInChildren<SpriteRenderer>();
        nextMoveTime = Random.Range(nextMoveTimeMIN, nextMoveTimeMAX);
        timer = nextMoveTime;
        player = GameObject.Find("Player").transform;
        camera1 = GameObject.Find("CamGame").GetComponent<Camera>();

        seeker = GetComponent<Seeker>();

        InvokeRepeating("updatePath",0f,nextMoveTime -0.5f);

    }

    // Update is called once per frame
    void Update()
    {

        gridMovement();

        if(enemyHP.health<=0)
            cd.removeFromList(gameObject);

        if (enemyHP.health > 0)
        {
            //verifica se vc esta na mesma sala que o inimigo
            if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera1), renderer1.bounds))
            {

                cd.closeDoors();
                cd.addToList(gameObject);

                if (path == null)
                    return;

                if (currentWaypoint >= path.vectorPath.Count)
                {
                    reachedEndPath = true;
                    return;
                }
                else
                {
                    reachedEndPath = false;
                }

                Vector2 direction = ((Vector3)path.vectorPath[currentWaypoint] - transform.position).normalized;

                float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
                if (distance < nextWaypointDist)
                {
                    currentWaypoint++;
                }


                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = nextMoveTime;
                    // Debug.Log("player detectado");
                    if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
                    {
                        if (direction.y > 0)
                        {
                            checkU();

                        }
                        else
                        {
                            checkD();
                        }
                    }
                    // Verificar se o objeto está indo para a esquerda ou para a direita
                    else
                    {
                        if (direction.x > 0)
                        {
                            checkR();
                        }
                        else
                        {
                            checkL();
                        }



                    }
                }
            }
        }
        
    }

    void updatePath()
    {
        if(seeker.IsDone())
        seeker.StartPath(transform.position, player.position, onPathComplete);
    }

    void updateGraph()
    {

        EnemyMove[] enemies = GameObject.FindObjectsOfType<EnemyMove>(); 

        foreach(EnemyMove i in enemies)
        {

            AstarPath.active.UpdateGraphs(i.gameObject.GetComponent<Collider2D>().bounds);


        }


    }

    void onPathComplete(Path p)
    {

        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
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
            if(playerHP.health>0)
            playerHP.loseHP(1);
        }
        else
            Debug.Log("i missed :[");

    }

    void checkU()
    {

        //checa acima dele para ver se o player esta na celula
        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0, 1, 0), 0f, playerMask))
            attack();
        else
        {
            WL.U = true;
            anim.SetTrigger("walk");
        }
    }
    void checkD()
    {

        //checa acima dele para ver se o player esta na celula
        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0, -1, 0), 0f, playerMask))
            attack();
        else
        {
            WL.D = true;
            anim.SetTrigger("walk");
        }
    }
    void checkL()
    {

        //checa acima dele para ver se o player esta na celula
        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(-1, 0, 0), 0f, playerMask))
            attack();
        else
        {
            WL.L = true;
            anim.SetTrigger("walk");

        }
    }
    void checkR()
    {

        //checa acima dele para ver se o player esta na celula
        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(1, 0, 0), 0f, playerMask))
            attack();
        else
        {
            WL.R = true;
            anim.SetTrigger("walk");
        }
    }





    /*
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
    */
}
