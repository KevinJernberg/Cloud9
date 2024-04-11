using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///
/// Basic Structure for a Node in the StateMachine and what it needs to have - William
/// 
/// </summary>
/// <typeparam name="TState"></typeparam>
public interface IStateNode<TState>
{
    TState ParentState { get; }
    int StateLevel { get; }
    void Enter();
    void Exit();
}
