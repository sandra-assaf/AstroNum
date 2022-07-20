using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlipScript : MonoBehaviour
{
    public GameObject cardFront;
    public GameObject cardBack;

    public float rotationSpeed = 2f;
    private float animationSum = 0;
    private bool animateCard = false;


    private void FixedUpdate()
    {
        if(this.animationSum >= 90)
        {
            this.animationSum = 0;
            if (cardFront.active)
            {
                this.animateCard = false;
                this.GetComponent<BoxCollider2D>().enabled = false;

            } else
            {
                cardFront.SetActive(true);
                cardBack.SetActive(false);
            }
        }

        if (this.animateCard)
        {
            GameObject activeSide = cardFront.active ? cardFront : cardBack;
            int rotationDirection = cardFront.active ? 1 : -1;
            activeSide.gameObject.transform.Rotate(0, rotationDirection * rotationSpeed, 0);
            this.animationSum += rotationSpeed;
        }   
    }

    public void OnMouseDown()
    {
        this.animateCard = true;
        Debug.Log("clicked");
    }
}
