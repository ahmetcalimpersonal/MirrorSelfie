using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingController : MonoBehaviour
{

    public float swipeThreshold = 50f;
    public float timeThreshold = 0.3f;


    private Vector2 fingerDown;
    private DateTime fingerDownTime;
    private Vector2 fingerUp;
    private DateTime fingerUpTime;
    public Arrow.ArrowDirection direction;
    public BoxStrokeTrigger boxStrokeTrigger;
    public Animator girlAnimator;
    public Animator flashAnimator;
    public Camera takingCamera;
    public RenderTexture defaultRenderTexture;
    public List<Selfie> selfies = new List<Selfie>();
    public static int maxSelfieCount = 4;
    public static int currentSelfieCount;
    private void Start()
    {
        currentSelfieCount = 0;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fingerDown = Input.mousePosition;
            fingerUp = Input.mousePosition;
            fingerDownTime = DateTime.Now;
        }
        if (Input.GetMouseButtonUp(0))
        {
            fingerDown = Input.mousePosition;
            fingerUpTime = DateTime.Now;
            CheckSwipe();
        }
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerDown = touch.position;
                fingerUp = touch.position;
                fingerDownTime = DateTime.Now;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                fingerUpTime = DateTime.Now;
                CheckSwipe();
            }
        }
    }

    private void CheckSwipe()
    {
        //Mouse un deltası ve basılma süresi ile swipe ın yönünü belirliyorum.
        float duration = (float)fingerUpTime.Subtract(fingerDownTime).TotalSeconds;
        if (duration > timeThreshold) return;

        float deltaX = fingerDown.x - fingerUp.x;
        float deltaY = fingerDown.y - fingerUp.y;
        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
        {
            if (Mathf.Abs(deltaX) > swipeThreshold)
            {
                if (deltaX > 0)
                {
                    direction = Arrow.ArrowDirection.RIGHT;
                }
                else if (deltaX < 0)
                {
                    direction = Arrow.ArrowDirection.LEFT;
                }

                Swipe();
            }
        }
        else
        {
            if (Mathf.Abs(deltaY) > swipeThreshold)
            {
                if (deltaY > 0)
                {
                    direction = Arrow.ArrowDirection.UP;
                }
                else if (deltaY < 0)
                {
                    direction = Arrow.ArrowDirection.DOWN;
                }
                Swipe();
            }
        }
      

      
      

        fingerUp = fingerDown;
    }
    private void Swipe()
    {
        if (BoxStrokeTrigger.isHitting)
        {
            if (BoxStrokeTrigger.currentDirection == direction)
            {
                //Ok karenin içerisindeyse ve swipe edilen yön doğruysa
                boxStrokeTrigger.animator.SetTrigger("Correct");
                boxStrokeTrigger.currentArrow.transform.parent = transform;
                boxStrokeTrigger.currentArrow.Destroy();
                Debug.Log("True");
                girlAnimator.SetInteger("ChangePose", currentSelfieCount);
               
                StartCoroutine(TakePhoto());
            }
            else
            {
                //Ok karenin içerisindeyse ama swipe edilen yön doğru değilse
                boxStrokeTrigger.animator.SetTrigger("Wrong");

                Debug.Log("False");
            }
        }
        else
        {
            //Ok karenin içerisinde değilse
            boxStrokeTrigger.animator.SetTrigger("Wrong");
            Debug.Log("False");
        }
       
    }
    IEnumerator TakePhoto()
    {

        //Fotoğraf çekecek kameranın texture ını o anki pozun texture ı yapıyorum ve sonrasında onu defaulta çekiyorum, pozun texture ını resmin RawImage ına veriyorum.
        //Aynanın kamerası ayrı, fotoğraf çeken kamera ayrı, animasyonların pozisyonlanması için de işlemlerin arasına küçük zaman aralıkları yerleştiriyorum.
        yield return new WaitForSeconds(.25f);
        girlAnimator.SetInteger("ChangePose", -1);
        flashAnimator.SetTrigger("TriggerFlash");
        selfies.Add(boxStrokeTrigger.currentArrow.selfie);
        takingCamera.targetTexture = boxStrokeTrigger.currentArrow.selfie.renderTexture;

        yield return new WaitForSeconds(.1f);
        boxStrokeTrigger.currentArrow.selfie.image.texture = (Texture)takingCamera.targetTexture;

        yield return new WaitForSeconds(.1f);
        boxStrokeTrigger.currentArrow.selfie.moveToTargetPosition = true;

        takingCamera.targetTexture = null;

    }

}
