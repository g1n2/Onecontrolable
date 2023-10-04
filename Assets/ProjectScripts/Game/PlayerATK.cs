using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerATK : MonoBehaviour
{

    [SerializeField] private GameObject hammer,sword,spear;
    [SerializeField] private float hammerAtkSpeed, hswordAtkSpeed, spearAtkSpeed;
    private string currentWeapon,direction;
    private Animator anim;
    private float atkTimer;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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

    public void updateDirection(string dir)
    {

        direction = dir;

    }

}
