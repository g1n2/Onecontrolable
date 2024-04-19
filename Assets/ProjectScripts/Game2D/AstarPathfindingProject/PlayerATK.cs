using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerATK : MonoBehaviour
{

    [SerializeField] private GameObject hammer,sword,spear;
    [SerializeField] private float hammerAtkSpeed, hswordAtkSpeed, spearAtkSpeed,atkRange,atkDamage;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private Transform atkPoint;
    private string currentWeapon,direction;
    private Animator anim;
    private float atkTimer;
    private bool isFlipped;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        direction = "Front";
        currentWeapon = "Sword";
    }

    // Update is called once per frame
    void Update()
    {

        if (currentWeapon == "Hammer")
        {
            atkTimer = hammerAtkSpeed;
        }
        else if (currentWeapon == "Spear")
        {
            atkTimer = spearAtkSpeed;
        }
        else
            atkTimer = hswordAtkSpeed;

        /*
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentWeapon = "Sword";
            sword.SetActive(true);
            spear.SetActive(false);
            hammer.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            currentWeapon = "Spear";
            sword.SetActive(false);
            spear.SetActive(true);
            hammer.SetActive(false);
        }else if (Input.GetKeyDown(KeyCode.E))
        {
            currentWeapon = "Hammer";
            sword.SetActive(false);
            spear.SetActive(false);
            hammer.SetActive(true);
        }

#endif
        */
    }

    public IEnumerator Attack()
    {

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Atk" + direction + currentWeapon))
        {
            yield return new WaitForSeconds(0.1f);
            anim.Play("Atk" + direction + currentWeapon);
            hammer.SetActive(false);
            sword.SetActive(false);
            spear.SetActive(false);

            checkEnemy();

            yield return new WaitForSeconds(atkTimer);
            if (currentWeapon == "Hammer")
            {
                hammer.SetActive(true);
            }
            else if (currentWeapon == "Spear")
            {
                spear.SetActive(true);
            }
            else
                sword.SetActive(true);

        }
    }

    void checkEnemy()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(atkPoint.position, atkRange, enemyMask);


        foreach(Collider2D i in hitEnemies)
        {
            //Debug.Log("hitted"+i.name);
            i.GetComponent<EnemyHealth>().loseHP(atkDamage);
        }

        
    }

   

    public void updateDirection(string dir, bool flipped)
    {


        isFlipped = flipped;
        direction = dir;

        if(dir == "Front")
            atkPoint.localPosition = new Vector3(0,-0.6f,0);
        else if (dir == "Back")
            atkPoint.localPosition = new Vector3(0, 1.4f, 0);
        else if(dir == "Side" && flipped)
             atkPoint.localPosition = new Vector3(-1, 0.4f, 0);
        else if(dir == "Side" && !flipped)
            atkPoint.localPosition = new Vector3(1, 0.4f, 0);

    }



}
