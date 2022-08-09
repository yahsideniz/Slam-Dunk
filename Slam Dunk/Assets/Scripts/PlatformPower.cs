using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPower : MonoBehaviour
{
    [SerializeField] private float Angle;
    [SerializeField] private float appliedPower;

    private void OnCollisionEnter(Collision other)
    {
        other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Angle, 90, 0) * appliedPower, ForceMode.Force);

    }
}
