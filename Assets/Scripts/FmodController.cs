using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FmodController : MonoBehaviour
{
    public FMOD.Studio.EventInstance real;
    private FMOD.Studio.PARAMETER_ID contaminationParameterId;
    private FMOD.Studio.PARAMETER_ID forceParameterId;
    private FMOD.Studio.PARAMETER_ID proximityParameterId;

    [FMODUnity.EventRef] public string fomodEvent;

    [SerializeField] [Range(0f, 1f)] public float contamination;
    [SerializeField] [Range(0f, 1f)] public float force;
    [SerializeField] [Range(0f, 1f)] public float proximity;
    public float sigmaSq = 0.05f;
    public float muContamination = 0.5f;
    public float muForce = 0.5f;
    public float muProximity= 0.5f;
    // Start is called before the first frame update
    void Start()
    {        
        Initialise();
    }

    public void Initialise()
    {
        while (!FMODUnity.RuntimeManager.HasBankLoaded("Master")) ;
        //Debug.Log("bank loaded " + FMODUnity.RuntimeManager.HasBankLoaded("Master"));
        real = FMODUnity.RuntimeManager.CreateInstance(fomodEvent);
        real.start();
        real.setPaused(true);
        FMOD.Studio.EventDescription realDescription;
        FMOD.Studio.PARAMETER_DESCRIPTION pd;

        realDescription = FMODUnity.RuntimeManager.GetEventDescription(fomodEvent);
        realDescription.getParameterDescriptionByName("contamination", out pd);
        int count = 0;
        realDescription.getParameterDescriptionCount(out count);
        Debug.Log("count " + count);
        //realDescription.getParameterDescriptionByIndex(1, out pd);
        contaminationParameterId = pd.id;

        realDescription.getParameterDescriptionByName("force", out pd);
        forceParameterId = pd.id;

        realDescription.getParameterDescriptionByName("proximity", out pd);
        proximityParameterId = pd.id;

        real.setParameterByName("contamination", 0.5f);// contamination);
        real.setParameterByName("force", 0.5f);
        real.setParameterByName("proximity", 0.5f);//proximity);        
    }

    public void Pause()
    {
        real.setPaused(true);
    }

    public void Resume()
    {
        real.setPaused(false);
    }

    // Update is called once per frame

    public void setContamination(float newContamination)
    {
        float contaminationConverted = ConvertToGaussian(newContamination, sigmaSq, muContamination);
        contaminationConverted /= ConvertToGaussian(muContamination, sigmaSq, muContamination);//normalise
        //Debug.Log("Result = " + contaminationConverted);
        contamination = contaminationConverted;//newContamination;// test replacing with convertedcontamination
        //Debug.Log("Con Conv " + contamination);
        real.setParameterByID (contaminationParameterId, contamination);
    }
    public void setForce(float newForce)
    {
        float forceConverted = ConvertToGaussian(newForce, sigmaSq, muForce);
        forceConverted /= ConvertToGaussian(muForce, sigmaSq, muForce);//normalise
        //Debug.Log("Result = " + contaminationConverted);
        force =  forceConverted;//newForce;//test replacing with convertedcontamination
        real.setParameterByID (forceParameterId, force);
    }
    public void setProximity(float newProximity)
    {
        float proximityConverted = ConvertToGaussian(newProximity, sigmaSq, muProximity);
        proximityConverted /= ConvertToGaussian(muProximity, sigmaSq, muProximity);//normalise
        //Debug.Log("Result = " + contaminationConverted);
        proximity = proximityConverted;//newProximity;// test replacing with convertedcontamination        
        real.setParameterByID (proximityParameterId, proximity);
    }

    float ConvertToGaussian(float sliderValue, float sigmaSquared, float mu)
    {
        //(1 / sqrt(5) * sqrt(2 * pi)) * exp(-((x - 0) * *2) / (2 * 5))
        
        float result = ( 1/( Mathf.Sqrt(sigmaSquared)*Mathf.Sqrt(2*Mathf.PI) ) ) * Mathf.Exp(-(Mathf.Pow(sliderValue - mu,2)) / (2 * sigmaSquared));
        
        

        return result;
    }
}
