using UnityEngine;

public class ButtonActionCurseEffect : IButtonAction
{
    public void Execute(ParticleSystem particle)
    {
        particle.Play();
    }
}
