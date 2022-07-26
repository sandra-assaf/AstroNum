using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Events;
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

    public bool canHover = false;
    public Vector3 zoomPosition;
    public float zoomDuration;
    public float zoomScale;
    public bool zoomAnimationEnabled = false;

    private bool zoomOutAnimation = false;

    private Vector3 initialScale;

    private Vector3 finalZoomScale;

    private float zoomTimer;
    public CardFlipState cardState;

    public UnityEvent detailEvent;

    public enum CardFlipState
    {
        Unflipped,
        FlippedSmall,
        FlippedDetail
    }

    private void Start()
    {
        thisDisplacmentX = UnityEngine.Random.Range(0.1f, floatDisplacementMax);
        thisDisplacmentY = UnityEngine.Random.Range(0.1f, floatDisplacementMax);
        timer = 0;
        initialScale = transform.localScale;
        finalZoomScale = new Vector3(initialScale.x * zoomScale, initialScale.y * zoomScale, initialScale.z);
        cardState = CardFlipState.Unflipped;
        detailEvent = new UnityEvent();
    }

    private void Update()
    {
        if (cardState != CardFlipState.FlippedDetail)
        {
            if (floatingAnimationEnabled)
            {
                if (forward)
                {
                    if (timer < floatAnimationDuration)
                    {
                        timer += Time.deltaTime;
                    }
                    else
                    {
                        timer = floatAnimationDuration;
                        forward = false;
                    }
                }
                else
                {
                    if (timer < 0)
                    {
                        timer = 0;
                        forward = true;
                    }
                    else
                    {
                        timer -= Time.deltaTime;
                    }
                }


                transform.position = new Vector2(initialPosition.x + (Mathf.Sin(timer / floatAnimationDuration) * thisDisplacmentX),
                                                 initialPosition.y + (Mathf.Sin(timer / floatAnimationDuration) * thisDisplacmentY));
            }

            if (zoomAnimationEnabled)
            {
                zoomTimer += Time.deltaTime;
                if (zoomTimer > zoomDuration)
                {
                    zoomTimer = zoomDuration;
                    zoomAnimationEnabled = false;
                }
                scaleProgressively(transform.localScale, finalZoomScale, zoomTimer / zoomDuration);
            }

            else if (zoomOutAnimation)
            {
                zoomTimer += Time.deltaTime;
                if (zoomTimer > zoomDuration)
                {
                    zoomTimer = zoomDuration;
                    zoomOutAnimation = false;
                    floatingAnimationEnabled = true;
                }
                scaleProgressively(transform.localScale, initialScale, zoomTimer / zoomDuration);
            }
        }

    }


    private void scaleProgressively(Vector3 startingScale, Vector3 stopScale,
                                float steps)
    {
        transform.localScale = Vector3.MoveTowards(startingScale, stopScale, steps);
    }

    private void FixedUpdate()
    {
        if(this.animationSum >= 90)
        {
            this.animationSum = 0;
            if (cardFront.active)
            {
                this.animateCard = false;
                cardState = CardFlipState.FlippedSmall;
                //this.GetComponent<BoxCollider2D>().enabled = false;

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

    public void OnMouseEnter()
    {
        if(canHover && cardState != CardFlipState.FlippedDetail)
        {
            zoomTimer = 0;
            zoomAnimationEnabled = true;
        }
    }

    public void OnMouseExit()
    {
        if (canHover && cardState != CardFlipState.FlippedDetail)
        {
            zoomTimer = 0;
            zoomOutAnimation = true;
        }
    }

    public void OnMouseDown()
    {
        switch(cardState)
        {
            case CardFlipState.Unflipped:
                //blurSprite.gameObject.SetActive(true);
                cardFront.GetComponent<SpriteRenderer>().sortingOrder = 2;
                //Color tmp = blurSprite.color;
                //tmp.a = 0.4f;
                //blurSprite.color = tmp;
                this.animateCard = true;
                cardState = CardFlipState.FlippedSmall;
                Debug.Log("clicked");
                break;

            case CardFlipState.FlippedSmall:
                cardState = CardFlipState.FlippedDetail;
                detailEvent.Invoke();
                break;

            case CardFlipState.FlippedDetail:
                cardState = CardFlipState.FlippedSmall;
                break;

        }
        
    }
}
