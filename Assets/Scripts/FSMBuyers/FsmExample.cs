using UnityEngine;

public class FsmExample : MonoBehaviour
{
    private Fsm fsm;
    //ќстальные пол€: скорость передвижени€ и прочее.
    private void Start()
    {
        fsm = new Fsm();

        fsm.AddState(new FsmStateBrowsing(fsm));
        fsm.AddState(new FsmStateSelecting(fsm));
        fsm.AddState(new FsmStatePaying(fsm));
        fsm.AddState(new FsmStateCheckout(fsm));
        fsm.AddState(new FsmStateExiting(fsm));

        fsm.SetState<FsmStateBrowsing>();
    }
    private void Update()
    {
        fsm.Update();
    }
}
