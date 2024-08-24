using UnityEngine;

public class FsmStateSelecting : FsmState 
{
    public FsmStateSelecting(Fsm fsm) : base(fsm) { }
    public override void Enter()
    {
        Debug.Log("Selecting state [ENTER]");
    }
    public override void Exit()
    {
        Debug.Log("Selecting state [EXIT]");
    }
    public override void Update()
    {
        Debug.Log("Selecting state [EXIT]");
        //Логика перехода в другое состояния
        //Fsm.SetState<FmsState{example}>();
    }
}
