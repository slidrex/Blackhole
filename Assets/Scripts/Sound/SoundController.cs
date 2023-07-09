using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioClip _entityPlaceSound;
    [SerializeField] private AudioClip _entityDeathSound;
    [SerializeField] private AudioClip _playerStepSound;
    [SerializeField] private AudioClip _fountainSound;
    [SerializeField] private AudioClip _swordAttackSound;
    [SerializeField] private AudioClip _buttonClickSound;
    [SerializeField] private AudioClip _selectEntitySound;
    [SerializeField] private AudioClip _deconstructEntity;
    public static SoundController Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void PlayPlayerStep(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(_playerStepSound, position);
    }
    public void PlayMobDeath()
    {
        AudioSource.PlayClipAtPoint(_entityDeathSound, Camera.main.transform.position);
    }
    public void PlaySelectEntity()
    {
        AudioSource.PlayClipAtPoint(_selectEntitySound, Camera.main.transform.position);
    }
    public void PlayPlaceEntity()
    {
        AudioSource.PlayClipAtPoint(_entityPlaceSound, Camera.main.transform.position);
    }
    public void PlayDeconstructEntity()
    {
        AudioSource.PlayClipAtPoint(_deconstructEntity, Camera.main.transform.position);
    }
    public void PlaySwordAttack()
    {
        AudioSource.PlayClipAtPoint(_swordAttackSound, Camera.main.transform.position);
    }
    public void PlayFountain()
    {
        AudioSource.PlayClipAtPoint(_fountainSound, Camera.main.transform.position);
    }
    public void PlayClick()
    {
        AudioSource.PlayClipAtPoint(_buttonClickSound, Camera.main.transform.position);
    }
}
