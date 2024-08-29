using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Audio[] sounds;

    private static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

      
        foreach (Audio s in sounds)
        {

            if (s.globalSound)
            {
                s.source = gameObject.AddComponent<AudioSource>();

            }
            else
            {
                if (s.sound3D != null)
                {
                   s.source = s.sound3D.gameObject.AddComponent<AudioSource>();
                    s.source.spatialBlend = 1;

                }
                else
                    Debug.Log("Error: gameobject not found in " + s.name);
            }
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;

        }


    }

    private void Start()
    {
        //FindAnyObjectByType<AudioManager>().play("Music01");
        //FindAnyObjectByType<AudioManager>().play("Music02");
        //FindAnyObjectByType<AudioManager>().muteUnmute("Music02");
    }



    public void play(string name)
    {
        Audio s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        
    }

    public void stop(string name)
    {
        Audio s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
       
    }

    public void muteUnmute(string name)
    {
        Audio s = Array.Find(sounds, sound => sound.name == name);
        s.source.mute = !s.source.mute;

    }

}
