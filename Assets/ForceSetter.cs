using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ForceSetter : MonoBehaviour
{
    TextMeshProUGUI text;
    FmodController fmodController;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        fmodController = FindObjectOfType<FmodController>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = fmodController.force.ToString("F2");
    }
}
