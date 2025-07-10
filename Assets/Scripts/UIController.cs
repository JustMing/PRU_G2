using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider expSlider;
    [SerializeField] private TMP_Text expText;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    public void UpdateHealthSlider(float current, float max)
    {
        healthSlider.maxValue = max;
        healthSlider.value = Mathf.RoundToInt(current);
        healthText.text = healthSlider.value + "/" + healthSlider.maxValue;
    }

    public void UpdateExpSlider(float current, float max)
    {
        expSlider.maxValue = max;
        expSlider.value = Mathf.RoundToInt(current);
        expText.text = expSlider.value + "/" + expSlider.maxValue;
    }
}
