using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private float speed;
    private SphereCollider detectionRadius;
    private MeshRenderer meshRenderer;
    [SerializeField] private Material normal;
    [SerializeField] private Material alert;
    [SerializeField] private Material flee;
    public LayerMask LookingFor;
    private Transform enemy;
    private Vector3 direction;
    private bool setPos;

    public void Awake(){
        detectionRadius = GetComponent<SphereCollider>();
        detectionRadius.isTrigger = true;
        meshRenderer = GetComponent<MeshRenderer>();
        rootCreatureState = new RootState(this, null);
        idleCreatureState = new IdleState(this, rootCreatureState);
        alertCreatureState = new AlertState(this, rootCreatureState);
        fleeCreatureState = new FleeState(this, rootCreatureState);
        _stateMachine = new StateMachine<CreatureState>();
        _stateMachine.InitializeMachine(idleCreatureState);
        
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
    
    public void SetPos()
    {
        Vector3 enemyPos = enemy.position;
        direction = new Vector3(transform.position.x -enemyPos.x, transform.position.y -enemyPos.y, transform.position.z -enemyPos.z).normalized;
        setPos = true;
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

        if (setPos)
        {
            transform.position += direction * (speed * Time.deltaTime);
            despawntimer+= Time.deltaTime;
            if (despawntimer >= delayTillDespawn)
            {
                Destroy(gameObject);
            }
        }
        
    }

    public void Flee(){
        _stateMachine.currentState.Flee();
    }

    IEnumerator TimerForFlee()
    {
        CreatureState current= _stateMachine.currentState;
        yield return new WaitForSeconds(delayTillFlee);
        
        yield return null;
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
        public override void Enter(){Debug.Log("RootState: Enter()");}
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
        }

        public override void Range()
        {
            Creature._stateMachine.Transit(Creature.alertCreatureState);
        }

        public override void Exit(){Debug.Log("IdleState: Exit()");}
    }

    private class AlertState : CreatureState{      
        public AlertState(CreatureBehaviour creature, CreatureState parent) : base(creature, parent){}

        public override void Enter(){
            Debug.Log("AlertState: Enter()");
            Creature.meshRenderer.material = Creature.alert;
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
            Creature.SetPos();
        }
        public override void Exit(){Debug.Log("FleeState: Exit()");}
    }
    #endregion
}



