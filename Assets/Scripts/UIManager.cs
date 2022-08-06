using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonMonobehaviour<UIManager>
{
    [SerializeField] private Slider slider;

    public void FillKickMeter(float holdDownTimeNormalized)
    {
        slider.value = holdDownTimeNormalized;
    }

    public void ClearKickMeter(float holdDownTimeNoramlized)
    {
        StartCoroutine(ReturnKickMeterToZeroCoroutine());
    }

    private IEnumerator ReturnKickMeterToZeroCoroutine()
    {
        while (slider.value > 0)
        {
            slider.value -= Time.deltaTime;
            yield return null;
        }

    }
}
