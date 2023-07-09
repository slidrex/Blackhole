using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransform : MonoBehaviour
{
    public void PlayStepSound()
    {
        SoundController.Instance.PlayPlayerStep(Camera.main.transform.position);
    }
    public void PlaySwordSound()
    {
        SoundController.Instance.PlaySwordAttack();
    }
}
