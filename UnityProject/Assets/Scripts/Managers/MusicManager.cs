using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    [SerializeField]
    private int approxSecondsToFade = 10;
    [SerializeField]
    private AudioSource audioSource;


    public static MusicManager Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is multiple instance of singleton Music Manager");
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if(audioSource != null)
        {
            if (audioSource.volume < 0.8)
            {
                audioSource.volume = audioSource.volume + (Time.deltaTime / (approxSecondsToFade + 1));
            }
            else
            {
                // Deactivate this script to not waste the Update.
                this.GetComponent<MusicManager>().enabled = false;
            }
        }
        else {
            // Deactivate this script to not waste the Update.
            this.GetComponent<MusicManager>().enabled = false;  
        }
    }
}
