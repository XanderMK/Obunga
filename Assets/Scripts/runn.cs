using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runn : MonoBehaviour
{
    public Transform leftLeg;
    public Transform rightLeg;
    public Transform leftArm;
    public Transform rightArm;

    public float speed;

    // Update is called once per frame
    void Update()
    {
        float sineWave = Mathf.Sin(Time.time * speed);
        leftLeg.localRotation = Quaternion.Euler(sineWave * 90f, 0f, 180f);
        rightLeg.localRotation = Quaternion.Euler(sineWave * -90f, 0f, 180f);
        leftArm.localRotation = Quaternion.Euler(0f, sineWave * 60f, 85f);
        rightArm.localRotation = Quaternion.Euler(0f, sineWave * 60f, -85f);
    }
}
