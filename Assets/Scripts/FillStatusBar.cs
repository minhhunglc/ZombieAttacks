using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStatusBar : MonoBehaviour
{
    public PlayerController playerController;
    public Image fillImage;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        FillBar();
    }

    private void FillBar()
    {
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }
        float fillValue = playerController.currentHeath / playerController.maxHealth;
        if (fillValue <= slider.maxValue / 3)
        {
            fillImage.color = Color.green;
        }
        if (fillValue > slider.maxValue / 3)
        {
            fillImage.color = Color.red;
        }
        slider.value = fillValue;
    }
}
