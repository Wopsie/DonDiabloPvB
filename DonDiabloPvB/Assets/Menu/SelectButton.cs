using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : MonoBehaviour
{

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    [SerializeField] private bool _IsSelectButton = false;

    [SerializeField] private ShaderController _shaderController;

    [SerializeField] private StartButton _startButton;

    [SerializeField] private int _levelNumber;

    private void Awake()
    {
        _audioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void ButtonSelect()
    {
        //Changes the music that you hear in the menu.
        _audioSource.Stop();
        _audioSource.clip = _audioClip;
        //_audioSource.Play();
        _shaderController.TriggerEffect(3);
        _startButton.LevelNumber(_levelNumber);
    }
}
