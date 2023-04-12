using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotator : MonoBehaviour
{

    public float currentAngle;
    public float startAngle;
    public bool rotating;
    public float rotateDuration;
    public float counter;
    private float _xAngle;

    public float xAngle
    {
        get
        {
            return _xAngle;
        }

        set
        {
            
            startAngle = transform.localRotation.eulerAngles.x;
            _xAngle = value;
            rotating = true;
            counter = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        if (counter > rotateDuration && rotating == true)
        {       
            rotating = false; 
        }

        currentAngle = Mathf.LerpAngle(startAngle, _xAngle, counter / rotateDuration);
        transform.localEulerAngles = new Vector3(currentAngle, 0, 0);
    }
}