using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Image mask;
    public Text bulletCount;
    float originalSize;
    public static HealthBar instance { get; private set; }
    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }
    void Awake()
    {
        instance = this;
    }

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
    public void SetBullet(int value)
    {
        bulletCount.text = "Bullet: " + value;
    }
    // Update is called once per frame
}
