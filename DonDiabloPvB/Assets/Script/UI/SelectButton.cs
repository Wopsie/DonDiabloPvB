using UnityEngine;

public class SelectButton : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private bool _IsSelectButton = false;
    [SerializeField] private int _levelNumber;
    [SerializeField] private GameObject ReadyButton;

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
        ShaderController.Instance.TriggerEffect(3);
        //StartButton.Instance.PassLevelNumber(_levelNumber);
        ReadyButton.SetActive(true);
    }
}
