using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Logic.Player;

//[ExecuteInEditMode]
public class XRayReplacement : MonoBehaviour
{
    public IPlayerManager _playerManager;

    public Shader XRayShader;
    public float transitionTimePulse;
    public float transitionTimeXRay;
    public float startNearClip;
    public float endNearClip;
    public float startFarClip;
    public float endFarClip;

    private Camera cam;

    private void Awake()
    {
        _playerManager = ServiceLocator.GetService<IPlayerManager>();

        cam = GetComponent<Camera>();
        if(gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if(_playerManager.GetHearing() == 3f)
        {
            StartCoroutine(ChangeClippingPlanes());
        }

        if(_playerManager.GetHearing() == 4)
        {
            StartCoroutine(StayInXRay());
        }
        cam.SetReplacementShader(XRayShader, "XRay");
    }

    private IEnumerator ChangeClippingPlanes()
    {
        cam.nearClipPlane = startNearClip;
        cam.farClipPlane = startFarClip;

        float timer = 0f;
        while (timer < transitionTimePulse)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / transitionTimePulse);
            cam.farClipPlane = Mathf.Lerp(startFarClip, endFarClip, t);
            yield return null;
        }

        //Near clipping plane
        timer = 0f;
        while (timer < transitionTimePulse)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / transitionTimePulse);
            cam.nearClipPlane = Mathf.Lerp(startNearClip, endNearClip, t);
            yield return null;
        }

        cam.nearClipPlane = endNearClip;
        cam.farClipPlane = endFarClip;
        gameObject.SetActive(false);
    }

    private IEnumerator StayInXRay()
    {
        cam.nearClipPlane = 0.5f;
        cam.farClipPlane = 50f;

        float timer = 0f;
        while (timer < transitionTimeXRay)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}