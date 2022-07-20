using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class GameStateScript : MonoBehaviour
{
    public Button shuffleButton;
    private enum GameState
    {
        Started,
        Shuffling,
        Reveal
    }
    private GameState stateNow;

    void Start()
    {
        this.shuffleButton.onClick.AddListener(gameStart);
        stateNow = GameState.Started;
    }

    void Update()
    {
        
    }

    void gameStart()
    {
        stateNow = GameState.Shuffling;
        Debug.Log(stateNow);
    }
}
