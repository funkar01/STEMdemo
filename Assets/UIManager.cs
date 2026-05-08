using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform meterNeedle;
    [SerializeField] TMP_Text speedText;
    string speed;
    bool isNeedleMoving = false;
    Coroutine needleCoroutine;
    [SerializeField] float needleLerpDuration = 0.25f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //float currentRotation = meterNeedle.transform.rotation.eulerAngles.z;
        //meterNeedle.transform.rotation = Quaternion.Euler(0, 0, currentRotation);
    }

    // Update is called oncife per frame
    public void UpdateNeedle(bool isIncreased)
    {
        if (meterNeedle != null)
        {
            float currentRotation = meterNeedle.transform.localRotation.eulerAngles.z;

            Debug.Log($"Current Rotation: {currentRotation}");
            float targetRotation = currentRotation;
            if (isIncreased)
            {
                //if (currentRotation <= 150f)
                if (currentRotation < 180f)
                    targetRotation = currentRotation + 30f;
            }
            else
            {
                if (currentRotation >= 30f)
                    //if (currentRotation > 0f )
                    targetRotation = currentRotation - 30f;
            }

            if (isNeedleMoving == false)
            {
                this.GetComponent<SpeedController>().UpdateMotorSpeed(isIncreased);

                // stop any existing lerp and start a new one
                if (needleCoroutine != null)
                    StopCoroutine(needleCoroutine);
                isNeedleMoving = true;
                needleCoroutine = StartCoroutine(LerpNeedle(currentRotation, targetRotation, needleLerpDuration));
            }
        }

        speed = "Speed: " + this.GetComponent<SpeedController>().speed.ToString("0");
        speedText.text = speed;
    }

    IEnumerator LerpNeedle(float from, float to, float duration)
    {
        float elapsed = 0f;
        // handle wrap-around by normalizing angles
        from = Mathf.Repeat(from, 360f);
        to = Mathf.Repeat(to, 360f);

        // If the shortest path crosses the 0/360 boundary, adjust 'to' to take the shorter route
        float delta = Mathf.DeltaAngle(from, to);
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float angle = from + delta * t;
            meterNeedle.localRotation = Quaternion.Euler(0f, 0f, angle);
            yield return null;
        }
        meterNeedle.localRotation = Quaternion.Euler(0f, 0f, to);
        isNeedleMoving = false;
        needleCoroutine = null;
    }
}
