using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeAudio : MonoBehaviour
{    
    FMOD.Studio.EventInstance instance;
    FmodController fmodController;
    // Start is called before the first frame update
    void Start()
    {
        /*
        var result = FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
        Debug.Log(result);
        */
        fmodController = FindObjectOfType<FmodController>();
        //instance = fmodController.real;
        //instance.setPaused(true);
        //fmodController.Pause();
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool audioResumed = false;
    bool started = false;

    public void ResumeAudioClick()
    {        
        if (!audioResumed)
        {
            if(!started)
            {
                FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
                FMODUnity.RuntimeManager.CoreSystem.mixerResume();
                started = true;
            }

            /*
            var result = FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
            Debug.Log(result);
            result = FMODUnity.RuntimeManager.CoreSystem.mixerResume();
            Debug.Log(result);
            */
            /*
            if (!started)
                fmodController.Initialise();
            else
            */
            fmodController.Resume();
            //instance.setPaused(false);
            //instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
           
            audioResumed = true;    
        }
        else
        {
            /*
            var result = FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
            Debug.Log(result);    
            */
            audioResumed = false;            
            //instance.setPaused(true);
            fmodController.Pause();
            //Debug.Log("pause");
        }
    }

}
