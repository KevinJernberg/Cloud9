using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///
///Structure for a Hierarchy StateMachine - William
///
/// </summary>
/// <typeparam name="TState"></typeparam>


public class StateMachine<TState> where TState : class, IStateNode<TState>{
        public TState currentState = null;
        private Stack<TState> stateStack = new Stack<TState>();

        public void InitializeMachine(TState startState){
            currentState = startState;
            TState enterState = startState;
            stateStack.Clear();
            while(enterState != null){
                stateStack.Push(enterState);
                enterState = enterState.ParentState;
            }
            while(0 < stateStack.Count){
                stateStack.Pop().Enter();
            }
        }
        public void FinalizeMachine(){
            TState exitState = currentState;
            while(exitState != null){
                exitState.Exit();
                exitState = exitState.ParentState;
            }
            currentState = null;
        }

        public void Transit(TState toState){
            TState exitState = currentState;
            TState enterState = toState;
            stateStack.Clear();
            while(enterState.StateLevel < exitState.StateLevel){
                exitState.Exit();
                exitState = exitState.ParentState;
            }
            while(exitState.StateLevel < enterState.StateLevel){
                stateStack.Push(enterState);
                enterState = enterState.ParentState;
            }
            while(exitState != enterState){
                exitState.Exit();
                stateStack.Push(enterState);
                exitState = exitState.ParentState;
                enterState = enterState.ParentState;
            }
            while(0 < stateStack.Count){
                stateStack.Pop().Enter();
            }
            currentState = toState;
        }
    }
