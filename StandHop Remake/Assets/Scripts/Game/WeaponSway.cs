using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] private float intensity = 0.005f;
    [SerializeField] private float smooth = 13.13f;

    private Vector3 origin;

    private void Start()
    {
        origin = transform.localPosition;
    }
    void FixedUpdate()
    {
        UpdateSway();
    }

    private void UpdateSway()
    {
        float rotx = -SimpleInput.GetAxis("LookX") * intensity;
        float roty = -SimpleInput.GetAxis("LookY") * intensity;

        Vector3 final = new Vector3(rotx, roty, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, final + origin, Time.deltaTime * smooth);
    }    
}
