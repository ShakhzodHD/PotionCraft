using UnityEngine;

public class FsmStateExiting : FsmState
{
    public FsmStateExiting(Fsm fsm) : base(fsm) { }
    public override void Enter()
    {
        Debug.Log("Exiting state [ENTER]");
    }
    public override void Exit()
    {
        Debug.Log("Exiting state [EXIT]");
    }
    public override void Update()
    {
        Debug.Log("Exiting state [EXIT]");
        //Логика перехода в другое состояния
        //Fsm.SetState<FmsState{example}>();
    }
}
