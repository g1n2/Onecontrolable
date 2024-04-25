using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

public class GlitchAndCutscene : MonoBehaviour
{

    [SerializeField] private DigitalGlitch glitchEffect;
    [SerializeField] private AnalogGlitch glitchEffect2;
    [SerializeField] [Range(0,1)]private float intensity;
    [SerializeField] private float time;
    private bool glitch = false;
    private float timeElapsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.N))
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
            }


        }
    }



}
