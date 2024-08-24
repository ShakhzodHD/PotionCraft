using UnityEngine;

public class FsmStateCheckout : FsmState
{
    public FsmStateCheckout(Fsm fsm) : base(fsm) { }
    public override void Enter()
    {
        Debug.Log("Checkout state [ENTER]");
    }
    public override void Exit()
    {
        Debug.Log("Checkout state [EXIT]");
    }
    public override void Update()
    {
        Debug.Log("Checkout state [Update]");
        //Логика перехода в другое состояния
        //Fsm.SetState<FmsState{example}>();
    }
}
