using UnityEngine;

public class FsmStatePaying : FsmState
{
    public FsmStatePaying(Fsm fsm) : base(fsm) { }
    public override void Enter()
    {
        Debug.Log("Paying state [ENTER]");
    }
    public override void Exit()
    {
        Debug.Log("Paying state [EXIT]");
    }
    public override void Update()
    {
        Debug.Log("Paying state [EXIT]");
        //Логика перехода в другое состояния
        //Fsm.SetState<FmsState{example}>();
    }
}
