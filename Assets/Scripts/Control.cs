using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;
using TMPro;

public class Control : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject thumb;
    public Slider contaminationSlider;
    public Slider forceSlider;
    public Slider proximitySlider;
    public RectTransform panel;
    public float vertical_offset = 40f;
    public TextMeshProUGUI contaminationText;
    public TextMeshProUGUI forceText;
    public TextMeshProUGUI proximityText;
    public float perFrameIncrease = 0.003f;
    bool countup = true;     
    public FmodController fmodController;
    public bool autoC = false;
    public bool autoF = false;    
    public bool autoP = false;
    // Start is called before the first frame update
    void Start()
    {
        contaminationSlider.onValueChanged.AddListener (delegate { sliderChanged();});
        forceSlider.onValueChanged.AddListener (delegate { sliderChanged();});
        proximitySlider.onValueChanged.AddListener (delegate { sliderChanged();});
        contaminationSlider.value = 0.5f;
        forceSlider.value = 0.5f;
        proximitySlider.value = 0.5f;
        sliderChanged();
    }

    void sliderChanged()
    {   
        fmodController.setContamination (contaminationSlider.value);
        contaminationText.text = contaminationSlider.value.ToString("F2");
        fmodController.setForce (forceSlider.value);
        forceText.text = forceSlider.value.ToString("F2");
        fmodController.setProximity (proximitySlider.value);
        proximityText.text = proximitySlider.value.ToString("F2");
    }
    

    // Update is called once per frame
    void Update()
    {
        
        bool p;
        
        fmodController.real.getPaused(out p);
        if (!p)
        {
            if (autoC)
            {
                if (contaminationSlider.value == 1f)
                {
                    countup = false;
                }
                else if (contaminationSlider.value == 0f)
                {
                    countup = true;
                }

                if (countup)
                    contaminationSlider.value += perFrameIncrease;
                else
                    contaminationSlider.value -= perFrameIncrease;
            }

            if (autoF)
            {
                if (forceSlider.value == 1f)
                {
                    countup = false;
                }
                else if (forceSlider.value == 0f)
                {
                    countup = true;
                }

                if (countup)
                    forceSlider.value += perFrameIncrease;
                else
                    forceSlider.value -= perFrameIncrease;
            }

            if (autoP)
            {
                if (proximitySlider.value == 1f)
                {
                    countup = false;
                }
                else if (proximitySlider.value == 0f)
                {
                    countup = true;
                }

                if (countup)
                    proximitySlider.value += perFrameIncrease;
                else
                    proximitySlider.value -= perFrameIncrease;
            }
        }
        
        updateThumb();
    }
    Vector2 convertNormalisedPointToWorldPoint(Vector2 normalisedPoint)
    {
        float width = panel.rect.width * panel.localScale.x;
        float height = panel.rect.height * panel.localScale.y;

        float widthOffset = (Screen.width - width) * 0.5f + panel.localPosition.x;
        float heightOffset = (Screen.height - height) * .5f + panel.localPosition.y;

        Vector2 screenPos = new Vector2 (normalisedPoint.x * width + widthOffset, 
            normalisedPoint.y * height + heightOffset);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        return worldPos;
    }

    void updateThumb()
    {
        Vector2 thumbPosition = getWorldPoint (contaminationSlider.value, forceSlider.value);
        thumb.transform.position = thumbPosition;

        Vector3 scale;
        scale.x = 0.2f + (3.0f * proximitySlider.value);
        scale.y = 0.2f + (3.0f * proximitySlider.value);
        scale.z = 1.0f;
        thumb.transform.localScale = scale;
    }

    Vector2 getWorldPoint(float xValue, float yValue)
    {
        Vector2 normalisedPoint = new Vector2(xValue, yValue);
        Vector2 point = convertNormalisedPointToWorldPoint (normalisedPoint);
        return point;
    }
}
