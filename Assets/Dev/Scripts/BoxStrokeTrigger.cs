using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxStrokeTrigger : MonoBehaviour
{
    public static bool isHitting;
    public static Arrow.ArrowDirection currentDirection;
    public Animator animator;
    public Arrow currentArrow;
    public TakingController takingController;
    public GameManager gameManager;
    private void Start()
    {
        isHitting = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Arrow>())
        {
            isHitting = true;
            currentArrow = collision.gameObject.GetComponent<Arrow>();
            currentDirection = currentArrow.arrowDirection;

            //Debug.Log("Is hitting an arrow, direction is " + currentDirection);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Arrow>())
        {
            isHitting = false;
            TakingController.currentSelfieCount++;
            if (TakingController.currentSelfieCount == TakingController.maxSelfieCount)
            {
                foreach (var item in takingController.selfies)
                {
                    item.moveToFinalPosition = true;

                }
                gameManager.Complete();
            }
        }
    }
}
