using UnityEngine;
using System.Collections;

public class ShaderController: MonoBehaviour
{

    [SerializeField] Material mat;

    [SerializeField] private float speed, max;

    [SerializeField] private float currentPower, startPower;


    private void Update()
    {
        if (currentPower > max) {
            mat.SetFloat("_DisolveCount", currentPower);
            currentPower += -Time.deltaTime * speed;
        }

        if (Input.GetKeyDown(KeyCode.E))
            TriggerEffect();
    }

    private void TriggerEffect() {
        startPower = 0;
        currentPower = 0;
    }
}
