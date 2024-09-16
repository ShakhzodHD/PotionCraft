using UnityEngine;

public class ButtonActionHeartEffect : IButtonAction
{
    public void Execute(ParticleSystem particle)
    {
        particle.Play();
    }
}
