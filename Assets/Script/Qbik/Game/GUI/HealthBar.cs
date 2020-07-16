using UnityEngine;
using UnityEngine.UI;

namespace Qbik.Game.GameUI.Character
{
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
}
