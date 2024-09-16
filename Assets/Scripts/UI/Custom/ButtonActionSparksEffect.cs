using UnityEngine;

public class ButtonActionSparksEffect : IButtonAction
{
    public void Execute(ParticleSystem particle)
    {
        particle.Play();
    }
}
