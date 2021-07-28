using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour, IDecoration
{
    private CinemachineVirtualCamera _cinemachineVC;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;
    public static float time = 0.7f, intesity=1.5f;

    private void Start()
    {
        _cinemachineVC = GetComponent<CinemachineVirtualCamera>();
        _cinemachineBasicMultiChannelPerlin = _cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("shake camera");
            Shake();
        }
        

#endif
    }

    public void InvokeDecoration()
    {
        if (Random.Range(0.0f, 1.0f) < 0.5f)
            return;
        Shake();
    }

    public void Shake()
    {
        StartCoroutine(StopShakingCrt(intesity,time));
    }

    private IEnumerator StopShakingCrt(float intesity, float timer)
    {
        var timerTotal = timer;
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intesity;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(intesity, 0, (1 - (timer / timerTotal)));
            yield return null;
        }

        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;

    }


}
