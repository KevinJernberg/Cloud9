using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckArea : MonoBehaviour
{
    //TODO: Visual is tilted with makes for a weird feeling.
    //Behövs något som håller koll på om en creature befinner sig i denna trigger
    //Behövs något sätt att meddela 
    private GameObject _creatureToLookFor;
    private bool _inArea;
    public bool InArea => _inArea;
    private bool _miniGameStarted;

    private void OnEnable()
    {
        SmallSuckManager.MiniGameStarted += StartMiniGame;
        SmallSuckManager.MiniGameEnded += EndMiniGame;
    }

    private void OnDisable()
    {
        SmallSuckManager.MiniGameStarted -= StartMiniGame;
        SmallSuckManager.MiniGameEnded -= EndMiniGame;
    }


    private void StartMiniGame(GameObject creature)
    {
        _creatureToLookFor = creature;
        _miniGameStarted = true;
    }

    private void EndMiniGame()
    {
        _creatureToLookFor = null;
        _miniGameStarted = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(!_miniGameStarted)return;
        if (other.gameObject == _creatureToLookFor) _inArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(!_miniGameStarted)return;
        if (other.gameObject == _creatureToLookFor) _inArea = false;
        
    }
}