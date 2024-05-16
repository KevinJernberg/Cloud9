using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
///
/// Creature class where the creature becomes alert if something comes too close and flees if it doesn't leave in time - William
/// Added support for creature rarity - Kevin
/// </summary>
/// <typeparam name="TState"></typeparam> TState is a name for a random state that fulfills the conditions of IStateNode

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class CreatureBehaviour : MonoBehaviour
{
    private CreatureState rootCreatureState = null;
    private CreatureState idleCreatureState = null;
    private CreatureState alertCreatureState = null;
    private CreatureState fleeCreatureState = null;
    private CreatureState suckedCreatureState = null;

    private StateMachine<CreatureState> _stateMachine = null;
    private bool startTimer;
    private float timer;
    private float despawntimer;
    [Header("Gizmos")]
    [SerializeField]private bool showMaxDistance;

    [SerializeField] private bool showAvoidRange;
    
    [Header("Creature Behaviour Values")]
    [SerializeField] private float delayTillFlee;
    [SerializeField] private float delayTillDespawn;
    [Tooltip("The value that is used in multiplication of movement")][SerializeField] private float speed;
    [Tooltip("How aggressively it should drag the creature back when it further away then MaxDistance from where it spawned. Lower number = More aggressive")][SerializeField] private float damper;
    [Tooltip("The maximum speed the rigidbody can have when idle")][SerializeField] private float idlemaxSpeed;
    [Tooltip("The maximum speed the rigidbody can have when fleeing")][SerializeField] private float fleemaxSpeed;
    [Tooltip("The Distance the creature can go from its spawnpoint before its dragged backwards")][SerializeField] private float maxDistance;
    [Tooltip("The Layer that the creature is looking for within its trigger collider, this creature goes to alert if it detects something with it")]public LayerMask LookingFor;
    [Tooltip("The Layers that the creature is avoiding")][SerializeField]private LayerMask Avoiding;
    [Tooltip("The radius it will keep between it self and other objects with the layer of 'Avoiding'")] [SerializeField] private float avoidanceRange;
    
    private SphereCollider detectionRadius;
    private MeshRenderer meshRenderer;
    
    [Header("Creature Materials For States (Temporary)")]
    [SerializeField] private Material normal;
    [SerializeField] private Material alert;
    [SerializeField] private Material flee;
    
    private Transform enemy;
    private Vector3 direction;
    private bool BeginFlee;
    private bool idle;
    private bool move;
    private float maxSpeed;
    private Rigidbody rb;
    private Vector3 startpos;
    private List<Transform> Avoid;

    private CloudCreatureSpawner connectedSpawner;
    public ItemData creatureItem;

    public CreatureRarity creatureRarity;
    
    public void Awake(){
        detectionRadius = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        detectionRadius.isTrigger = true;
        startpos = transform.position;
        meshRenderer = GetComponent<MeshRenderer>();
        Avoid = new List<Transform>();
        rootCreatureState = new RootState(this, null);
        idleCreatureState = new IdleState(this, rootCreatureState);
        alertCreatureState = new AlertState(this, rootCreatureState);
        fleeCreatureState = new FleeState(this, rootCreatureState);
        suckedCreatureState = new SuckedState(this, rootCreatureState);
        _stateMachine = new StateMachine<CreatureState>();
        _stateMachine.InitializeMachine(idleCreatureState);
        
    }
    

    #region Triggers

    
    /// <summary>
    /// Checks if the thing that entered its collier has a layer that is in "LookingFor". If it does, saves the transform and calls the functions "Range"
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (LookingFor == (LookingFor | (1 << other.transform.gameObject.layer)))
        {
            enemy= other.transform;
            Range();
        }
        if (Avoiding == (Avoiding | (1 << other.transform.gameObject.layer)))
        {
            Avoid.Add(other.transform);
        }
    }
    /// <summary>
    /// Checks if the thing that entered its collier has a layer that is in "LookingFor". If it does, calls the functions "Range"
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (LookingFor == (LookingFor | (1 << other.transform.gameObject.layer)))
        {
            Range();
        }
        if (Avoiding == (Avoiding | (1 << other.transform.gameObject.layer)) && Avoid.Contains(other.transform))
        {
            Avoid.Remove(other.transform);
        }
    }
    #endregion

    #region Funktions
    /// <summary>
    /// Changes the state of the creature to the behaviour it should have when it is being sucked
    /// </summary>
    public void IsSucked()
    {
        _stateMachine.currentState.Sucked();
    }
    
    
    
    public void Range(){
        _stateMachine.currentState.Range();
    }
    /// <summary>
    /// Starts the timer that counts down until the creature should flee 
    /// </summary>
    public void StartTimer()
    {
        timer = 0f;
        startTimer = true;
    }
    /// <summary>
    /// Stops the timer that counts down until the creature should flee 
    /// </summary>
    public void StopTimer()
    {
        startTimer = false;
    }
    /// <summary>
    /// Sets the direction in the opposite direction to the thing that made it alerted and sets the bool for it to flee
    /// </summary>
    public void SetPos()
    {
        Vector3 enemyPos = enemy.position;
        direction = new Vector3(transform.position.x -enemyPos.x, transform.position.y -enemyPos.y, transform.position.z -enemyPos.z).normalized;
        BeginFlee = true;
    }

    private void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;
            if (timer >= delayTillFlee)
            {
                Flee();
            }
        }
        
        if (BeginFlee)
        {
            rb.AddForce(direction * (speed * 5), ForceMode.Force);
            despawntimer+= Time.deltaTime;
            if (despawntimer >= delayTillDespawn)
            {
                Destroy(gameObject);
            }
        }

        if (idle)
        {
            StartCoroutine(Move());
        }
        
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    public void Flee(){
        _stateMachine.currentState.Flee();
    }
    /// <summary>
    /// Moves in a random direction and if its too far from where it spawned it begins to move back to the allowed range
    /// It will also add a force away from a objects center if it gets inside a range add has a certain layer
    /// </summary>
    IEnumerator Move()
    {
        var dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized* (speed);
        var distance = Vector3.Distance(transform.position, startpos);
        rb.AddForce(dir *speed , ForceMode.Force);
        if (distance >= maxDistance)
        {
            var dirToStart = new Vector3(startpos.x - transform.position.x , startpos.y - transform.position.y, startpos.z - transform.position.z).normalized;
            rb.AddForce(dirToStart * ((distance/damper)*speed) , ForceMode.Force);
        }

        if (Avoid.Count > 0 && _stateMachine.currentState!=alertCreatureState)
        {
            for (int i = 0; i < Avoid.Count; i++)
            {
                var distancefromobject = Vector3.Distance(transform.position, Avoid[i].position);
                if (distancefromobject <= avoidanceRange)
                {
                    var diraway = new Vector3(transform.position.x -Avoid[i].position.x, transform.position.y-Avoid[i].position.y, transform.position.z-Avoid[i].position.z).normalized;
                    rb.AddForce(diraway * ((distancefromobject/damper)*speed) , ForceMode.Force);
                }
            }
        }
        yield return null;
    }
    
    public void SetSpawner(CloudCreatureSpawner spawner)
    {
        connectedSpawner = spawner;
    }
    
	
	#endregion


    #region States
    
    /// <summary>
    /// The basestate for this script, holds all the valid functions in it and the basic logic that all children from it
    /// inherits, such as "Creature" that allows all children to have access to other variables in the "CreatureBehaviour" class.
    /// It also holds the functions such as "Flee" which the children inherit and can override if it should do different things in
    /// different states.
    /// </summary>
    private abstract class CreatureState: IStateNode<CreatureState> {
        public CreatureBehaviour Creature;
        public CreatureState ParentState { get; }
        public int StateLevel { get; }

        protected CreatureState(CreatureBehaviour creature, CreatureState parent){
            this.Creature = creature;
            this.ParentState = parent;
            StateLevel = parent == null ? 0 : parent.StateLevel + 1;
        }
        public virtual void Enter(){}
        public virtual void Exit(){}
        public virtual void Range(){ParentState.Range();}
        public virtual void Flee(){ParentState.Flee();}
        public virtual void Sucked(){ParentState.Sucked();}
    }
    
    /// <summary>
    /// The rootstate, is the state highest up in the hierarchy. If "Flee" or "Range" is called in any state and neither it or its parents
    /// overrides it the functions goes here which isn't the plan 
    /// </summary>
    private class RootState : CreatureState{      
        public RootState(CreatureBehaviour creature, CreatureState parent) : base(creature, parent){}

        public override void Enter()
        {
            //Debug.Log("RootState: Enter()");
        }

        public override void Exit()
        {
            //Debug.Log("RootState: Exit()");
        }
        public override void Range(){Debug.LogError("Range ha reached RootState");}
        public override void Flee(){Debug.LogError("Flee ha reached RootState");}
        public override void Sucked(){Debug.LogError("Flee ha reached RootState");}
    }
    /// <summary>
    /// The IdleState: Sets the creatures behaviour to its idle state and transitions to alert if "Range" is called when in this state
    /// </summary>
    private class IdleState : CreatureState{
        
        public IdleState(CreatureBehaviour creature, CreatureState parent) : base(creature, parent){}

        public override void Enter()
        {
            //Debug.Log("IdleState: Enter()");
            Creature.meshRenderer.material = Creature.normal;
            Creature.maxSpeed=Creature.idlemaxSpeed;
            Creature.idle = true;
        }

        public override void Range()
        {
            Creature._stateMachine.Transit(Creature.alertCreatureState);
        }

        public override void Exit()
        {
            //Debug.Log("IdleState: Exit()"); 
            Creature.idle = false;
        }

        public override void Sucked()
        {
            Creature._stateMachine.Transit(Creature.suckedCreatureState);
        }
    }
    /// <summary>
    /// The AlertState: Sets the creatures behaviour to its alert state. Transitions to idle if range is called when in this state
    /// and to flee if "Flee" is called when in this state. It also starts a timer when its entered and stops it when it leaves it
    /// </summary>
    private class AlertState : CreatureState{      
        public AlertState(CreatureBehaviour creature, CreatureState parent) : base(creature, parent){}

        public override void Enter(){
            //Debug.Log("AlertState: Enter()");
            Creature.meshRenderer.material = Creature.alert;
            Creature.rb.velocity = Vector3.zero;
            Creature.StartTimer();
        }

        public override void Exit()
        {
            //Debug.Log("AlertState: Exit()");
            Creature.StopTimer();
        }

        public override void Range()
        {
            Creature._stateMachine.Transit(Creature.idleCreatureState);
        }

        public override void Flee()
        {
            Creature._stateMachine.Transit(Creature.fleeCreatureState);
        }
        public override void Sucked()
        {
            Creature._stateMachine.Transit(Creature.suckedCreatureState);
            Debug.Log("GÅR TILL SUCK");
        }
    }
    /// <summary>
    /// The FleeState: Sets the creatures behaviour to its alert state.
    /// </summary>
    private class FleeState : CreatureState{
        
        public FleeState(CreatureBehaviour creature, CreatureState parent) : base(creature, parent){}

        public override void Enter()
        {
            //Debug.Log("FleeState: Enter()");
            Creature.meshRenderer.material = Creature.flee;
            Creature.maxSpeed = Creature.fleemaxSpeed;
            Creature.idle = false;
            Creature.SetPos();
        }

        public override void Exit()
        {
            //Debug.Log("FleeState: Exit()");
        }
        public override void Range()
        {
        }
        public override void Sucked()
        {
            Creature._stateMachine.Transit(Creature.suckedCreatureState);
        }
    }
    
    private class SuckedState : CreatureState{
        
        public SuckedState(CreatureBehaviour creature, CreatureState parent) : base(creature, parent){}

        public override void Enter()
        {
            Debug.Log("SuckedState: Enter()");
            Creature.meshRenderer.material = Creature.normal;
            Creature.maxSpeed=Creature.idlemaxSpeed;
            Creature.idle = true;
            Creature.BeginFlee = false;
            Creature.StopTimer();
        }

        public override void Exit()
        {
            Debug.Log("SuckedState: Exit()");
        }
        public override void Range()
        {
        }

        public override void Flee()
        {
            
        }

        public override void Sucked()
        {
            Creature._stateMachine.Transit(Creature.fleeCreatureState);
        }
        
    }
    #endregion
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (showMaxDistance)
        {
            Gizmos.DrawSphere(startpos,maxDistance);
        }

        if (showAvoidRange)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position,avoidanceRange);
        }
        

    }
    public enum CreatureRarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }
}