using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
///
/// Creature class where the creature becomes alert if something comes too close and flees if it doesn't leave in time - William
/// 
/// </summary>
/// <typeparam name="TState"></typeparam>

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class CreatureBehaviour : MonoBehaviour
{
    private CreatureState rootCreatureState = null;
    private CreatureState idleCreatureState = null;
    private CreatureState alertCreatureState = null;
    private CreatureState fleeCreatureState = null;

    private StateMachine<CreatureState> _stateMachine = null;
    private bool startTimer;
    private float timer;
    private float despawntimer;
    [SerializeField] private float delayTillFlee;
    [SerializeField] private float delayTillDespawn;
    [Tooltip("The value that is used in multiplication of movement")][SerializeField] private float speed;
    [Tooltip("The maximum speed the rigidbody can have when idle")][SerializeField] private float idlemaxSpeed;
    [Tooltip("The maximum speed the rigidbody can have when fleeing")][SerializeField] private float fleemaxSpeed;
    [Tooltip("The Distance the creature can go from its spawnpoint before its dragged backwards")][SerializeField] private float maxDistance;
    private SphereCollider detectionRadius;
    private MeshRenderer meshRenderer;
    [SerializeField] private Material normal;
    [SerializeField] private Material alert;
    [SerializeField] private Material flee;
    [Tooltip("The Layer that the creature is looking for within its trigger collider, this creature goes to alert if it detects something with it")]public LayerMask LookingFor;
    private Transform enemy;
    private Vector3 direction;
    private bool BeginFlee;
    private bool idle;
    private bool move;
    private float maxSpeed;
    private Rigidbody rb;
    private Vector3 startpos;
    
    private CloudCreatureSpawner connectedSpawner;

    public void Awake(){
        detectionRadius = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        detectionRadius.isTrigger = true;
        meshRenderer = GetComponent<MeshRenderer>();
        rootCreatureState = new RootState(this, null);
        idleCreatureState = new IdleState(this, rootCreatureState);
        alertCreatureState = new AlertState(this, rootCreatureState);
        fleeCreatureState = new FleeState(this, rootCreatureState);
        _stateMachine = new StateMachine<CreatureState>();
        _stateMachine.InitializeMachine(idleCreatureState);
        startpos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LookingFor == (LookingFor | (1 << other.transform.gameObject.layer)))
        {
            enemy= other.transform;
            Range();
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (LookingFor == (LookingFor | (1 << other.transform.gameObject.layer)))
        {
            Range();
        }
    }

    public void Range(){
        _stateMachine.currentState.Range();
    }

    public void StartTimer()
    {
        timer = 0f;
        startTimer = true;
    }
    public void StopTimer()
    {
        startTimer = false;
    }
    /// <summary>
    /// hämtar vad som gjorde den alerts position, sätter riktingen åt motsatt håll och sätter bool för att börja röra på sig
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
        }
        if (timer >= delayTillFlee)
        {
            Flee();
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
            if (!move)
            {
                move = true;
                StartCoroutine(Move());
            }
        }

        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    public void Flee(){
        _stateMachine.currentState.Flee();
    }

    IEnumerator Move()
    {
        //var force = Vector3.zero;
        var dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized* (speed);
        var distance = Vector3.Distance(transform.position, startpos);
        var dirToStart = new Vector3(startpos.x - transform.position.x , startpos.y - transform.position.y, startpos.x - transform.position.z).normalized;
        //force = new Vector3(dir.x * (dirToStart.x), dir.y * dirToStart.y, dir.z * dirToStart.z);
        rb.AddForce(dir , ForceMode.Force);
        if (distance >= maxDistance)
        {
            Debug.Log("now goes mid");
            rb.AddForce(dirToStart * ((distance/50)*speed) , ForceMode.Force);
        }
        
        //yield return new WaitForSeconds(1);
        move = false;
        yield return null;
    }
    
    IEnumerator TimerForFlee()
    {
        CreatureState current= _stateMachine.currentState;
        yield return new WaitForSeconds(delayTillFlee);
        
        yield return null;
    }
    
    public void SetSpawner(CloudCreatureSpawner spawner)
    {
        connectedSpawner = spawner;
    }

    #region States
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
    }
    private class RootState : CreatureState{      
        public RootState(CreatureBehaviour creature, CreatureState parent) : base(creature, parent){}
        public override void Enter(){Debug.Log("RootState: Enter()"); Creature.maxSpeed=Creature.idlemaxSpeed;}
        public override void Exit(){Debug.Log("RootState: Exit()");}
        public override void Range(){}
        public override void Flee(){}
    }
    
    private class IdleState : CreatureState{
        
        public IdleState(CreatureBehaviour creature, CreatureState parent) : base(creature, parent){}

        public override void Enter()
        {
            Debug.Log("IdleState: Enter()");
            Creature.meshRenderer.material = Creature.normal;
            Creature.idle = true;
        }

        public override void Range()
        {
            Creature._stateMachine.Transit(Creature.alertCreatureState);
        }

        public override void Exit(){Debug.Log("IdleState: Exit()"); Creature.idle = false; }
    }

    private class AlertState : CreatureState{      
        public AlertState(CreatureBehaviour creature, CreatureState parent) : base(creature, parent){}

        public override void Enter(){
            Debug.Log("AlertState: Enter()");
            Creature.meshRenderer.material = Creature.alert;
            Creature.rb.velocity = Vector3.zero;
            Creature.StartTimer();
        }

        public override void Exit()
        {
            Debug.Log("AlertState: Exit()");
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
    }
    private class FleeState : CreatureState{
        
        public FleeState(CreatureBehaviour creature, CreatureState parent) : base(creature, parent){}

        public override void Enter()
        {
            Debug.Log("FleeState: Enter()");
            Creature.meshRenderer.material = Creature.flee;
            Creature.maxSpeed = Creature.fleemaxSpeed;
            Creature.SetPos();
        }
        public override void Exit(){Debug.Log("FleeState: Exit()");}
    }
    #endregion
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(startpos,maxDistance);

    }
}



