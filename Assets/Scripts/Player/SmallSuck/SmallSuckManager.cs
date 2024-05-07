using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SmallSuckManager : MonoBehaviour
{
    //När de blir sucked ska man kalla på creatureBehaviour beingSucked
    //Det kommer finnas 3 olika colliders att hålla koll på, och de kommer att ge olika resultat
    // Start is called before the first frame update
    [SerializeField] private SuckArea _smallSuckArea;
    [SerializeField] private SuckArea _middleSuckArea;
    [SerializeField] private SuckArea _bigSuckArea;
    [SerializeField] private LayerMask _layersToIgnore;
    [SerializeField] private float _startMiniGameRange = 10f;
    [SerializeField] private CreatureValue[] _creatureValues;
    
    [Serializable]
    internal class CreatureValue
    {
        [SerializeField]private CreatureRarity _rarity;
        public CreatureRarity Rarity => _rarity;
        
        [SerializeField]private int _value;
        public int Value => _value;
    }
    
    private bool _inMiniGame;

    ///Frågor:
    /// Vad händer om man slutar mitt i? Ska man kunna göra det? ja, escape
    /// Måste man hålla in LMB hela tiden eller kan man släppa? nej
    /// Info:
    /// Smååå zoner (som effekten)
    /// prioritera den zon som ger mest poäng
    /// när man går in i minigamet ska small suck flyttas till mitten av skärmen och man ska
    /// se lite mer inzoomat på den.
    ///
    private void Update()
    {
        
    }

    private void SuckMiniGame()
    {
        
    }
    

    private void StartMiniGame()
    {
        Debug.Log("Start Mini game");
        _inMiniGame = true;
    }

    public void ExitMiniGame(InputAction.CallbackContext context)
    {
        if (context.started && _inMiniGame)
        {
            Debug.Log("Exit minigame");
            _inMiniGame = false;
        }
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if(!gameObject.activeSelf)return;
        if (context.started && !_inMiniGame)
        {
            Ray ray = Camera.main.ScreenPointToRay (Mouse.current.position.ReadValue());
            if (Physics.Raycast (ray, out RaycastHit hit, _startMiniGameRange, ~_layersToIgnore)) {
                if(hit.transform.CompareTag("Creature"))StartMiniGame();
            }
        }
    }

    
}
