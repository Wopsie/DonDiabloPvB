using UnityEngine;

/// <summary>
/// This class is resonsible for changing the values of our shader script so that the texture disolves by these values.
/// </summary>
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


    /// <summary>
    /// This start function makes sure texture value is full.
    /// </summary>
    void Start()
    {
        currentPower = 0.2f;
        mat.SetFloat("_DisolveCount", currentPower);
    }

    /// <summary>
    /// this update functions calls the function that changes the value of the shader.
    /// </summary>
    void Update()
    {
        CallTransition(_SetShader);
    }

    /// <summary>
    /// This function is being called upon in other scripts to start the value changing with the parameter making sure in what way.
    /// </summary>
    /// <param name="ShaderValue"></param>
    public void TriggerEffect(float ShaderValue)
    {
        Debug.Log(ShaderValue);
        _SetShader = ShaderValue;
        Debug.Log(_SetShader);
    }

    /// <summary>
    /// This function changes the values in the shader, the parameter is set to choose in what way this function changes the value in the shader.
    /// </summary>
    /// <param name="Set"></param>
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

    /// <summary>
    /// This function makes sure the shader values stop at a certain point.
    /// </summary>
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
