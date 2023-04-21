using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class XRayReplacement : MonoBehaviour
{
    public Shader XRayShader;
    public float transitionTime = 3f;
    public float startNearClip;
    public float endNearClip;
    public float startFarClip;
    public float endFarClip;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        StartCoroutine(ChangeClippingPlanes());
        cam.SetReplacementShader(XRayShader, "XRay");
    }

    private IEnumerator ChangeClippingPlanes()
    {
        cam.nearClipPlane = startNearClip;
        cam.farClipPlane = startFarClip;

        float timer = 0f;
        while (timer < transitionTime)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / transitionTime);
            cam.farClipPlane = Mathf.Lerp(startFarClip, endFarClip, t);
            yield return null;
        }

        //Near clipping plane
        timer = 0f;
        while (timer < transitionTime)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / transitionTime);
            cam.nearClipPlane = Mathf.Lerp(startNearClip, endNearClip, t);
            yield return null;
        }

        cam.nearClipPlane = endNearClip;
        cam.farClipPlane = endFarClip;
    }
}