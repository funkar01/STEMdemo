using System.Collections;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public float speed = 0f;
    [SerializeField] float speedFactor = 10f;
    [SerializeField] Transform motorShaft;
    Coroutine rotationCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    public void UpdateMotorSpeed(bool isIncreased)
    {
        if (rotationCoroutine != null) // Start the rotation coroutine if it's not already running
            StopCoroutine(rotationCoroutine);

        if (isIncreased)
        {
            if (speed < 90f)
                speed += 15f;
        }
        else
        {
            if (speed > 0f)
                speed -= 15f;
        }
        SetMotorSpeed();
    }

    IEnumerator RotateMotor()
    {
        while (true)
        {
            if (motorShaft != null)
            {
                // rotate around local Y using speed as degrees per second
                motorShaft.Rotate(Vector3.left, speed * speedFactor * Time.deltaTime, Space.Self);
            }
            yield return null;
        }
    }
    void SetMotorSpeed()
    {
        // Here you would set the motor speed using your specific motor control code.
        // rotation is handled continuously in the RotateMotor coroutine; speed is used as degrees/sec
        rotationCoroutine = StartCoroutine(RotateMotor());
    }
}
