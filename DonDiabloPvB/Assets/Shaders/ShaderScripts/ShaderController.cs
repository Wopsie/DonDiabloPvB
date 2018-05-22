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


    private void Start()
    {
        currentPower = 0;
        mat.SetFloat("_DisolveCount", currentPower);
    }

    private void Update()
    {

        if (currentPower < max)
        {
            currentPower = max;
            _startButton.ToChangeLevel(2);
            _FadeTransition = true;
            _StartFading = false;
        }
        if (currentPower > 0)
        {
            currentPower = 0;
            _startButton.ToChangeLevel(1);
            _FadeTransition = false;
        }

        if (_StartFading & !_FadeTransition)
        {
            mat.SetFloat("_DisolveCount", currentPower);
            currentPower += -Time.deltaTime * speed;
        }

        if (_FadeTransition & _StartFading)
        {
            Debug.Log("help");
            mat.SetFloat("_DisolveCount", currentPower);
            currentPower += Time.deltaTime * speed;
        }
    }

    public void TriggerEffect() {
        _StartFading = true;
        //startPower = 0;
        //currentPower = 0;
    }

    //TriggerEffect();
}
