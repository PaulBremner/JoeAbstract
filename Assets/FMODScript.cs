using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODScript : MonoBehaviour
{
    public FMOD.Studio.EventInstance real;
    private FMOD.Studio.PARAMETER_ID contaminationParameterId;
    private FMOD.Studio.PARAMETER_ID forceParameterId;
    private FMOD.Studio.PARAMETER_ID proximityParameterId;

    [FMODUnity.EventRef] public string fmodEvent;

    [SerializeField] [Range(0f, 1f)] private float contamination;
    [SerializeField] [Range(0f, 1f)] private float force;
    [SerializeField] [Range(0f, 1f)] private float proximity;

    // Start is called before the first frame update
    void Start()
    {
        real = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        real.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
