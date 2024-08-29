
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Audio 
{
    public AudioClip clip;
    public string name;
    public GameObject sound3D;

    [Range(0,1)]
    public float volume;
    [Range(0.1f,3)]
    public float pitch;
    public bool globalSound;
    public bool loop;

    [HideInInspector]
    public AudioSource source;

}
