using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;
using UnityEngine.UI;

public class Reset2d : MonoBehaviour
{

    [SerializeField] private GameObject txt,camera;
    [SerializeField]private GameObject player,playerMP;
    [SerializeField] private DigitalGlitch glitchEffect;
    [SerializeField] private AnalogGlitch glitchEffect2,mainCamGlitch;
    [SerializeField] private EnemyHealth tutorialEnemy;
    [SerializeField] private AudioClip audio;
    private Audio audioClass;
    private GameObject[] enemies;
    private Vector3 startPos;
    private List<Vector3> enemiesStartPos = new List<Vector3>();
    private PlayerHealth PH;
    private CamSystem CS;
    private bool isOnTutorial = true;

    // Start is called before the first frame update
    void Start()
    {
        PH = GameObject.FindObjectOfType<PlayerHealth>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy2d");
        startPos = new Vector3(player.transform.position.x,player.transform.position.y+0.6f,player.transform.position.z);
        CS = FindObjectOfType<CamSystem>();
        audioClass = FindAnyObjectByType<Audio>();

        foreach (GameObject i in enemies)
        {
            enemiesStartPos.Add( i.transform.position);
        }

    }

 

    public IEnumerator restart2d()
    {
        if (!isOnTutorial)
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


            for (int i = 0; i < enemies.Length; i++)
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
        else
        {
            audioClass.playAudio(audio);
            txt.SetActive(true);
            txt.GetComponent<TextMesh>().text = "NOT NOW";
            txt.GetComponent<TextMesh>().color = new Vector4(0,0,0,255);
            glitchEffect.intensity = 0.3f;
            glitchEffect2.scanLineJitter = 0.3f;
            glitchEffect2.verticalJump = 0.3f;
            glitchEffect2.horizontalShake = 0.3f;
            mainCamGlitch.scanLineJitter = 0.3f;
            mainCamGlitch.colorDrift = 0.3f;
            tutorialEnemy.health = 0;

            yield return new WaitForSeconds(1f);

            PH.health = 3;
            PH.PM.enabled = true;
            PH.PA.enabled = true;
            PH.CB.isEnabled = true;
            CS.enabled = true;
            PH.sword.SetActive(true);
            PH.sprRender.enabled = true;
            PH.once = true;
            txt.GetComponent<TextMesh>().text = "YOU DIED";
            txt.GetComponent<TextMesh>().color = new Vector4(255,255,255,255);
            txt.SetActive(false);
            glitchEffect.intensity = 0f;
            glitchEffect2.scanLineJitter = 0f;
            glitchEffect2.verticalJump = 0f;
            glitchEffect2.horizontalShake = 0f;
            mainCamGlitch.scanLineJitter = 0f;
            mainCamGlitch.colorDrift = 0f;
        }

    }
    
}
