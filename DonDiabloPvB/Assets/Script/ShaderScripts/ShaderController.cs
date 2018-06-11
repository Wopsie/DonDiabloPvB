using UnityEngine;

public class ShaderController : MonoBehaviour
{
    public static ShaderController Instance { get { return GetInstance(); } }

    #region Singleton
    private static ShaderController instance;

    private static ShaderController GetInstance(){
        if (instance == null){
            instance = FindObjectOfType<ShaderController>();
        }

        return instance;
    }
    #endregion

    [SerializeField] Material mat;
    [SerializeField] private float speed, max;
    [SerializeField] private float currentPower, startPower;

    private bool _StartFading = true;
    private bool _FadeTransition = false;
    [SerializeField]private float _SetShader;

    void Start()
    {
        currentPower = 0.2f;
        mat.SetFloat("_DisolveCount", currentPower);
    }

    void Update()
    {
        CallTransition(_SetShader);
    }

    public void TriggerEffect(float ShaderValue)
    {
        Debug.Log(ShaderValue);
        _SetShader = ShaderValue;
        Debug.Log(_SetShader);
    }

    void CallTransition(float Set)
    {
        //this value will open the shader.
        if (Set == 1)
        {
            mat.SetFloat("_DisolveCount", currentPower);
            currentPower += -Time.deltaTime * speed;
            ButtonTransition();
        }

        //this value will close the shader.
        else if (Set == 2)
        {
            mat.SetFloat("_DisolveCount", currentPower);
            currentPower += Time.deltaTime * speed;
            ButtonTransition();
        }

        //this value will activate the button transition.
        else if (Set == 3)
        {
            _FadeTransition = true;
            _StartFading = true;
            ButtonTransition();
        }
    }

    void ButtonTransition()
    {
        if (_FadeTransition)
        {
            if (_StartFading)
            {
                if (currentPower > 0f)
                {
                    currentPower = 0.2f;
                    MainMenuHandler.Instance.ChangeLevelScreen();
                    _SetShader = 1;
                    _StartFading = false;
                }

                if (currentPower < -0.15f)
                {
                    currentPower = -0.15f;
                    _SetShader = 2;
                }

            }
            if (currentPower < -0.15f)
            {
                if (currentPower < -0.15)
                {
                    _SetShader = 0;
                    _FadeTransition = false;
                }
            }
        }

       else if (currentPower < max) {
           // _SetShader = 0;
            currentPower = max;
           
        }
    }
}
