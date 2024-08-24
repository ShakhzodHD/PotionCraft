using UnityEngine;

public class FsmStateBrowsing : FsmState
{
    public FsmStateBrowsing(Fsm fsm) : base(fsm) { }
    public override void Enter()
    {
        Debug.Log("Browsing state [ENTER]");
    }
    public override void Exit()
    {
        Debug.Log("Browsing state [EXIT]");
    }
    public override void Update()
    {
        Debug.Log("Browsing state [Update]");
        //Логика перехода в другое состояния
        //Fsm.SetState<FmsState{example}>();
    }
}
