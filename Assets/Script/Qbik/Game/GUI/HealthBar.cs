using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Color color = Color.red;

    Slider slider;
    
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.fillRect.GetComponent<Image>().color = color;
    }

    public void ApllyDamage(int adjust)
    {
        slider.value -= adjust;
    }
}
