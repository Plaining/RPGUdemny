using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Entity entity;
    private CharacterStat myStat;
    private RectTransform rectTransform;
    private Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        entity = GetComponentInParent<Entity>();
        slider = GetComponentInChildren<Slider>();
        myStat = GetComponentInParent<CharacterStat>();

        entity.onFlipped += FlipUI;
        myStat.onHealthChanged += UpdateHealthUI;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = myStat.GetMaxHealthValue();
        slider.value = myStat.currentHealth;
    }

    private void FlipUI() => rectTransform.Rotate(0, 180, 0);
    private void OnDisable()
    {
        entity.onFlipped -= FlipUI;
        myStat.onHealthChanged -= UpdateHealthUI;
    }
}
