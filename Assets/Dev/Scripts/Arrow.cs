using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    public enum ArrowDirection {UP,DOWN,RIGHT,LEFT}
    public ArrowDirection arrowDirection;
    public bool destroy;
    private RectTransform rectTransform;
    public float speed;
    private Image image;
    public Selfie selfie;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }
    private void FixedUpdate()
    {
        if (destroy)
        {
            rectTransform.anchoredPosition3D += new Vector3(0f, Time.deltaTime * speed,0f);
            
            image.color -= new Color(0f, 0f, 0f, Time.deltaTime);
        }
    }
    public void Destroy()
    {
        image.color = Color.green;
        destroy = true;
        Destroy(gameObject,2f);
    }
}
