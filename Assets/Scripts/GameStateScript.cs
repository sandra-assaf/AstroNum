using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class GameStateScript : MonoBehaviour
{
    public ButtonWrapperScript shuffleButton;
    public DeckShuffleScript deck;
    public ButtonWrapperScript stopShuffleButton;
    public SelectCardsScript selectedCards;
    private enum GameState
    {
        Started,
        StartShuffling,
        DeckAppear,
        Shuffling,
        StopShuffling,
        DealCards,
        Reveal
    }
    private GameState stateNow;

    void Start()
    {
        Screen.SetResolution(960, 620, true);
        this.shuffleButton.onClick.AddListener(gameStart);
        this.stopShuffleButton.onClick.AddListener(stopShuffle);
        stateNow = GameState.Started;
    }

    void Update()
    {
        switch(stateNow)
        {
            case GameState.DeckAppear:
                stateNow = GameState.Shuffling;
                shuffleButton.gameObject.SetActive(false);
                deck.gameObject.SetActive(true);
                stopShuffleButton.gameObject.SetActive(true);
                break;
            case GameState.StartShuffling:
                if (shuffleButton.isFadedOut == true)
                {
                    stateNow = GameState.DeckAppear;
                }
                    break;
            case GameState.StopShuffling:
                if(deck.transitioning)
                {
                    stopShuffleButton.fadeOut();
                    deck.transitioning = false;
                }
                if(deck.animationsDone)
                {
                    deck.gameObject.SetActive(false);
                    selectedCards.gameObject.SetActive(true);
                    stateNow = GameState.DealCards;
                }
                break;
        }
        
    }

    void gameStart()
    {
        stateNow = GameState.StartShuffling;
        shuffleButton.fadeOut();
    }

    void stopShuffle()
    {
        stateNow = GameState.StopShuffling;
        deck.stopShuffle();
    }
}
