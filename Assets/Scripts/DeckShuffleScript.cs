using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckShuffleScript : MonoBehaviour
{
    public GameObject card1;
    public GameObject card2;
    public GameObject card3;

    private bool isAnimating = false;
    private bool forward;
    
    public float speedCard1;
    public float speedCard2;
    public float speedCard3;

    private Vector3 initialPositionCard1;
    private Vector3 initialPositionCard2;
    private Vector3 initialPositionCard3;

    private Quaternion initialRotation;

    public Vector3 card1RefPosition;
    public Vector3 card2RefPosition;
    public Vector3 card3RefPosition;

    private Vector3 card1TargetPosition;
    private Vector3 card2TargetPosition;
    private Vector3 card3TargetPosition;

    public float animationDuration;
    public float moveAnimationDuration;
    public float rotateModifier;

    public bool animationsDone = false;
    public bool transitioning = false;

    public Vector3 finalScale = new Vector3(0.35f, 0.35f, 1);
    public Vector3 finalDeckPosition;

    void Start()
    {
        initialPositionCard1 = new Vector3(0,0,-1);
        initialPositionCard2 = new Vector3(0, 0, 0);
        initialPositionCard3 = new Vector3(0, 0, 0);
        initialRotation = card1.transform.rotation;
        forward = true;
        startShuffle();
    }

    void Update()
    {
        
    }

    public void stopShuffle()
    {
        this.isAnimating = !this.isAnimating;
    }

    void startShuffle()
    {
        this.isAnimating = true;
        StartCoroutine(AnimateCard());
    }

    IEnumerator AnimateCard()
    {
        float t = 0f;
        card1TargetPosition = variablePosition(card1RefPosition);
        card2TargetPosition = variablePosition(card2RefPosition);
        card3TargetPosition = variablePosition(card3RefPosition);

        float zRotation1 = Random.Range(0, rotateModifier);
        float zRotation2 = Random.Range(0, rotateModifier);
        float zRotation3 = Random.Range(0, rotateModifier);

        while (t < animationDuration)
        {
            t += Time.deltaTime;

            card1.transform.position = Vector3.Lerp(card1.transform.position, forward? card1TargetPosition : initialPositionCard1, t/speedCard1);
            card2.transform.position = Vector3.Lerp(card2.transform.position, forward ? card2TargetPosition : initialPositionCard2, t/speedCard2);
            card3.transform.position = Vector3.Lerp(card3.transform.position, forward ? card3TargetPosition : initialPositionCard3, t / speedCard3);


            card1.transform.rotation = Quaternion.Lerp(card1.transform.rotation, forward? 
                                                                                        Quaternion.AngleAxis(zRotation1, Vector3.forward) :
                                                                                        initialRotation, 
                                                                                        t / speedCard1);
            card2.transform.rotation = Quaternion.Lerp(card2.transform.rotation, forward ?
                                                                                        Quaternion.AngleAxis(zRotation2, Vector3.forward) :
                                                                                        initialRotation, 
                                                                                        t / speedCard2);
            card3.transform.rotation = Quaternion.Lerp(card3.transform.rotation, forward ?
                                                                                        Quaternion.AngleAxis(zRotation3, Vector3.forward) :
                                                                                        initialRotation,
                                                                                        t / speedCard3);

            yield return null;
        }
        forward = !forward;
        if(this.isAnimating)
        {
            StartCoroutine(AnimateCard());
        } else if (!forward)
        {
            StartCoroutine(AnimateCard());
        } else
        {
            transitioning = true;
            StartCoroutine(MoveToFinalPos());
        }
    }

    IEnumerator MoveToFinalPos()
    {
        float t = 0f;
        _ = Time.deltaTime;
        while (t < moveAnimationDuration)
        {
            t += Time.deltaTime;

            if (t > moveAnimationDuration)
            {
                t = moveAnimationDuration;
            }

            this.transform.position = Vector3.MoveTowards(this.transform.position,
                                                    finalDeckPosition,
                                                    t / moveAnimationDuration);
            this.transform.localScale = Vector3.Lerp(this.transform.localScale,
                                                    finalScale,
                                                    t / moveAnimationDuration);
            yield return null;
        }

        this.animationsDone = true;
    }

    Vector3 variablePosition( Vector3 initialPosition)
    {
        float randomDifference = Random.Range(-2.0f, 2.0f);
        return new Vector3(initialPosition.x + randomDifference, initialPosition.y, initialPosition.z);
    }


}
