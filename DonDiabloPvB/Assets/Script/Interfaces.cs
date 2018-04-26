using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public interface IDamagable
    {
        void OnPlayerContact();
        void OnPlayerInput();
        float CheckPlayerDistance();
        void SetInputWindow(float dist);
    }


