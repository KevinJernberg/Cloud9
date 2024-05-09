using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SmallSuckManager : MonoBehaviour
{
    public static UnityAction<GameObject> MiniGameStarted;

    public static UnityAction MiniGameEnded;
    //När de blir sucked ska man kalla på creatureBehaviour beingSucked
    //Det kommer finnas 3 olika colliders att hålla koll på, och de kommer att ge olika resultat
    // Start is called before the first frame update
    [SerializeField] private SuckArea _smallSuckArea;
    [SerializeField] private SuckArea _middleSuckArea;
    [SerializeField] private SuckArea _bigSuckArea;
    [SerializeField] private LayerMask _layersToIgnore;
    [SerializeField] private float _startMiniGameRange = 10f;
    [SerializeField] private CreatureValue[] _creatureValues;
    [SerializeField] private Vector3 _smallSuckMinigamePos;
    [SerializeField] private float _suckForce;
    [SerializeField] private Transform _nozzlePosition;
    [SerializeField] private float _suckNozzleSize = 0.1f;
    [SerializeField] private float _creatureSpinSpeed = 10;
    [SerializeField] private int _smallAreaPoints = 3;
    [SerializeField] private int _middleAreaPoints = 2;
    [SerializeField] private int _bigAreaPoints = 1;
    [SerializeField] private int _outsideOfAreaPoints = -4;
    
    [Serializable]
    internal class CreatureValue
    {
        [SerializeField]private CreatureBehaviour.CreatureRarity _rarity;
        public CreatureBehaviour.CreatureRarity Rarity => _rarity;
        
        [SerializeField]private int _value;
        public int Value => _value;
    }
    
    private bool _inMiniGame;
    private Vector3 _smallSuckOriginalPos;
    private Rigidbody _creatureToBeSucked;
    private float _nozzleOffset = 0.7f;
    private int points;

    private void Awake()
    {
        _smallSuckOriginalPos = transform.localPosition;
    }

    ///Frågor:
    /// Vad händer om man slutar mitt i? Ska man kunna göra det? ja, escape
    /// Måste man hålla in LMB hela tiden eller kan man släppa? nej
    /// Info:
    /// Smååå zoner (som effekten)
    /// prioritera den zon som ger mest poäng
    /// när man går in i minigamet ska small suck flyttas till mitten av skärmen och man ska
    /// se lite mer inzoomat på den.
    ///
    private void FixedUpdate()
    {
        if (_inMiniGame)SuckMiniGame();
    }

    private void SuckMiniGame()
    {
        SpinCreature();
        if(points >= 100)WinMiniGame();
        else if(points <= 0)LoseMiniGame();
        points += CheckWhatArea();
    }

    private int CheckWhatArea()
    {
        if (_smallSuckArea.InArea)
        {
            return _smallAreaPoints;
        }
        if (_middleSuckArea.InArea)
        {
            return _middleAreaPoints;
        }

        if (_bigSuckArea.InArea)
        {
            return _bigAreaPoints;
        }

        return _outsideOfAreaPoints;
    }

    private void StartMiniGame()
    {
        Debug.Log("Start Mini game");
        _inMiniGame = true;
        CreatureBehaviour.CreatureRarity creatureRarity = _creatureToBeSucked.GetComponent<CreatureBehaviour>().creatureRarity;
        for (int i = 0; i < _creatureValues.Length; i++)
        {
            if (_creatureValues[i].Rarity == creatureRarity)
            {
                points = _creatureValues[i].Value;
                break;
            }
        }
        transform.localPosition = _smallSuckMinigamePos;
        MiniGameStarted?.Invoke(_creatureToBeSucked.gameObject);
    }

    public void ExitMiniGame(InputAction.CallbackContext context)
    {
        if (context.started && _inMiniGame)
        {
            Debug.Log("Exit minigame");
            transform.localPosition = _smallSuckOriginalPos;
            _inMiniGame = false;
            MiniGameEnded?.Invoke();
        }
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if(!gameObject.activeSelf)return;
        if (context.started && !_inMiniGame)
        {
            Ray ray = Camera.main.ScreenPointToRay (Mouse.current.position.ReadValue());
            if (Physics.Raycast (ray, out RaycastHit hit, _startMiniGameRange, ~_layersToIgnore)) {
                if(hit.transform.CompareTag("Creature"))
                {
                    hit.transform.TryGetComponent(out _creatureToBeSucked);
                    StartMiniGame();
                }
            }
        }
    }
    private void SuckCreature(float suckForce)
    {
        Vector3 diff = Vector3.Normalize(_creatureToBeSucked.transform.position - _nozzlePosition.position);
        float dot = Vector3.Dot(diff, transform.forward);
        
        _creatureToBeSucked.AddForce(diff * (-dot * suckForce), ForceMode.Acceleration);
    }

    private void SpinCreature()
    {
        // get vector center <- obj
        var gravityVector = _nozzlePosition.position + _nozzlePosition.forward - _creatureToBeSucked.position;

        // check whether left or right of target
        var left = Vector2.SignedAngle(_creatureToBeSucked.velocity, gravityVector) > 0;

        // get new vector which is 90° on gravityDirection 
        // and world Z (since 2D game)
        // normalize so it has magnitude = 1
        var newDirection = Vector3.Cross(gravityVector, Vector3.forward).normalized;

        // invert the newDirection in case user is touching right of movement direction
        if (!left) newDirection *= -1;

        // set new direction but keep speed(previously stored magnitude)
        _creatureToBeSucked.AddForce(newDirection * _creatureSpinSpeed, ForceMode.Force);
    }

    private void CheckIfSucked()
    {
        Collider[] toCheck = Physics.OverlapSphere(_nozzlePosition.position, _suckNozzleSize);
        foreach (var creature in toCheck)
        {
            if(creature.transform.gameObject == _creatureToBeSucked.gameObject)creature.gameObject.SetActive(false);
        }
    }

    private void WinMiniGame()
    {
        Debug.Log("MiniGameWon");
        _creatureToBeSucked.gameObject.SetActive(false);
    }

    private void LoseMiniGame()
    {
        Debug.Log("MiniGameLost");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(_nozzlePosition.position, _suckNozzleSize);
    }
}
