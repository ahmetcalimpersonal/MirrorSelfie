using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    public float speed;
    private void FixedUpdate()
    {
        //Okların sola doğru akışı
        GetComponent<RectTransform>().anchoredPosition3D -= new Vector3(Time.deltaTime * speed, 0f,0f);
      
    }
}
