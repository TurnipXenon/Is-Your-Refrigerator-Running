using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBar : MonoBehaviour
{
    public TextMeshProUGUI details;
    public Image bar;

    public void UpdateBar(float currentValue, float maxValue)
    {
        details.text = currentValue.ToString() + " / " + maxValue.ToString();
        if (maxValue != 0)
        {
            bar.fillAmount = currentValue / maxValue;
        }
        else
        {
            bar.fillAmount = 0.0f;
        }
    }

    public void UpdateFull()
    {
        details.text = "MAX";
        bar.fillAmount = 1.0f;
    }
}
