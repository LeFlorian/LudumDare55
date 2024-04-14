using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances; 
    public static AudioManager instance { get; private set; }

    private EventInstance musicEventInstance; 
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one AudioManager in the scene.");
        }
        instance = this;
        eventInstances = new List<EventInstance>();
    }

    private void Start()
    {
        initializeMusic(FMODEvents.instance.music);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance createInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance; 
    }

    private void initializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = createInstance(musicEventReference);
        musicEventInstance.start();
    }
    private void cleanUp()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    private void OnDestroy()
    {
        cleanUp();
    }
}
