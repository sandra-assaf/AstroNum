using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System;

public class CardFlipScript : MonoBehaviour
{
    public GameObject cardFront;
    public GameObject cardBack;

    public float rotationSpeed = 2f;
    private float animationSum = 0;
    private bool animateCard = false;

    private bool floatingAnimationEnabled = false;
    public float floatAnimationDuration;
    public float floatDisplacementMax;
    private bool forward = true;
    private float thisDisplacmentX;
    private float thisDisplacmentY;
    private Vector3 initialPosition;
    private Vector3 displacementVector;
    private float timer;
    public SpriteRenderer blurSprite;

    private void Start()
    {
        thisDisplacmentX = UnityEngine.Random.Range(0.1f, floatDisplacementMax);
        thisDisplacmentY = UnityEngine.Random.Range(0.1f, floatDisplacementMax);
        timer = 0;
    }

    private void Update()
    {
        if(floatingAnimationEnabled)
        {
            if(forward)
            {
                if(timer<floatAnimationDuration)
                {
                    timer += Time.deltaTime;
                } else
                {
                    timer = floatAnimationDuration;
                    forward = false;
                }
            } else
            {
                if(timer < 0)
                {
                    timer = 0;
                    forward = true;
                } else
                {
                    timer -= Time.deltaTime;
                }
            }


            transform.position = new Vector2(initialPosition.x + (Mathf.Sin(timer/floatAnimationDuration) * thisDisplacmentX),
                                             initialPosition.y + (Mathf.Sin(timer/floatAnimationDuration) * thisDisplacmentY));
        }

    }

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

    public void startFloating()
    {
        initialPosition = transform.position;
        floatingAnimationEnabled = true;
    }

    public void OnMouseDown()
    {
        blurSprite.gameObject.SetActive(true);
        cardFront.GetComponent<SpriteRenderer>().sortingOrder = 2;
        Color tmp = blurSprite.color;
        tmp.a = 0.4f;
        blurSprite.color = tmp;
        this.animateCard = true;
        Debug.Log("clicked");
    }
}
