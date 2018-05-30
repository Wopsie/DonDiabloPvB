using UnityEngine;
using System.Collections;

public class ShaderController: MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] private float speed, max;
    [SerializeField] private float currentPower, startPower;
    [SerializeField] private StartButton _startButton;

    private bool _StartFading = false;
    private bool _FadeTransition = false;
    private float _SetShader = 0;

    private void Start()
    {
        currentPower = 0;
        mat.SetFloat("_DisolveCount", currentPower);
    }

    private void Update()
    {
        TransitionButton();
        CallTransition(_SetShader);
    }

    public void TriggerEffect(float ShaderValue)
    {
        _SetShader = ShaderValue;
    }

    private void CallTransition(float Set)
    {
        //this value will open the shader.
        if (Set == 1)
        {
            mat.SetFloat("_DisolveCount", currentPower);
            currentPower += -Time.deltaTime * speed;
        }

        //this value will close the shader.
        else if (Set == 2)
        {
            Debug.Log("help");
            mat.SetFloat("_DisolveCount", currentPower);
            currentPower += Time.deltaTime * speed;
        }

        //this value will activate the button transition.
        else if (Set == 3)
        {
            _StartFading = true;
        }
        else if (Set == 0) Debug.Log("haha niets");
    }

    //this function will set the shader to turn off and on to make the button transition.
    private void TransitionButton()
    {
        if (_StartFading & !_FadeTransition)
        {
            _SetShader = 1;
            CurrentValueCheck();
        }

        else if (_FadeTransition & _StartFading)
        {
            _SetShader = 2;
            CurrentValueCheck();
        }
    }

    //This function is used to check if the shaded is over his max value and changes booleans to change back.
    private void CurrentValueCheck()
    {
        if (currentPower < max)
        {
            currentPower = max;
            _FadeTransition = true;
            _StartFading = false;
        }
        else if (currentPower > 0)
        {
            currentPower = 0;
            _startButton.ToChangeLevel();
            _FadeTransition = false;
        }
        _SetShader = 0;
    }
}
