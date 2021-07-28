using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UI_FPS : MonoBehaviour
    {
        [SerializeField] Text textFps;
        private float _deltaTime;

        private void Update()
        {
            _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
            float fps = 1.0f / _deltaTime;
            textFps.text = Mathf.Ceil(fps).ToString();
        }

    }
}