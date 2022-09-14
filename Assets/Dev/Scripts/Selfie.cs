using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selfie : MonoBehaviour
{
    public RawImage image;
    public RenderTexture renderTexture;
    public RectTransform targetTransform;
    public bool moveToTargetPosition;
    public bool moveToFinalPosition;
    private Vector3 finalPosition;
    private RectTransform rTransform;
    public float speed;
    private Vector3 targetAngles;
    private void Start()
    {
        rTransform = GetComponent<RectTransform>();
        targetAngles = new Vector3(rTransform.localEulerAngles.x, rTransform.localEulerAngles.y, rTransform.localEulerAngles.z + Random.Range(-15f,15f));
        finalPosition = new Vector3(Random.Range(-500f, 0f), Random.Range(-500f, 500f),0f);
   }
    private void FixedUpdate()
    {
        if (moveToTargetPosition)
        {
            //Fotoğraf çekildikten sonra fotoğrafın ekranın sağına gelmesi ve rasgele bi açıda durması için.
            rTransform.anchoredPosition3D = Vector3.Lerp(rTransform.anchoredPosition3D, targetTransform.anchoredPosition3D,Time.deltaTime * speed);
            rTransform.localEulerAngles = Vector3.Lerp(rTransform.localEulerAngles, targetAngles,Time.deltaTime * speed);
        }
        if (moveToFinalPosition)
        {
            //Tüm fotoğraflar çekildikten sonra final pozisyonuna gelmesi ve biraz büyümesi için.
            rTransform.localScale = Vector3.Lerp(rTransform.localScale, new Vector3(1.5f,1.5f,1.5f), Time.deltaTime * speed);
            rTransform.anchoredPosition3D = Vector3.Lerp(rTransform.anchoredPosition3D, finalPosition, Time.deltaTime * speed);

        }
    }
}
