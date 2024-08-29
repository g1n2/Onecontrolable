using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

public class GlitchAndCutscene : MonoBehaviour
{

    [SerializeField] private DigitalGlitch glitchEffect;
    [SerializeField] private AnalogGlitch glitchEffect2,glitchEffect3;
    [SerializeField, Range(0,1)]private float intensity;
    [SerializeField] private float speed,amount;
    [SerializeField] private float time;
    [SerializeField] private Camera camera1;
    [SerializeField] private GameObject sprite,face,portal,player2d,player,place;
    [SerializeField] private Material material;
    [SerializeField] private Reset2d reset;
    [SerializeField] private CamSystem camSys;
    private Vector3 originalPos;
    private CloseDoors2d cd;
    private Animator anim;
    private Renderer renderer1;
    private bool glitch = false;
    private float timeElapsed = 0;
    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        renderer1 = GetComponentInChildren<Renderer>();
        anim = GetComponentInChildren<Animator>();
        cd = FindAnyObjectByType<CloseDoors2d>();
        originalPos = sprite.transform.localPosition;
        material.SetColor("_EmissionColor", Color.white);
    }

    // Update is called once per frame
    void Update()
    {
        


        StartCoroutine(shake());

        if (GeometryUtility.TestPlanesAABB
            (GeometryUtility.CalculateFrustumPlanes(camera1), renderer1.bounds))
        {

            FindAnyObjectByType<AudioManager>().stop("Music01");
            FindAnyObjectByType<AudioManager>().stop("Music02");
            cd.addToList(gameObject);
            StartCoroutine(startCutscene());
        }

            
        }

    IEnumerator startCutscene()
    {
        speed = 0;
        amount = 0;
        yield return new WaitForSeconds(2);
        anim.SetTrigger("turn");
        yield return new WaitForSeconds(2);
        glitch = true;
        if (glitch)
        {

            if (timeElapsed < time)
            {
                float t = timeElapsed / time;

                glitchEffect.intensity = Mathf.Lerp(glitchEffect.intensity, intensity, t);
                glitchEffect2.scanLineJitter = Mathf.Lerp(glitchEffect2.scanLineJitter, intensity, t);
                glitchEffect2.verticalJump = Mathf.Lerp(glitchEffect2.verticalJump, intensity, t);
                glitchEffect2.horizontalShake = Mathf.Lerp(glitchEffect2.horizontalShake, intensity, t);

                timeElapsed += Time.deltaTime;
                yield return null;
            }
            if (timeElapsed >= 2.5f)
            {
                player2d.transform.position = place.transform.position;
                player.transform.position = place.transform.position;
                camSys.toGo = place.transform.position;
                StartCoroutine(faceJump());
                glitch = false;
            }

        }

    }

    IEnumerator faceJump()
    {

        if (once)
        {
           
            float t = timeElapsed / time;
            glitchEffect3.verticalJump = Mathf.Lerp(1, 0, t * 4);
            glitchEffect3.scanLineJitter = Mathf.Lerp(1, 0, t * 4);
            glitchEffect3.horizontalShake = Mathf.Lerp(1, 0, t * 4);
            face.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            timeElapsed = 0;
            material.SetColor("_EmissionColor", Color.black);
            yield return new WaitForSeconds(3);

            material.SetColor("_EmissionColor", Color.white);
            portal.SetActive(true);

            resetValues();
            once = false;
            yield return null;
        }
    }
  
    IEnumerator shake()
    {

        sprite.transform.localPosition = new Vector3(
            sprite.transform.localPosition.x + ((Mathf.Sin(Time.time * speed) * amount) * Random.Range(-1, 1)),
            sprite.transform.localPosition.y + ((Mathf.Sin(Time.time * speed) * amount) * Random.Range(-1, 1)),
            0);
        yield return new WaitForSeconds(0.1f);
        sprite.transform.localPosition = originalPos;

    }

    void resetValues()
    {
        glitchEffect.intensity = 0;
        glitchEffect2.scanLineJitter = 0;
        glitchEffect2.verticalJump = 0;
        glitchEffect2.horizontalShake = 0;
        glitchEffect3.verticalJump = 0;
        glitchEffect3.scanLineJitter = 0;
        glitchEffect3.horizontalShake = 0;
        face.SetActive(false);
        reset.isOnTutorial = false;
    }

}
