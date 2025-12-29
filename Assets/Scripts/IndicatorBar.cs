using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorBar : MonoBehaviour
{
    [SerializeField] private float maxValueParameter = 100f;
    [SerializeField] private float currentValueParameter;
    private float maxWidth;
    [SerializeField] private Image image;
    
    private RectTransform rectTransform;  // ← Добавлено

    private void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        currentValueParameter = maxValueParameter;
        maxWidth = rectTransform.rect.width;
        UpdateIndicatorBar();

        if( rectTransform == null ) Debug.Log("нет ссылки на индикатор");
    }

    public void FullyRestoreValueParameter()
    {
        currentValueParameter = maxValueParameter;
        UpdateIndicatorBar();
    }

    public void ReduceValueParameter(float amount)
    {
        currentValueParameter -= amount;
        currentValueParameter = Mathf.Clamp(currentValueParameter, 0f, maxValueParameter);
        UpdateIndicatorBar();
    }

    public void SetValueParameter(float amount)
    {
        currentValueParameter = amount;
        currentValueParameter = Mathf.Clamp(currentValueParameter, 0f, maxValueParameter);
        UpdateIndicatorBar();
    }

    public void IncreaseValueParameter(float amount)
    {
        currentValueParameter += amount;
        currentValueParameter = Mathf.Clamp(currentValueParameter, 0f, maxValueParameter);
        UpdateIndicatorBar();
    }

    private void UpdateIndicatorBar()
    {
        float percent = currentValueParameter / maxValueParameter;

        float newWidth = maxWidth * percent;    // Mathf.Round()

        if( rectTransform == null ) rectTransform = GetComponent<RectTransform>();
        
        rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);

        if (percent > 0.75f)
        {
            image.color = Color.green;
        }
        else if (percent > 0.30f)
        {
            image.color = Color.yellow;
        }
        else
        {
            image.color = Color.red;
        }
    }
}



// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class IndicatorBar : MonoBehaviour
// {
//     [SerializeField] private float maxValueParameter = 100f;
//     [SerializeField] private float currentValueParameter;
//     [SerializeField] private Image image;

//     private void Start()
//     {
//         image = GetComponent<Image>();
//         currentValueParameter = maxValueParameter;
//         UpdateIndicatorBar();
//     }
//     public void FullyRestoreValueParameter()
//     {
//         currentValueParameter = maxValueParameter;
//         UpdateIndicatorBar();
//     }

//     public void ReduceValueParameter(float amount)
//     {
//         currentValueParameter -= amount;
//         currentValueParameter = Mathf.Clamp(currentValueParameter, 0f, maxValueParameter); // Ограничиваем здоровье в диапазоне от 0 до maxHealth
//         UpdateIndicatorBar();
//     }

//     public void IncreaseValueParameter(float amount)
//     {
//         currentValueParameter += amount;
//         currentValueParameter = Mathf.Clamp(currentValueParameter, 0f, maxValueParameter); // Ограничиваем здоровье в диапазоне от 0 до maxHealth
//         UpdateIndicatorBar();
//     }

//     private void UpdateIndicatorBar()
//     {
//         float persent = currentValueParameter / maxValueParameter;

//         image.fillAmount = persent;

//         if( persent > 0.75f )
//         {
//             image.color = Color.green;
//         }
//         else if( persent > 0.30f )
//         {
//             image.color = Color.yellow;
//         }
//         else
//         {
//             image.color = Color.red;
//         }
//     }
// }
