using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoors2d : MonoBehaviour
{

    [SerializeField] private Camera cam;
    private GameObject[] walls;
    private GameObject[] doors;
    private List<GameObject> enemylist = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        doors = GameObject.FindGameObjectsWithTag("door2d");
        walls = GameObject.FindGameObjectsWithTag("wall2d");
    }

    public void addToList(GameObject obj)
    {
        if(!enemylist.Contains(obj))
        enemylist.Add(obj);

    }
    public void removeFromList(GameObject obj)
    {

        enemylist.Remove(obj);


        if (enemylist.Count <=0)
        {
            openDoors();
        }

    }



    public void closeDoors()
    {

        foreach (GameObject wall in walls)
        {
            wall.SetActive(true);
        }
        foreach (GameObject door in doors)
        {
            Animator anim = door.gameObject.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetBool("close", true);
                anim.SetBool("open", false);
            }
        }

    }
    public void openDoors()
    {

        foreach (GameObject wall in walls)
        {
            wall.SetActive(false);
        }
        foreach (GameObject door in doors)
        {
            Animator anim = door.gameObject.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetBool("close", false);
                anim.SetBool("open", true);
            }
        }

    }
}
