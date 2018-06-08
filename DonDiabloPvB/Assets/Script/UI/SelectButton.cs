using UnityEngine;

public class SelectButton : MonoBehaviour { 
    [SerializeField] private AudioClip AudioClip;
    [SerializeField] private int LevelNumber;

    public void ButtonSelect()
    {
        //Changes the music that you hear in the menu.
        MainMenuHandler.Instance.SetAudioClip(AudioClip);
        MainMenuHandler.Instance.AudioSource.Play();
        ShaderController.Instance.TriggerEffect(3);
        MainMenuHandler.Instance.SetLevelNumber(LevelNumber);
        //
        MainMenuHandler.Instance.UIStartButton.SetActive(true);
    }
}
