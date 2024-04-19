using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reset2d : MonoBehaviour
{

    [SerializeField] private GameObject txt,camera;
    [SerializeField]private GameObject player,playerMP;
    private GameObject[] enemies;
    private Vector3 startPos;
    private List<Vector3> enemiesStartPos = new List<Vector3>();
    private PlayerHealth PH;
    private CamSystem CS;
    

    // Start is called before the first frame update
    void Start()
    {
        PH = GameObject.FindObjectOfType<PlayerHealth>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy2d");
        startPos = new Vector3(player.transform.position.x,player.transform.position.y+0.6f,player.transform.position.z);
        CS = FindObjectOfType<CamSystem>();

        foreach (GameObject i in enemies)
        {
            enemiesStartPos.Add( i.transform.position);
        }

    }

    public IEnumerator restart2d()
    {
        CS.enabled = false;
        yield return new WaitForSeconds(1f);
        txt.SetActive(true);
        yield return new WaitForSeconds(1f);
        txt.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        txt.SetActive(true);
        yield return new WaitForSeconds(1f);
        txt.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        txt.SetActive(true);
        yield return new WaitForSeconds(3f);


        for (int i = 0; i<enemies.Length; i++)
        {
            enemies[i].gameObject.transform.position = enemiesStartPos[i];
        }

        txt.SetActive(false);
        PH.sprRender.enabled = true;
        PH.once = true;
        player.transform.position = startPos;
        playerMP.transform.position = startPos;
       // camera.transform.position = new Vector3(startPos.x,startPos.y,-10);
        CS.toGo = new Vector3(startPos.x, startPos.y, -10);
        PH.health = 3;
        PH.PM.enabled = true;
        PH.PA.enabled = true;
        PH.CB.isEnabled = true;
        CS.enabled = true;
        PH.sword.SetActive(true);

    }
    
}
