using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMusic : MonoBehaviour
{
    public GameObject MusicGameObject;
   
    AudioSource source;
    AudioSource musicSource;
    // Use this for initialization
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        musicSource = MusicGameObject.GetComponent<AudioSource>();
    }
   
    public void ToggleSpacialBlend()
    {
        if (musicSource.spatialBlend == 1)
        {
            musicSource.spatialBlend = 0;
            Debug.Log("Spatical Blend = 0 (2d)");
        }
        else
        {
            musicSource.spatialBlend = 1;
            Debug.Log("Spatical Blend = 1 (3d)");
        }
    }
}
