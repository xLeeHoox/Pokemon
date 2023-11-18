using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerController : MonoBehaviour
{
    public float currentPower { get; private set; }
    MoveByVelocity moveByVelocity;
    [SerializeField] TextMeshProUGUI powerValue;
    private void Start()
    {
        moveByVelocity = GetComponentInParent<MoveByVelocity>();
    }
    public void Update()
    {

    }
    public void SetCurrentPower(float value)
    {
        currentPower = value;
        UpdatePowerValue();
    }
    public void IncreasePower(float value)
    {
        currentPower += value;
        UpdatePowerValue();
        UpdateTransformScale(value / 600);
        EvovelCharacter();

    }

    public void UpdatePowerValue()
    {
        int intCurrentPower = (int)currentPower;
        powerValue.text = intCurrentPower.ToString();
        UpdatePowerColor();

    }

    public void UpdateTransformScale(float value)
    {
        Transform parentTransform = transform.parent;
        Vector3 currentScale = parentTransform.localScale;
        parentTransform.localScale = new Vector3(currentScale.x + value, currentScale.y + value, currentScale.z + value);
    }
    public void EvovelCharacter()
    {
        if (currentPower > 2000)
        {
            IncreasePower(-currentPower * 0.5f);
            transform.parent.localScale = new Vector3(1, 1, 1);
            moveByVelocity.permanentSpeed *= 1.1f;

        }
    }
    public void SetPowerColorText(Color color)
    {
        powerValue.color = color;
    }
    public void UpdatePowerColor()
    {
        if (currentPower <= GameManager.Instance.player.powerController.currentPower)
        {
            SetPowerColorText(Color.green);
        }
        else
        {
            SetPowerColorText(Color.red);
        }
    }


}
